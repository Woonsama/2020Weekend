using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class pc : MonoBehaviourPunCallbacks
{
    /*--------------------Const / Enum--------------------*/
    private const int c_Max_JumpCount = 2;

    /*--------------------PublicMember--------------------*/
    [Header("Move Speed")]
    public float moveSpeed;

    [Header("Jump Power")]
    public float jumpPower;

    /*--------------------Private / Protected Member--------------------*/
    private Rigidbody rigid;
    private bool isJumpButtonPressed;
    private PhotonView pv;
    private int currentJumpCount;
    private int direction;

    /*--------------------Routine--------------------*/

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        //if (pv.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.localPosition += Vector3.forward * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.localPosition += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.localPosition += Vector3.back * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.localPosition += Vector3.right * moveSpeed * Time.deltaTime;
            }


            if (Input.GetKeyDown(KeyCode.Space) && currentJumpCount < c_Max_JumpCount)
            {
                //if(pv.IsMine)
                isJumpButtonPressed = true;
            }
        }
    }


    private void FixedUpdate()
    {
        JumpRayCheck();

        if(isJumpButtonPressed)
        {
            //if(pv.IsMine)
            //pv.RPC("Jump", RpcTarget.AllBuffered);

            if (currentJumpCount < c_Max_JumpCount)
            {
                OffRay_Jump();
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJumpButtonPressed = false;
                currentJumpCount++;
                Invoke("OnRay_Jump", 0.5f);
            }
        }
    }

    /*--------------------Public Function--------------------*/

    /*--------------------Protected / Override--------------------*/

    /*--------------------Private Function--------------------*/

    #region private
    bool isRayOn;

    private void JumpRayCheck()
    {
        RaycastHit hit;

        if(isRayOn)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.0f, LayerMask.GetMask("Map")))
            {
                Debug.Log("Hit");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
                currentJumpCount = 0;
            }
            else
            {
                Debug.Log("Not Hit");
                Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.down) * 5, Color.green);
            }
        }
    }

    private void OnRay_Jump()
    {
        isRayOn = true;
    }

    private void OffRay_Jump()
    {
        isRayOn = false;
    }

    [PunRPC]
    private void Jump()
    {
        if(pv.IsMine)
        {
            if (currentJumpCount < c_Max_JumpCount)
            {
                OffRay_Jump();
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJumpButtonPressed = false;
                currentJumpCount++;
                Invoke("OnRay_Jump", 0.5f);
            }
        }
    }

    #endregion private

    /*--------------------Event Function--------------------*/

    #region event

    #endregion event
}