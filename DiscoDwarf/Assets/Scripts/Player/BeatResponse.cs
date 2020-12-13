using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatResponse : MonoBehaviour
{
    public static BeatResponse Instance = null;

    public AudioClip missClip;
    public AudioClip goodClip;

    private AudioSource audioSource = null;

    private void Awake()
    {
        if (BeatResponse.Instance == null)
            BeatResponse.Instance = this;
        else
            Destroy(this);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMissClip()
    {
        audioSource.clip = missClip;
        audioSource.Play();
    }

    public void PlayGoodClip()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = goodClip;
            audioSource.Play();
        }
    }
}
