using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    public float rotationSpeed = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 1);

        if (rand == 0)
            rotationSpeed = -rotationSpeed;

        StartCoroutine(RotChangeTimer(8.0f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.Self);
    }

    IEnumerator RotChangeTimer(float waitTime)
    {   
        yield return new WaitForSeconds(waitTime);
        rotationSpeed = -rotationSpeed;
        StartCoroutine(RotChangeTimer(8.0f));
    }

}
