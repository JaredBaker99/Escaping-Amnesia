using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public static bool pauseButtonClicked;
    public static bool isPaused;//Used to tell if game is paused.
    

    // Start is called before the first frame update
    void Start()
    { 
        isPaused = false;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        settingsMenu = GameObject.Find("SettingsMenu");
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape)){
        if(pauseButtonClicked == true){
            pauseButtonClicked = false;
            if(isPaused){
               ResumeGame();
               isPaused = false; 
            }
            else{
                PauseGame();
                isPaused = true;
            }
        }
    }

    public void PauseGameButton(){//Pause button pauses the game
        pauseButtonClicked = true;
    }
    public void PauseGame(){//Pause game and open menu
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame(){//Resume game and close menu
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenSettings(){//Open the settings menu 
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void QuitGame(){//Exit to Main Menu (Save too)
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToPause(){
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Mute(){//Either mute or lower volume. To be seen
        PlayerPrefs.SetFloat("musicVolume", 0);
        AudioListener.volume = 0;
    }
}
