using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    [Header("Button GameStart")]
    public Button button_GameStart;

    [Header("Disable GameStartObj")]
    public GameObject disable_startBtn;

    private void Awake()
    {
        button_GameStart.onClick?.AddListener(OnClick_GameStart);

        InitData();
        InitUI();
    }

    private void Update()
    {
        if(Input.GetKeyDown("escape"))
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
        //Set GameStart Color And Enabled
        if (!PhotonNetwork.IsMasterClient)
        {
            button_GameStart.enabled = false;
            disable_startBtn.SetActive(true);
        }
    }


    #endregion private

    #region Event

    private void OnClick_GameStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            
        }
    }

    public void OnClickBackBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
    #endregion Event
}