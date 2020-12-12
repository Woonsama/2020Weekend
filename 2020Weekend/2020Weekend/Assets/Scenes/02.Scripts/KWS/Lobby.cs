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

    private void Awake()
    {
        button_GameStart.onClick?.AddListener(OnClick_GameStart);

        InitData();
        InitUI();
    }

    private void Update()
    {

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
            button_GameStart.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
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

    #endregion Event
}