using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CamMovement : MonoBehaviour
{
    private const int c_Max_JumpCount = 2;


    public Transform centralAxis;
    public float camSpeed;
    public float camOffsetY;
    public float camOffsetZ;
    float mouseX;
    float mouseY;
    

    public Transform cam;
    float wheel;

    public Transform player;
    public Transform playerAxis;
    public float playerMoveSpeed;
    public float jumpPower;
    Vector3 movement;

    private Rigidbody rigid;
    private bool isJumpButtonPressed;
    private PhotonView pv;
    private int currentJumpCount;

    private void Awake()
    {
        rigid = player.GetComponent<Rigidbody>();
        //pv = GetComponent<PhotonView>();

    }

    private void Update()
    {
        CamMove();
        Zoom();
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.Space) && currentJumpCount < c_Max_JumpCount)
        {
            //if(pv.IsMine)
            isJumpButtonPressed = true;
        }
    }

    private void FixedUpdate()
    {
        JumpRayCheck();

        if (isJumpButtonPressed)
        {
            //if(pv.IsMine)
            //pv.RPC("Jump", RpcTarget.AllBuffered);

            if (currentJumpCount < c_Max_JumpCount)
            {
                OffRay_Jump();
                if(currentJumpCount == 0)
                {
                    player.GetComponent<Animator>().SetBool("IsJump", true);
                }
                else
                {
                    player.GetComponent<Animator>().SetBool("isDoubleJump", true);
                    player.GetComponent<Animator>().SetBool("IsJump", false);
                }

                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJumpButtonPressed = false;
                currentJumpCount++;
                Invoke("OnRay_Jump", 0.5f);
            }
        }
    }

    private void LateUpdate()
    {
        // 카메라 중심축이 플레이어 따라다니기
        centralAxis.position = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z);
    }

    private void CamMove()
    {
        if(Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            centralAxis.rotation = Quaternion.Euler(
                new Vector3(
                    centralAxis.rotation.x + mouseY,
                    centralAxis.rotation.y + mouseX,
                    0) * camSpeed);
        }

        //Rotate By Cam
        playerAxis.eulerAngles = new Vector3(playerAxis.eulerAngles.x, centralAxis.eulerAngles.y, playerAxis.eulerAngles.z);
    }

    private void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel");
        if (wheel >= -1)
            wheel = -1;
        if (wheel <= -5)
            wheel = -5;

        cam.localPosition = new Vector3(0, camOffsetY, wheel + camOffsetZ);
    }

    private void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector3(moveX * playerMoveSpeed, 0, moveY * playerMoveSpeed);

        if (movement != Vector3.zero)
        {
            playerAxis.Translate(movement * Time.deltaTime);
            player.localRotation = Quaternion.Slerp(player.transform.localRotation,
                Quaternion.LookRotation(movement), 5 * Time.deltaTime);

            player.GetComponent<Animator>().SetBool("isMove", true);
        }
        else
        {
            player.GetComponent<Animator>().SetBool("isMove", false);
        }
    }

    bool isRayOn;

    private void JumpRayCheck()
    {
        RaycastHit hit;

        if (isRayOn)
        {
            if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.down), out hit, 1.0f, LayerMask.GetMask("Map")))
            {
                Debug.Log("Hit");
                Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
                currentJumpCount = 0;
                player.GetComponent<Animator>().SetBool("IsJump", false);
                player.GetComponent<Animator>().SetBool("isDoubleJump", false);
            }
            else
            {
                Debug.Log("Not Hit");
                Debug.DrawLine(player.transform.position, player.transform.TransformDirection(Vector3.down) * 5, Color.green);
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
        if (pv.IsMine)
        {
            if (currentJumpCount < c_Max_JumpCount)
            {
                isJumpButtonPressed = false;
                OffRay_Jump();
                if (currentJumpCount == 0)
                {
                    player.GetComponent<Animator>().SetBool("IsJump", true);
                }
                else
                {
                    player.GetComponent<Animator>().SetBool("IsDoubleJump", true);
                    player.GetComponent<Animator>().SetBool("IsJump", false);
                }
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                currentJumpCount++;
                Invoke("OnRay_Jump", 0.5f);
            }
        }
    }
}