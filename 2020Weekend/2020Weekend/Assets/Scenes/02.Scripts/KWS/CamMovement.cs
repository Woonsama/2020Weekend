using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CamMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    private const int c_Max_JumpCount = 2;

    [Header("테스트")]
    public bool isTest;   

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

    private bool isJumpButtonPressed;
    private int currentJumpCount;

    public PlayerClip playerClips;
    public PlayerParticle playerParticles;

    [HideInInspector]
    public PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(pv.IsMine || isTest)
        {
            CamMove();
            Zoom();
            PlayerMove();

            if (Input.GetKeyDown(KeyCode.Space) && currentJumpCount < c_Max_JumpCount)
            {
                isJumpButtonPressed = true;
            }
        }
        else
        {
            if (cam.GetComponent<PhotonView>().IsMine || isTest)
                cam.gameObject.SetActive(true);
            else
                cam.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if(pv.IsMine && GetComponent<PhotonView>().IsMine || isTest)
        {
            JumpRayCheck();

            if (isJumpButtonPressed && !isTest)
            {
                pv.RPC("Jump", RpcTarget.AllBuffered);
            }
            else if(isJumpButtonPressed && isTest)
            {
                if (player.GetComponent<PhotonView>().IsMine || isTest)
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
                        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                        currentJumpCount++;

                        Invoke("OnRay_Jump", 0.5f);

                        //Sound
                        player.transform.root.GetChild(1).GetChild(0).GetChild(player.transform.root.GetChild(1).GetChild(0).childCount - 1).GetComponent<AudioSource>().PlayOneShot(playerClips.clip_Jump);

                        //Particle
                        ParticleSystem temp_Particle = Instantiate(playerParticles.particle_Jump, player.transform.root.GetChild(1).GetChild(0).position, Quaternion.identity);
                        temp_Particle.Play();
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        // 카메라 중심축이 플레이어 따라다니기
        if(pv.IsMine && cam.GetComponent<PhotonView>().IsMine || isTest)
        centralAxis.position = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z);
    }

    private void CamMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //if (pv.IsMine && player.GetComponent<PhotonView>().IsMine)
            //    player.localPosition = new Vector3(0, player.localPosition.y, 0);//  플레이어 부모 - 자식 고정 오류
        }

        if(Input.GetMouseButton(0))
        {
            if(pv.IsMine && centralAxis.GetComponent<PhotonView>().IsMine || isTest)
            {
                mouseX += Input.GetAxis("Mouse X");
                mouseY += Input.GetAxis("Mouse Y") * -1;

                centralAxis.rotation = Quaternion.Euler(
                    new Vector3(
                        centralAxis.rotation.x + mouseY,
                        centralAxis.rotation.y + mouseX,
                        0) * camSpeed);
            }
        }

        //Rotate By Cam
        if(pv.IsMine && playerAxis.GetComponent<PhotonView>().IsMine || isTest)
        playerAxis.eulerAngles = new Vector3(playerAxis.eulerAngles.x, centralAxis.eulerAngles.y, playerAxis.eulerAngles.z);
    }

    private void Zoom()
    {
        if(pv.IsMine && cam.GetComponent<PhotonView>().IsMine || isTest)
        {
            wheel += Input.GetAxis("Mouse ScrollWheel");
            if (wheel >= -1)
                wheel = -1;
            if (wheel <= -5)
                wheel = -5;

            cam.localPosition = new Vector3(0, camOffsetY, wheel + camOffsetZ);
        }
    }

    private void PlayerMove()
    {
        if(pv.IsMine && player.GetComponent<PhotonView>().IsMine || isTest)
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
    }

    bool isRayOn;

    private void JumpRayCheck()
    {
        if(player.GetComponent<PhotonView>().IsMine || isTest)
        {
            RaycastHit hit;

            if (isRayOn)
            {
                if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.down), out hit, 1.0f, LayerMask.GetMask("Map")))
                {
                    Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
                    currentJumpCount = 0;
                    player.GetComponent<Animator>().SetBool("IsJump", false);
                    player.GetComponent<Animator>().SetBool("isDoubleJump", false);
                }
                else
                {
                    Debug.DrawLine(player.transform.position, player.transform.TransformDirection(Vector3.down) * 5, Color.green);
                }
            }
        }
    }

    private void OnRay_Jump()
    {
        if(pv.IsMine || isTest) isRayOn = true;
    }

    private void OffRay_Jump()
    {
        if(pv.IsMine || isTest) isRayOn = false;
    }

    [PunRPC]
    private void Jump()
    {
        if (player.GetComponent<PhotonView>().IsMine || isTest)
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
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                currentJumpCount++;

                Invoke("OnRay_Jump", 0.5f);

                //Sound
                player.transform.root.GetChild(1).GetChild(0).GetChild(player.transform.root.GetChild(1).GetChild(0).childCount - 1).GetComponent<AudioSource>().PlayOneShot(playerClips.clip_Jump);

                //Particle
                ParticleSystem temp_Particle = Instantiate(playerParticles.particle_Jump, player.transform.root.GetChild(1).GetChild(0).position,Quaternion.identity);
                temp_Particle.Play();
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            //stream.SendNext(wheel);
            //stream.SendNext(currentJumpCount);
            //stream.SendNext(isRayOn);
            //stream.SendNext(isJumpButtonPressed);
            //stream.SendNext(mouseX);
            //stream.SendNext(mouseY);
        }
        else
        {
            //wheel = (float)stream.ReceiveNext();
            //currentJumpCount = (int)stream.ReceiveNext();
            //isRayOn = (bool)stream.ReceiveNext();
            //isJumpButtonPressed = (bool)stream.ReceiveNext();
            //mouseX = (float)stream.ReceiveNext();
            //mouseY = (float)stream.ReceiveNext();
        }
    }
}

[System.Serializable]

public class PlayerClip
{
    public AudioClip clip_Jump;
}

[System.Serializable]

public class PlayerParticle
{
    public ParticleSystem particle_Jump;
}