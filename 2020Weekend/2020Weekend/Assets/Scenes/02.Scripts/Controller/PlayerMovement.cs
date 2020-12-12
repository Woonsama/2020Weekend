using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float rotateVel = 100;

    Quaternion targetRotation;
    Rigidbody rigidBody;
    float forwardInput, turnInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rigidBody = GetComponent<Rigidbody>();
        forwardInput = 0;
        turnInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        Run();
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void Run()
    {
        if(Mathf.Abs(forwardInput) > inputDelay)
        {
            rigidBody.velocity = transform.forward * forwardInput * forwardVel;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
            
        transform.rotation = targetRotation;
    }
}
