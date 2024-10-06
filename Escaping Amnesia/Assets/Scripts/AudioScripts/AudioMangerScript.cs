using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMangerScript : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("-------Audio Clips----------")]
    public AudioClip MainMenuMusic;

    private void Start(){
        musicSource.clip = MainMenuMusic;
        musicSource.Play();
        sfxSource.clip = MainMenuMusic;
    }


}
