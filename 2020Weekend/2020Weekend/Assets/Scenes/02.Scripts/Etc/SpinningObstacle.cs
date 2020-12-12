using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    // Start is called before the first frame update

    private float maxVel = 100.0f;
    private float minVel = 50.0f;
    private float randVel;

    [Range(0, 1)]
    int variant; 

    HingeJoint joint;
    JointMotor motor;

    void SetMotorVelocity()
    {
        variant = Random.Range(0, 1);

        if (variant == 0)
            randVel = -randVel;

        motor.targetVelocity = randVel;
    }

    void SetjointMotor()
    {
        joint.motor = motor;
    }

    void Start()
    {
        joint = GetComponent<HingeJoint>();
        randVel = minVel;        
        motor.force = 10000.0f;
        SetMotorVelocity();
        SetjointMotor();
        StartCoroutine(VelChangeTimer(15.0f));
    }

    // Update is called once per frame
    IEnumerator VelChangeTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (randVel > 0)
            randVel = Random.Range(randVel, maxVel);
        else
            randVel = Random.Range(randVel, -maxVel);
        SetMotorVelocity();
        SetjointMotor();
        StartCoroutine(VelChangeTimer(Random.Range(10.0f, 15.0f)));
    }
}
