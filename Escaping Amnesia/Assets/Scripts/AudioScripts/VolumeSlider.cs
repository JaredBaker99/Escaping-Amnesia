using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;
    OverworldAudioManager overworldAudioManager;
    void Start()
    {
        //Set Player prefs
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);

        if (!PlayerPrefs.HasKey("music"))
            PlayerPrefs.SetFloat("music", 0);

        if (!PlayerPrefs.HasKey("sfxVolume"))
            PlayerPrefs.SetFloat("sfxVolume", 0);

        //Load after ensuring playerprefs exist
        Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = MasterVolumeSlider.value;
        Save();
    }

    public void SetMusicVolume()
    {
        myMixer.SetFloat("music", MusicSlider.value);
        PlayerPrefs.SetFloat("music", MusicSlider.value);
    }

    public void SetSFXVolume()
    {
        myMixer.SetFloat("sfx", SFXSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", SFXSlider.value);
    }

    private void Load()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("music");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", MasterVolumeSlider.value);
        PlayerPrefs.SetFloat("music", MusicSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", SFXSlider.value);
    }

}