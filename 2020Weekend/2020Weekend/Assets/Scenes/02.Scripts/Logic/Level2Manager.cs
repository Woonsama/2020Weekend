using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Level2Manager : MonoBehaviourPun
{
    [Header("Depart Pos")]
    public GameObject departPos;

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.collider.tag ==)
    }
}