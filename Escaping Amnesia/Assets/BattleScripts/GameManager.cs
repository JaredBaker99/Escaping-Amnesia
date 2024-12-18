using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    // public int playerCurrentHealth;
    //private int playerHealth;

    // private int playerGold;

    // private int difficulty = 5;
    public int playerEnergyStart = 3;

    public int currentEnergy;

    public OptionsManager OptionsManager {get; private set;}

    public AudioManager AudioManager {get; private set;}

    public DeckManager DeckManager {get; private set;}

    public bool PlayingCard = false;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
            InitializeManager();
        }
        else if (Instance != this)
        {
            // Destroy(gameObject);
        }
    }

    private void InitializeManager()
    {
        //PlayerHealth = 10;
        // PlayerGold = 0;
        currentEnergy = 3;

        // OptionsManager = GetComponentInChildren<OptionsManager>();
        // AudioManager = GetComponentInChildren<AudioManager>();
        // DeckManager = GetComponentInChildren<DeckManager>();

        // if (OptionsManager == null)
        // {
        //     GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
        //     if (prefab == null)
        //     {
        //         Debug.Log($"OptionsManager prefab not found");
        //     }
        //     else
        //     {
        //         Instantiate(prefab, transform.position, Quaternion.identity, transform);
        //         OptionsManager = GetComponentInChildren<OptionsManager>();
        //     }
        // }

        // if (AudioManager == null)
        // {
        //     GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
        //     if (prefab == null)
        //     {
        //         Debug.Log($"AudioManager prefab not found");
        //     }
        //     else
        //     {
        //         Instantiate(prefab, transform.position, Quaternion.identity, transform);
        //         AudioManager = GetComponentInChildren<AudioManager>();
        //     }
        // }
        
        // if (DeckManager == null)
        // {
        //     GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
        //     if (prefab == null)
        //     {
        //         Debug.Log($"DeckManager prefab not found");
        //     }
        //     else
        //     {
        //         Instantiate(prefab, transform.position, Quaternion.identity, transform);
        //         DeckManager = GetComponentInChildren<DeckManager>();
        //     }
        // }
    }

    public void AddEnergy()
    {
        currentEnergy = currentEnergy + 2;
    }

    // public int PlayerHealth
    // {
    //     get {return playerHealth;}
    //     set {playerHealth = value;}
    // }

    // public int PlayerGold
    // {
    //     get {return playerGold;}
    //     set {playerGold = value;}
    // }

    // public int Difficulty
    // {
    //     get {return difficulty;}
    //     set {difficulty = value;}
    // }

}
