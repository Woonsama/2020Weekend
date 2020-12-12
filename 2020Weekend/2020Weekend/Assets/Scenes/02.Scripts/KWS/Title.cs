using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;

public class Title : MonoBehaviourPunCallbacks
{

    [Header("닉네임 입력 란")]
    public TMP_InputField nickNameInputField;

    [Header("로비로 이동 버튼")]
    public Button button_GameStart;

    [Header("최대로 접속 가능한 인원")]
    private byte maxPlayer;

    [Header("AudioSource")]
    public GameObject titleAndLobbyObj;

    private void Awake()
    {
        DontDestroyOnLoad(titleAndLobbyObj);
        button_GameStart.onClick?.AddListener(OnClick_GameStart);
    }

    #region private

    private void SetNickName()
    {
        PhotonNetwork.NickName = nickNameInputField.text;
    }

    #endregion private


    #region Event

    private void OnClick_GameStart()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //PhotonNetwork.GameVersion
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결 성공");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("서버 연결 실패");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 참가 성공");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 참가 실패");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayer });
    }

    #endregion Event

}