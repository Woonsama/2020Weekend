using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameManager : MonoBehaviourPun
{
    [Header("Main Camera")]
    public Camera mainCamera;

    private PhotonView pv;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        CreatePlayer();
        SetMainCameraFalse();
        //pv.RPC("CreatePlayer", RpcTarget.AllBuffered);
        //pv.RPC("SetMainCameraFalse", RpcTarget.AllBuffered);
    }

    //[PunRPC]
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("BearBrown", new Vector3(Random.Range(-10, 10), 2, Random.Range(-10, 10)), Quaternion.identity);
    }

    //[PunRPC]
    private void SetMainCameraFalse()
    {
        mainCamera.gameObject.SetActive(false);
    }
}