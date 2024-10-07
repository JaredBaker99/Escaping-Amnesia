using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("-------Audio Clips----------")]
    public AudioClip OverworldMusic;

    private void Start(){
        musicSource.clip = OverworldMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }
}
