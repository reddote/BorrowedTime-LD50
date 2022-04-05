using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimationSounds : MonoBehaviour
{
    [SerializeField]
    AudioClip footStepClip;

    public void PlayFootStep()
    {
        OneShotAudioScript.Instance.PlayOneShot(footStepClip, Random.Range(.9f, 1.1f), Random.Range(.9f, 1.1f));

    }
}
