using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PhotonNetworkManager : SingletonMonoBase<PhotonNetworkManager>
{
    private void Awake()
    {
        SetRate();
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();
   
    public void SetNickName(string name)
    {
        PhotonNetwork.LocalPlayer.NickName = name;
    }
    
    #region private

    private void SetRate()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    //

    #endregion private
}