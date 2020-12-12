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
    private float startDelayTime = 3;

    private PhotonView pv;

    private int currentPlayerCount;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        button_GameStart.onClick?.AddListener(OnClick_GameStart);

        InitData();
        InitUI();
    }

    private void Update()
    {
        SetConnectPlayerCount();

        if (Input.GetKeyDown("escape"))
        {
            OnClickBackBtn();
        }
    }


    #region private

    private void InitData()
    {
        
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
        //text_PlayerCount.text = PhotonNetwork.PlayerList.Length + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    private void SetGameStartButtonImage()
    {
        //Set GameStart Color And Enabled
        if (!PhotonNetwork.IsMasterClient)
        {
            button_GameStart.GetComponent<Image>().sprite = sprite_GameStart_Enable;
            button_GameStart.enabled = true;
        }
        else
        {
            button_GameStart.GetComponent<Image>().sprite = sprite_GameStart_Disable;
            button_GameStart.enabled = false;
        }
    }

    private IEnumerator StartTime()
    {
        text_PlayerCount.gameObject.SetActive(false);
        text_StartDelayText.gameObject.SetActive(true);

        int currentLeftTime;

        while(startDelayTime <= 0)
        {
            startDelayTime -= Time.deltaTime;
            currentLeftTime = (int)startDelayTime + 1;
            text_StartDelayText.text = currentLeftTime.ToString();
            yield return null;
        }

        GoToGameScene();
    }

    private void GoToGameScene()
    {
        SceneManager.LoadScene("InGameTest");
    }

    #endregion private

    #region Event

    private void OnClick_GameStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(StartTime());
        }
    }

    public void OnClickBackBtn()
    {
        SceneManager.LoadScene("Kws_Title_Test");
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(currentPlayerCount);
            stream.SendNext(startDelayTime);
        }
        else
        {
            currentPlayerCount = (int)stream.ReceiveNext();
            startDelayTime = (float)stream.ReceiveNext();
        }
    }



    #endregion Event
}