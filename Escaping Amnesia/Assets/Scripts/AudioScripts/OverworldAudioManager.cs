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
                StartCoroutine(FadeTrack(OverworldMusicSoft));
            }
            else if (curCount >= 10 && curCount < 15)
            {
                StartCoroutine(FadeTrack(OverworldMusicLow));
            }
            else if (curCount >= 15 && curCount < 21)
            {
                StartCoroutine(FadeTrack(OverworldMusicMedium));
            }
            // else{//Stop the music. This is to let the potential cutscene roll. After which, start the High or Boss theme
            //     musicSource.Stop();
            // }
        }
    }


    private IEnumerator FadeTrack(AudioClip newMusic)
    {
        float timeToFade = 1.5f;
        float timeElapsed = 0;
        if (musicSource.isPlaying)
        {
            musicSource2.clip = newMusic;
            musicSource2.Play();
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
                musicSource.clip = newMusic;
                musicSource.Play();
                musicSource.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            musicSource2.Stop();
        }
    }
}
