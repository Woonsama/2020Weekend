using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCameraController : MonoBehaviour
{
    //Public / SerializeField
    public Vector3 cameraOffset;

    public Vector3 cameraRotation;

    public float cameraRotateSpeed;

    public float cameraRotateRecognizeRange;

    //Private
    private GameObject target;
    private Camera cam;

    private void Awake()
    {
        SetTargetTest();
        SetCamera();
    }

    private void Update()
    {      
        CameraRotateCheck();
        FollowTargetPosition();
    }

    private void SetCamera()
    {
        cam = GetComponent<Camera>();
        cam.transform.parent = target.transform.GetChild(0).transform;
        cam.transform.localPosition = Vector3.zero + cameraOffset;
        cam.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    private void SetTarget()
    {
        pc[] targetList;

        targetList = FindObjectsOfType<pc>();

        for(int i = 0; i < targetList.Length; i++)
        {
            if(targetList[i].photonView.IsMine)
            {
                target = targetList[i].transform.parent.gameObject;
                return;
            }
        }
    }

    private void SetTargetTest()
    {
        target = GameObject.Find("Player").transform.parent.gameObject;
    }

    private void FollowTargetPosition()
    {
        cam.transform.localPosition = Vector3.zero + cameraOffset;
    }

    private bool isClicked = false;
    private Vector2 originalPos;
    private Vector2 currentPos;
    private Vector3 originalPlayerPos;

    private void CameraRotateCheck()
    {
        if(Input.GetMouseButtonDown(0)){
            isClicked = true;
            originalPos = Input.mousePosition;
            currentPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;

        }

        if(Input.GetMouseButtonUp(0)){
            isClicked = false;
            originalPos = Vector2.zero;
            currentPos = Vector2.zero;
        }
    }

}

[System.Serializable]

public class DoubleFloat
{
    public float item1;
    public float item2;

    public DoubleFloat(float item1, float item2)
    {
        this.item1 = item1;
        this.item2 = item2;
    }
}