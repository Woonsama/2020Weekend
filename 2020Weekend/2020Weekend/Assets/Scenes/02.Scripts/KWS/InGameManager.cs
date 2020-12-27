using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameManager : MonoBehaviourPun
{
    public enum EGameType
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
    }

    [Header("Level")]
    public EGameType eGameType;

    private const int c_MaxCharacterCount = 6;
    private Vector3 randomPosition;

    private PhotonView pv;

    private GameObject target;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        CreatePlayer();
       //if(pv.IsMine)
       //pv.RPC("CreatePlayer", RpcTarget.AllBuffered);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ResetPlayerPosition();

        if(target.GetComponent<PhotonView>().IsMine)
        {
            if(target.transform.root.GetChild(1).GetChild(0).localPosition.y < -15.0f)
            {
                ResetPlayerPosition();
            }
        }
    }

    // [PunRPC]
    private void CreatePlayer()
    {

        switch (eGameType)
        {
            case EGameType.Level1:
                break;
            case EGameType.Level2:
                {
                    int randomCharacter = Random.Range(0, c_MaxCharacterCount);
                    randomPosition = new Vector3(Random.Range(-7, 7), 2, Random.Range(-7, 7));

                    var player = PhotonNetwork.Instantiate("InGamePlayer/Player" + (randomCharacter + 1), randomPosition, Quaternion.identity);
                    player.name = "Player" + (randomCharacter + 1);

                    target = player;
                }
                break;

            case EGameType.Level3:
                break;
            case EGameType.Level4:
                break;
            case EGameType.Level5:
                break;
            case EGameType.Level6:
                {
                    int randomCharacter = Random.Range(0, c_MaxCharacterCount);
                    randomPosition = new Vector3(Random.Range(-7, 7), 2, Random.Range(-7, 7));

                    var player = PhotonNetwork.Instantiate("InGamePlayer/Player" + (randomCharacter + 1), randomPosition, Quaternion.identity);
                    player.name = "Player" + (randomCharacter + 1);

                    target = player;
                }

                break;
            default:
                break;
        }
    } 

    private void ResetPlayerPosition()
    {
        if(target.GetComponent<PhotonView>().IsMine)
        {
            //Debug.Log("Reset");
            //Debug.Log(target.transform.GetChild(1).name);
            target.transform.root.GetChild(1).GetChild(0).localPosition = new Vector3(0,2,0);
            target.transform.GetChild(1).localPosition = randomPosition;
        }
    }
}