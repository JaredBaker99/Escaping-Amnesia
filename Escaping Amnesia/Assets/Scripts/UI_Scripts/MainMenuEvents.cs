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

        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.RegisterCallback<ClickEvent>(QuitClick);

        container = _document.rootVisualElement.Q("Container");
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(StartClick);
        _quitButton.UnregisterCallback<ClickEvent>(QuitClick);
    }

    private void StartClick(ClickEvent evt)
    {//Event for the start button being clicked
        Debug.Log("You pressed the Start Button");
        audioManager.PlaySFX(audioManager.StartClick);


        StartCoroutine(DelayedTransition());

        Invoke("DelayedLoad", 3f);
    }

    private IEnumerator DelayedTransition()
    {
        while (container.style.opacity.value > 0)
        {
            float opacity = container.style.opacity.value - .1f;
            Mathf.Clamp(opacity, 0, 1);
            container.style.opacity = opacity;
            yield return new WaitForSeconds(.2f);
        }
    }

    private void QuitClick(ClickEvent evt)
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    private void DelayedLoad()
    {
        SceneManager.LoadScene("SpawnRoom");
    }
}

