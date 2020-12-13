using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Item
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBroomingSound()
    {
        audioSource.Play();
    }
}
