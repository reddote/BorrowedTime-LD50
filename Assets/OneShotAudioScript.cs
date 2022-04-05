using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAudioScript : MonoBehaviour
{
    AudioSource source;
    public static OneShotAudioScript Instance;
    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    

    public void PlayOneShot(AudioClip clip,float pitch = 1f, float volume = 1f)
    {
        if (clip == null) return;
        source.pitch = pitch;
        source.PlayOneShot(clip,volume);
    }
}
