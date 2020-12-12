using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSample : MonoBehaviour
{
    private void Awake()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    //public override void OnConnectedToMaster()
    //{
    //    //닉네임 설정
    //    PhotonNetwork.LocalPlayer.NickName = 
    //}

}