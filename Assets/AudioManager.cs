using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource sfxSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip backgroundMusic;
    
    // Update is called once per frame
    private void Start()
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
}
