using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    const int c_Pattern_Count = 3;

    [Header("초기 속도")]
    public float initialSnakeSpeed;

    [Header("패턴 당 속도 증감치")]
    public float accelerationSnakeSpeed;

    private float currentAnimationSpeed;

    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();

        currentAnimationSpeed = initialSnakeSpeed;
    }

    public void ChangePattern()
    {
        currentAnimationSpeed += accelerationSnakeSpeed;

        int randomAnimIndex = Random.Range(0, c_Pattern_Count) + 1;

        anim.Play("snake_" + randomAnimIndex);
        anim["snake_" + randomAnimIndex].speed = currentAnimationSpeed;
    }

}