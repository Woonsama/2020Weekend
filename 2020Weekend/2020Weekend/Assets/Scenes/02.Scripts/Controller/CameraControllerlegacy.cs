using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerlegacy : MonoBehaviour
{
    public Transform target;
    public float lookSmooth = 0.09f;
    // Camera springArm
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
    // Player Look up and down
    public float xTilt = 1;
    // Camera target location
    Vector3 destination = Vector3.zero;

    PlayerMovement charController;
    float rotateVel = 0;



    // Start is called before the first frame update
    void Start()
    {
        SetCameraTarget(target);
    }

    // Set the Camera to look the new Target
    void SetCameraTarget(Transform _transfrom)
    {
        target = _transfrom;

        if(target != null)
        {
            if (target.GetComponent<PlayerMovement>())
                charController = target.GetComponent<PlayerMovement>();
        }
    }

    // Update after Update Func is called
    void LateUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }

    void MoveToTarget()
    {
        destination = charController.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        transform.rotation = Quaternion.Euler(xTilt,eulerYAngle, 0);
    }
}
