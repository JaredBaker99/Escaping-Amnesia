using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OverworldAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource musicSource2;
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
        musicSource.Play();
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
        //If in upgrade room, check room count and change audio

        sceneCounter = GameObject.Find("Scene Counter");

        if (curCount % 5 == 0)
        {

            if (curCount > 0 && curCount < 5)
            {
                //Do Nothing

            }
            else if (curCount >= 5 && curCount < 10)
            {
                changeTracks(OverworldMusicSoft);
            }
            else if (curCount >= 10 && curCount < 15)
            {
                changeTracks(OverworldMusicLow);
            }
            else if (curCount >= 15 && curCount < 21)
            {
                changeTracks(OverworldMusicMedium);
            }
            else{//Stop the music. This is to let the potential cutscene roll. After which, start the High or Boss theme
                //musicSource.Stop();

                //For now test chnaging it to End
                changeTracks(OverworldMusicHigh); 
            }
        }
    }

    private void changeTracks(AudioClip newMusic)
    {
        int playingTrack;
        if (musicSource.isPlaying)
        {
            float time = musicSource.time;
            musicSource2.clip = newMusic;
            musicSource2.Play();
            musicSource2.volume = 0;
            musicSource2.time = time;
            playingTrack = 1;
        }
        else{
            float time = musicSource2.time;
            musicSource.clip = newMusic;
            musicSource.Play();
            musicSource.volume = 0;
            musicSource.time = time;
            playingTrack = 2;
        }
        StartCoroutine(FadeTrack(playingTrack));
    }
    private IEnumerator FadeTrack(int track)
    {
        float timeToFade = 2.25f;
        float timeElapsed = 0;
        if (track == 1)
        {

            while (timeElapsed < timeToFade)
            {
                musicSource2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            musicSource.Stop();

        }
        else
        {
            while (timeElapsed < timeToFade)
            {
                musicSource.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            musicSource2.Stop();
        }
    }
}
