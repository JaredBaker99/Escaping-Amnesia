using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCards;

public class Enemy : MonoBehaviour
{

    private int enemyHealth;

    public int enemyStartingHealth = 10;

    private int goldDrop = 10;

    private int difficulty;
    
    public int startingEnergy = 3;

    public int enemyCurrentEnergy;

    public List<Card> enemyCards = new List<Card>();

    public int usableCards;

    public GameObject toBattleArea ;
    public string enemy ;

    void Start()
    {
        toBattleArea = GameObject.Find("To Battle");
        enemy = toBattleArea.GetComponent<ToBattleArea>().enemyName ;
        // Load all card assets from the Resources folder
        Card[] cards ; // = Resources.LoadAll<Card>("EnemyCards");
        if(enemy == "008") {
            cards = Resources.LoadAll<Card>("008EnemyCards");
        }
        else if (enemy == "009") {
            cards = Resources.LoadAll<Card>("009EnemyCards");
        }
        else if (enemy == "010") {
            cards = Resources.LoadAll<Card>("010EnemyCards");
        }
        else if(enemy == "boss") {
            cards = Resources.LoadAll<Card>("BossCards");
        }
        else {
            cards = Resources.LoadAll<Card>("EnemyCards");
        }

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].currentHealth = cards[i].maxHealth;
        }
        // Add the loaded cards to the allCards list
        enemyCards.AddRange(cards);

        enemyCurrentEnergy = startingEnergy;

        enemyHealth = enemyStartingHealth;

        usableCards = 4;

    }

    // Update is called once per frame
    void Update()
    {
        //UpdateEnemyDisplay();    
    }

    public void  UpdateEnemyDisplay()
    {
        // OnfieldDisplay();
        // make the back of cards and put it on the enemy side
    }

    //This is what the BattleSystem.EnemyTurn calls... and all of this is in the EnemyChoice file
    public void PlayCard()
    {   
        // The enemy draws a card here.... will have to do a usableCards-- inside the enemy choice file!!!
        //usableCards++;
        // enemyChoice.CheckHealthValue(enemyHealth, enemyStartingHealth);
        // enemyChoice.CalculateFutureDecision();
    }

    public int EnemyHealth
    {
        get {return enemyHealth;}
        set {enemyHealth = value;}
    }

    public int GoldDrop
    {
        get {return goldDrop;}
        set {goldDrop = value;}
    }

    public int Difficulty
    {
        get {return difficulty;}
        set {difficulty = value;}
    }

}
