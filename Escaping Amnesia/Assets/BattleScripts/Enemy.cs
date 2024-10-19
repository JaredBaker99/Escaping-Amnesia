using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCards;

public class Enemy : MonoBehaviour
{

    private int enemyHealth;

    private int goldDrop = 10;

    private int difficulty;
    
    public int startingEnergy = 3;

    public int enemyCurrentEnergy;

    public GridManager gridManager;

    public List<Card> enemyCards = new List<Card>();

    void Start()
    {
        // Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("EnemyCards");

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].currentHealth = cards[i].maxHealth;
        }
        // Add the loaded cards to the allCards list
        enemyCards.AddRange(cards);

        enemyCurrentEnergy = startingEnergy;

        enemyHealth = 7;

        gridManager = GetComponent<GridManager>();
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

    //this hurts to look at. im sorry
    public void PlayCard()
    {   

        Debug.Log("we are in enemy.PlayCard()");
        bool placedCard = false;
        gridManager = FindObjectOfType<GridManager>();
        //AI has cards to play
        if (0 <= enemyCards.Count)
        {
            for (int x = 0; x < gridManager.width; x++)
            {  
                // check to see if a card is in the x,1 grid and if we played a card yet
                if (gridManager.gridCells[x,1].GetComponent<GridCell>().cellFull == false && placedCard == false)
                {
                    for (int i = 0; i < enemyCards.Count; i++)
                    {
                        Debug.Log("enemyCards.count on iteration i: " + i);
                        Debug.Log(enemyCards.Count);
                        // do we have the energy to play ith card.
                        int energyLeft = enemyCurrentEnergy - enemyCards[i].energy;
                        // the card is legal to play for the AI 
                        if(energyLeft >= 0)
                        {
                            placedCard = gridManager.AddObjectToGrid(enemyCards[i].prefab, gridManager.gridCells[x,1].GetComponent<GridCell>().gridIndex, enemyCards[i]);
                            if (placedCard == true)
                            {
                                enemyCurrentEnergy = energyLeft;
                                enemyCards.RemoveAt(i);
                            }
                            else
                            {
                                Debug.Log("something isn't right, how did we get in here?");
                            }
                        }
                    }
                }
            }
        } else
        {
            //Enemy is out of cards
        } 
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
