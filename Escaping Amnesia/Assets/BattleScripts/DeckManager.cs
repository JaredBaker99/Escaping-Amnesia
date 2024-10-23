using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCards;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHandSize = 4;

    public int maxHandSize = 7;

    public int currentHandSize;

    private HandManager handManager;

    private DrawPileManager drawPileManager;

    private bool startBattleRun = true;

    void Start()
    {
        // Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");
        for (int i = 0; i < cards.Length; i++)
        {
        //     Card cardDataInstance;
        //     cardDataInstance = ScriptableObject.CreateInstance<Card>(); 

            cards[i].currentHealth = cards[i].maxHealth;
        //     cardDataInstance = cards[i];
                Debug.Log("CARD HP IS " + cards[i].currentHealth);
        //     allCards.Add(cardDataInstance);
        }
        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);
    }

    void Awake()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindObjectOfType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindObjectOfType<HandManager>();
        }
    }

    void Update()
    {
        if(startBattleRun)
        {
            BattleSetup();
        }
    }

    public void BattleSetup()
    {
        // sets max hand size
        handManager.BattleSetup(maxHandSize);
        // put all of our cards in a pile and shuffles
        drawPileManager.MakeDrawPile(allCards);
        // draw cards from our DrawPile 4 times 
        drawPileManager.BattleSetup(startingHandSize, maxHandSize);
        // turns start battle run off cuz this is at START
        startBattleRun = false;
    }
}
