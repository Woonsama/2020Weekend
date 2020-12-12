using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class pc : MonoBehaviourPunCallbacks
{
    /*--------------------Const / Enum--------------------*/

    /*--------------------PublicMember--------------------*/
    [Header("Move Speed")]
    public float moveSpeed;

    [Header("Jump Power")]
    public float jumpPower;

    /*--------------------Private / Protected Member--------------------*/
    private Rigidbody rigid;
    private bool isJumpButtonPressed;
    private int direction;
    private PhotonView pv;

    /*--------------------Routine--------------------*/

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        Debug.Log(pv.IsMine);

        //if (pv.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.back * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                //if(pv.IsMine)
                isJumpButtonPressed = true;
            }
        }
    }


    private void FixedUpdate()
    {
        if(isJumpButtonPressed)
        {
            //if(pv.IsMine)
            //pv.RPC("Jump", RpcTarget.AllBuffered);
            Jump();
        }
    }

    /*--------------------Public Function--------------------*/

    /*--------------------Protected / Override--------------------*/

    /*--------------------Private Function--------------------*/

    #region private

    //[PunRPC]
    private void Jump()
    {
        //if(pv.IsMine)
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumpButtonPressed = false;
        }
    }

    #endregion private

    /*--------------------Event Function--------------------*/

    #region event

    #endregion event
}