using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCards;
using System.Diagnostics;

public class playerDeck : MonoBehaviour
{
    // List to store the player's selected cards
    public List<Card> deck = new List<Card>();

    // Debugging to confirm the script's initialization
    void Start()
    {
        if (deck == null)
        {
            deck = new List<Card>();
            UnityEngine.Debug.Log("Deck was null, initialized in Start.");
        }
        else
        {
            UnityEngine.Debug.Log("Deck initialized successfully.");
        }
    }

    // Method to add a card to the player's deck
    public void AddCardToDeck(Card card)
    {
        if (card != null)
        {
            deck.Add(card);
            UnityEngine.Debug.Log($"Card '{card.cardName}' added to the deck. Total cards: {deck.Count}");
        }
        else
        {
            UnityEngine.Debug.LogError("Attempted to add a null card to the deck!");
        }
    }

    // Optional: Debug all cards in the deck
    public void DisplayDeck()
    {
        UnityEngine.Debug.Log("Current deck:");
        foreach (Card c in deck)
        {
            UnityEngine.Debug.Log($"- {c.cardName}");
        }
    }
}
