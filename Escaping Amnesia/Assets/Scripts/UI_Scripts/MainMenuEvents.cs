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
    private VisualElement _mainMenu;
    private VisualElement _howToPlayMenu;
    private Button _startButton;
    private Button _howToPlayButton;
    private Button _quitButton;
    private Button _backButton;
    private VisualElement container;
    private AudioClip _MainMenuMusic;
    AudioManager audioManager;
    private void Awake()
    {
        //Grab UI Document & Root
        _document = GetComponent<UIDocument>();
        VisualElement _root = GetComponent<UIDocument>().rootVisualElement;
        Debug.Log(_root.name);
        //Grab Audio
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();//Grab Audio Manager
        //Set menus
        _mainMenu = _root.Q("MainMenuVT");
        _howToPlayMenu = _root.Q("HowToPlayVT");
        _mainMenu.Display(true);
        _howToPlayMenu.Display(false);
        

        //Grab buttons and set their clicks to function calls
        _startButton = _document.rootVisualElement.Q("StartButton") as Button;
        _startButton.RegisterCallback<ClickEvent>(StartClick);

        _howToPlayButton = _document.rootVisualElement.Q("HowToPlayButton") as Button;
        _howToPlayButton.RegisterCallback<ClickEvent>(HowToPlay);

        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.RegisterCallback<ClickEvent>(QuitClick);

        _backButton = _document.rootVisualElement.Q("BackButton") as Button;
        _backButton.RegisterCallback<ClickEvent>(BackClick);

        //Grab container and set opacity so fade to black works
        container = _document.rootVisualElement.Q("Container");
        container.style.opacity = 1;



    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(StartClick);
        _howToPlayButton.UnregisterCallback<ClickEvent>(HowToPlay);
        _quitButton.UnregisterCallback<ClickEvent>(QuitClick);
    }

    private void StartClick(ClickEvent evt)
    {//Event for the start button being clicked
        audioManager.PlaySFX(audioManager.StartClick);
        _startButton.style.opacity = 0;
        _howToPlayButton.style.opacity = 0;
        _quitButton.style.opacity = 0;

        StartCoroutine(DelayedTransition());
    }

    private void HowToPlay(ClickEvent hvt)
    {
        _mainMenu.Display(false);
        _howToPlayMenu.Display(true);
    }

    private void BackClick(ClickEvent bvt){
        _mainMenu.Display(true);
        _howToPlayMenu.Display(false);     
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

