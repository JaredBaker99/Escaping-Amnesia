using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("-------Audio Clips----------")]
    public AudioClip MainMenuMusic;
    public AudioClip StartClick;
    public AudioClip hover;
    public AudioClip buttonClick;

    private void Start(){
        musicSource.clip = MainMenuMusic;
        musicSource.Play();
        sfxSource.clip = MainMenuMusic;
    }

    public void PlaySFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }


}
