using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private UIDocument _document;
    private Button _startButton;
    private Button _quitButton;
    private AudioClip _MainMenuMusic;
    private void Awake(){
        _document = GetComponent<UIDocument>();

        _startButton = _document.rootVisualElement.Q("StartButton") as Button;
        _startButton.RegisterCallback<ClickEvent>(StartClick);

        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.RegisterCallback<ClickEvent>(QuitClick);


    }

    private void OnDisable(){
        _startButton.UnregisterCallback<ClickEvent>(StartClick);
        _quitButton.UnregisterCallback<ClickEvent>(QuitClick);
    }

    private void StartClick(ClickEvent evt){//Event for the start button being clicked
        Debug.Log("You pressed the Start Button");
        SceneManager.LoadScene("SpawnRoom");
    }   

    private void QuitClick(ClickEvent evt){
        Application.Quit();
        Debug.Log("Quit Game");
    }
}

