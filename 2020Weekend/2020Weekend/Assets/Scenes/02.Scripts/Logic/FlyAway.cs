using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
    [Header("Power")]
    public Vector3 power;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(1);
            if (other.transform.root.GetChild(2).GetComponent<CamMovement>().pv.IsMine)
            {
                Debug.Log(2);
                Quaternion targetRotation = Quaternion.LookRotation(other.transform.root.GetChild(1).position - this.transform.position, this.transform.up);

                Debug.Log(targetRotation);

                //if (targetRotation.x > 0)
                //{
                //    other.transform.root.GetChild(1).GetComponent<Rigidbody>().velocity = Vector3.zero;
                //    other.transform.root.GetChild(1).GetComponent<Rigidbody>().AddForce(power);

                //}
                //else if (targetRotation.x < 0)
                //{
                //    other.transform.root.GetChild(1).GetComponent<Rigidbody>().velocity = Vector3.zero;
                //    other.transform.root.GetChild(1).GetComponent<Rigidbody>().AddForce(power * -1);
                //}
            }
        }
    }
}