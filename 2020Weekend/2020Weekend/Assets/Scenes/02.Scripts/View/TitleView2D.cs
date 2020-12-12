using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleView2D : MonoBehaviour
{
    [SerializeField]
    AudioClip _bgmClip;

    private void Awake()
    {
        SoundManager.Instance.Play(_bgmClip);
    }

    public void OnClickCustomBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Customize");
    }

}
