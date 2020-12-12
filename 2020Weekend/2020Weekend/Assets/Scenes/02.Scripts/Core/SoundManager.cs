using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBase<SoundManager>
{
    [SerializeField]
    AudioClip _btnClip;

    AudioSource _audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void Play(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void PlayBtnSound()
    {
        Play(_btnClip);
    }

}
