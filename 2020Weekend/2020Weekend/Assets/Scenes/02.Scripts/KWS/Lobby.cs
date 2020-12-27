using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Button GameStart")]
    public Button button_GameStart;

    [Header("Button Back")]
    public Button button_Back;  
    
    [Header("Button Change Sprite")]
    public Sprite sprite_GameStart_Enable;
    public Sprite sprite_GameStart_Disable;

    [Header("대기 / 시작 이미지")]
    public Image image_Delay;
    public Image image_Start;

    [Header("텍스트 - 인원 표시")]
    public Text text_PlayerCount;

    [Header("텍스트 - 시작까지 남은 시간")]
    public Text text_StartDelayText;

    [Header("시작 대기 시간")]
    private float startDelayTime = 4;

    private PhotonView pv;

    private int currentPlayerCount;
    private float currentLeftTime;
    bool isGameStart;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        Debug.Log(pv.IsMine);
        button_GameStart.onClick?.AddListener(OnClick_GameStart);
        button_Back.onClick?.AddListener(OnClickBackBtn);

        InitData();
        InitUI();
    }

    private void Update()
    {
        SetConnectPlayerCount();

        if(isGameStart)
        {
            if(currentLeftTime >= 0)
            {
                currentLeftTime -= Time.deltaTime;
                text_StartDelayText.text = ((int)currentLeftTime + 1).ToString();
            }
            else
            {
                isGameStart = false;
                GoToGameScene();
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            OnClickBackBtn();
        }
    }


    #region private

    private void InitData()
    {
        currentLeftTime = startDelayTime;
        
    }

    private void InitUI()
    {

        SetConnectPlayerCount();
        SetGameStartButtonImage();
    }

    private void SetConnectPlayerCount()
    {
        currentPlayerCount = PhotonNetwork.PlayerList.Length;
        text_PlayerCount.text = currentPlayerCount + "/" + 8;
    }

    private void SetGameStartButtonImage()
    {
        //Set GameStart Color And Enabled
        if (!PhotonNetwork.IsMasterClient)
        {
            button_GameStart.GetComponent<Image>().sprite = sprite_GameStart_Disable;
            button_GameStart.enabled = false;
        }
        else
        {
            button_GameStart.GetComponent<Image>().sprite = sprite_GameStart_Enable;
            button_GameStart.enabled = true;
        }
    }




    private void GoToGameScene()
    {
        Destroy(GameObject.Find("TitleAndLobbySource"));
        PhotonNetwork.LoadLevel("Level_6");
    }

    #endregion private

    #region Event

    private void OnClick_GameStart()
    {
        currentLeftTime = startDelayTime;
        isGameStart = true;


        pv.RPC("SetTextInfo", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void SetTextInfo()
    {
        image_Delay.gameObject.SetActive(false);
        image_Start.gameObject.SetActive(true);

        text_PlayerCount.enabled = false;
        text_StartDelayText.enabled = true;
    }

    public void OnClickBackBtn()
    {
        Destroy(GameObject.Find("TitleAndLobbySource"));
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Kws_Title_Test");
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(startDelayTime);
            stream.SendNext(currentPlayerCount);
            stream.SendNext(currentLeftTime);
            stream.SendNext(isGameStart);
        }
        else
        {
            startDelayTime = (float)stream.ReceiveNext();
            currentPlayerCount = (int)stream.ReceiveNext();
            currentLeftTime = (float)stream.ReceiveNext();
            isGameStart = (bool)stream.ReceiveNext();
        }
    }



    #endregion Event
}