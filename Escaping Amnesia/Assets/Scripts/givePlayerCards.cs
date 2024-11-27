using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using BattleCards;

public class givePlayerCards : MonoBehaviour
{
    [SerializeField]
    private List<Card> allCards; // Drag and drop all Card ScriptableObjects in the Inspector.
    public List<Card> selectedCards;

    private void Start()
    {
        loadCards();
        pickCards();

        playerDeck playerDeckInstance = GameObject.Find("Player Deck").GetComponent<playerDeck>();
        playerDeckInstance.deck = selectedCards;
    }

    void loadCards()
    {
        Card[] loadedCards = Resources.LoadAll<Card>("Cards");
        allCards = new List<Card>(loadedCards);
    }

    void pickCards()
    {
        if (allCards == null || allCards.Count < 10)
        {
            //Debug.LogError("Not enough cards to pick from!");
            return;
        }

        selectedCards = new List<Card>();
        List<Card> tempCards = new List<Card>(allCards); // Create a temporary list to avoid modifying the original list.

        for (int i = 0; i < 10; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, tempCards.Count); // Pick a random index.
            selectedCards.Add(tempCards[randomIndex]); // Add the card to selectedCards.
            tempCards.RemoveAt(randomIndex); // Remove the selected card to avoid duplicates.
        }
    }

}
