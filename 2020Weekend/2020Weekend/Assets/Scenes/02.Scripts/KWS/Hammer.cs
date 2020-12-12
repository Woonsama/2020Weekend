using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [Header("Start Time")]
    public float startTime;

    [Header("Animation Speed")]
    public float animationSpeed = 1;

    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        Invoke("PlayAnim", startTime);
    }

    private void PlayAnim()
    {
        anim["anim_hammer"].speed = animationSpeed;
        anim.Play("anim_hammer");
    }

}
