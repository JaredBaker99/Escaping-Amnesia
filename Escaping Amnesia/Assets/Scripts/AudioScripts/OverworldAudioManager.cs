using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OverworldAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource musicSource2;
    [SerializeField] AudioSource musicSource3;
    [SerializeField] AudioSource musicSource4;
    [SerializeField] AudioSource musicSource5;
    [SerializeField] AudioSource sfxSource;
    [Header("-------Audio Clips----------")]
    public AudioClip OverworldMusicStart;
    public AudioClip OverworldMusicSoft;
    public AudioClip OverworldMusicLow;
    public AudioClip OverworldMusicMedium;
    public AudioClip OverworldMusicHigh;
    public AudioClip BattleMusic;
    public AudioClip BossRoomMusic;
    private GameObject sceneCounter;



    private void Start()
    {
        sceneCounter = GameObject.Find("Scene Counter");
        musicSource.clip = OverworldMusicStart;
        musicSource2.clip = OverworldMusicSoft;
        musicSource3.clip = OverworldMusicLow;
        musicSource4.clip = OverworldMusicMedium;
        musicSource5.clip = OverworldMusicHigh;
        musicSource.Play();
        musicSource2.Play();
        musicSource3.Play();
        musicSource4.Play();
        musicSource5.Play();

    }

    public void BattleStart()
    {
        musicSource.Stop();
        musicSource.clip = BattleMusic;
        musicSource.Play();
    }

    public void BattleEnd()
    {
        musicSource.Stop();
        musicSource.clip = OverworldMusicSoft;
        musicSource.Play();

    }

    public void BossRoom()
    {
        musicSource.Stop();
        musicSource.clip = BossRoomMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    //if scene count < 5, play Low Soft
    public void changeMusic(int curCount)
    {
        //If in upgrade room, check room count and increase the volume of the respective tracks

        sceneCounter = GameObject.Find("Scene Counter");

        if (curCount % 5 == 0)
        {

            if (curCount > 0 && curCount < 5)
            {
                //Reset volumes
                musicSource2.volume = 0;
                musicSource3.volume = 0;
                musicSource4.volume = 0;
                musicSource5.volume = 0;

            }
            else if (curCount >= 5 && curCount < 10)
            {
                StartCoroutine(blendTrack(musicSource2));
            }
            else if (curCount >= 10 && curCount < 15)
            {
                StartCoroutine(blendTrack(musicSource3));
            }
            else if (curCount >= 15 && curCount < 20)
            {
                StartCoroutine(blendTrack(musicSource4));
            }
            else
            {//Stop the music. This is to let the potential cutscene roll. After which, start the High or Boss theme
                //musicSource.Stop();

                //For now test chnaging it to End
                StartCoroutine(blendTrack(musicSource5));
            }
        }
    }

    private IEnumerator blendTrack(AudioSource musicTrack)
    {
        float timeToFade = 2.25f;
        float timeElapsed = 0;
        while (timeElapsed < timeToFade){
            musicTrack.volume = Mathf.Lerp(0, 1, timeElapsed/timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
