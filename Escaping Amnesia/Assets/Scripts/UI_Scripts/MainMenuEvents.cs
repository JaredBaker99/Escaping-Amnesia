using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{
    private UIDocument _document;
    private Button _startButton;
    // private Button _optionsButton;
    private Button _quitButton;
    private VisualElement container;
    private AudioClip _MainMenuMusic;
    AudioManager audioManager;
    private void Awake()
    {

        _document = GetComponent<UIDocument>();//Grab UI Document
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();//Grab Audio Manager

        _startButton = _document.rootVisualElement.Q("StartButton") as Button;
        _startButton.RegisterCallback<ClickEvent>(StartClick);

        // _optionsButton = _document.rootVisualElement.Q("OptionButton") as Button;
        // _optionsButton.RegisterCallback<ClickEvent>(OptionClick);

        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.RegisterCallback<ClickEvent>(QuitClick);

        container = _document.rootVisualElement.Q("Container");
        container.style.opacity = 1;
        
        audioManager.MainMenuMusic.

    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(StartClick);
        // _optionsButton.UnregisterCallback<ClickEvent>(OptionClick);
        _quitButton.UnregisterCallback<ClickEvent>(QuitClick);
    }

    private void StartClick(ClickEvent evt)
    {//Event for the start button being clicked
        audioManager.PlaySFX(audioManager.StartClick);
        _startButton.style.opacity = 0;
        // _optionsButton.style.opacity = 0;
        _quitButton.style.opacity = 0;

        StartCoroutine(DelayedTransition());


    }


    private IEnumerator DelayedTransition()
    {
        while (container.style.opacity.value > 0)
        {
            float opacity = container.style.opacity.value - .005f;
            Mathf.Clamp(opacity, 0, 1);
            container.style.opacity = opacity;
            yield return new WaitForSeconds(.01f);
        }
        Invoke("DelayedLoad", .5f);
    }

    private void OptionClick(ClickEvent evt)
    {
        Debug.Log("Options Button Clicked");
    }

    private void QuitClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void DelayedLoad()
    {
        SceneManager.LoadScene("SpawnRoom");
    }
}

