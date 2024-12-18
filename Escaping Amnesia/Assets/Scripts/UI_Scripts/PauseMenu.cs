using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public static bool pauseButtonClicked;
    public static bool isPaused;//Used to tell if game is paused.
    public DoNotDestroy targetScript;
    public CanvasGroup fadeToBlack;
    private Slider masterVolumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        masterVolumeSlider = GameObject.Find("MasterVolumeSlider").GetComponent<Slider>();
        isPaused = false;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        settingsMenu = GameObject.Find("SettingsMenu");
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                isPaused = false;
            }
            else
            {
                PauseGame();
                isPaused = true;
            }
        }
    }

    public void PauseGameButton()
    {//Pause button pauses the game
        if (isPaused)
        {
            ResumeGame();
            isPaused = false;
        }
        else
        {
            PauseGame();
            isPaused = true;
        }
    }
    public void PauseGame()
    {//Pause game and open menu
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {//Resume game and close menus
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenSettings()
    {//Open the settings menu 
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void QuitGame()
    {//Exit to Main Menu (Save too)
        pauseMenu.SetActive(false);
        StartCoroutine(FadeToBlack());

    }

    public void BackToPause()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Mute()
    {
        PlayerPrefs.SetFloat("musicVolume", 0);
        AudioListener.volume = 0;
        masterVolumeSlider.value = 0;
    }

    private IEnumerator FadeToBlack()//Fade the scene when the quit button is clikced
    {
        while (fadeToBlack.alpha < 1)
        {
            float opacity = fadeToBlack.alpha + .01f;
            Mathf.Clamp(opacity, 0, 1);
            fadeToBlack.alpha = opacity;
            yield return new WaitForSecondsRealtime(.01f);
        }
        DelayedLoad();
    }

    private void DelayedLoad()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        if(targetScript == null){
            targetScript = GameObject.FindGameObjectWithTag("OverworldAudio").GetComponent<DoNotDestroy>();
        }
        if (targetScript != null)
            targetScript.ChangedScenes();
    }
}
