using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    // Start is called before the first frame update

    private float maxVel = 100.0f;
    private float minVel = 50.0f;
    private float randVel;
    
    HingeJoint joint;
    JointMotor motor;

    void SetMotorVelocity(float vel)
    {
        motor.targetVelocity = vel;
    }

    void SetjointMotor()
    {
        joint.motor = motor;
    }

    void Start()
    {
        joint = GetComponent<HingeJoint>();
        randVel = Random.Range(maxVel, minVel);
        motor.force = 10000.0f;
        SetMotorVelocity(randVel);
        SetjointMotor();
        StartCoroutine(VelChangeTimer(Random.Range(8.0f, 15.0f)));
    }

    // Update is called once per frame
    IEnumerator VelChangeTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetMotorVelocity(-randVel);
        SetjointMotor();
        StartCoroutine(VelChangeTimer(Random.Range(8.0f, 15.0f)));
    }
}
