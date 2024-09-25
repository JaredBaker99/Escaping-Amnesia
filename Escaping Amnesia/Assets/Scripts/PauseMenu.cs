using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;//Used to tell if game is paused.

    // Start is called before the first frame update
    void Start()
    { 
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
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

    }
    public void QuitGame(){//Exit to Main Menu (Save too)
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
