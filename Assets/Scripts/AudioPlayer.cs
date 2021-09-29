using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip collectSoundEffect;

    public AudioSource bgmAudioSource;
    public AudioClip backgroundMusic;

    public bool loopBGM;
    public float bgmInitialVolume;

    private void Awake()
    {
        sfxAudioSource = gameObject.GetComponent<AudioSource>();

        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.clip = backgroundMusic;
        bgmAudioSource.loop = loopBGM;
        bgmAudioSource.volume = bgmInitialVolume;

        bgmAudioSource.Play();
    }

    public void PlayCollectSoundEffect()
    {
        sfxAudioSource.clip = collectSoundEffect;

        sfxAudioSource.Play();
    }

    public void MuteBgm()
    {
        bgmAudioSource.volume = 0;
    }

    public void UnmuteBgm()
    {
        bgmAudioSource.volume = bgmInitialVolume;
    }
}
