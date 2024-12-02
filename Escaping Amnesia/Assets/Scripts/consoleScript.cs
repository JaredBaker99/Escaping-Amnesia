using UnityEngine;
using TMPro;
using BattleCards;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class GameConsole : MonoBehaviour
{
    public static GameConsole Instance { get; private set; }

    public TMP_InputField inputField; // Assign in Inspector
    public TMP_Text consoleOutput;   // Assign in Inspector

    public GameObject playerHealth;
    public GameObject playerCoin;
    public playerDeck playerDeckInstance;
    private List<Card> allCards;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Make this object persist across scenes
    }

    private void Start()
    {
        playerHealth = GameObject.Find("Player Health");
        playerCoin = GameObject.Find("Player Coins");
        playerDeckInstance = GameObject.Find("Player Deck").GetComponent<playerDeck>();
        inputField.onEndEdit.AddListener(HandleInput);
    }

    private void HandleInput(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput)) return;

        AppendToConsole("> " + userInput);
        ExecuteCommand(userInput);
        inputField.text = "";
        inputField.ActivateInputField();
    }

    private void ExecuteCommand(string command)
    {
        string cmd = command.ToLower();
        switch (cmd)
        {
            case "help":
                AppendToConsole("** Commands ** \n\n" +
                    "\tclear\n" +
                    "\tfullhealth - Bring player health to full\n" +
                    "\tallcards - Load all cards into player deck\n" +
                    "\taddcoin - Increase player coins by 10\n" +
                    "\tstart - Return to the main menu\n" +
                    "\tquit - Close game");
                break;
            case "clear":
                consoleOutput.text = "";
                break;
            case "fullhealth":
                playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth = playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth;
                AppendToConsole("Increase Player health to full");
                break;
            case "allcards":
                loadAll();
                playerDeckInstance.deck = allCards;
                AppendToConsole("All cards loaded into Player deck");
                break;
            case "addcoin":
                playerCoin.GetComponent<playerCoinCounter>().currentCoinCount += 10;
                AppendToConsole("Increased player coins by 10");
                break;
            case "start":
                SceneManager.LoadScene("MainMenu");
                AppendToConsole("Returning to the main menu");
                break;
            case "quit":
                consoleOutput.text = "";
                UnityEngine.Application.Quit();
                break;
            case "`":
                break;
            default:
                AppendToConsole("Unknown command: " + cmd);
                AppendToConsole("** Commands ** \n\n" +
                    "\thelp\n" +
                    "\tclear\n" +
                    "\tfullhealth - Bring player health to full\n" +
                    "\tallcards - Load all cards into player deck\n" +
                    "\taddcoin - Increase player coins by 10\n" +
                    "\tstart - Return to the main menu\n" +
                    "\tquit - Close game");
                break;
        }
    }

    private void loadAll() 
    {
        Card[] loadedCards = Resources.LoadAll<Card>("Cards");
        allCards = new List<Card>(loadedCards);
    }

    private void AppendToConsole(string text)
    {
        consoleOutput.text += text + "\n";
    }
}