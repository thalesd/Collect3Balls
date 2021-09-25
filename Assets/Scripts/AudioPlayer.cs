using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip collectSoundEffect;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayCollectSoundEffect()
    {
        audioSource.clip = collectSoundEffect;

        audioSource.Play();
    }
}
