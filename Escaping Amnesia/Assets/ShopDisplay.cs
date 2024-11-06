using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using BattleCards; // Include the BattleCards namespace

public class ShopDisplay : MonoBehaviour
{
    public Transform contentPanel; // Assign the "Content" object from the Scroll View in the Inspector
    private List<Card> cards;

    void Start()
    {
        LoadCards();
        DisplayCards();
    }

    void LoadCards()
    {
        // Load all Card ScriptableObjects from the Resources/Cards folder
        Card[] loadedCards = Resources.LoadAll<Card>("Cards");
        cards = new List<Card>(loadedCards);
    }

    void DisplayCards()
    {
        foreach (Card card in cards)
        {
            // Create a new GameObject for the text
            GameObject textObject = new GameObject("CardText");
            textObject.transform.SetParent(contentPanel, false); // Add to the content panel and maintain local scale

            // Add a TextMeshProUGUI component to the GameObject
            TextMeshProUGUI cardText = textObject.AddComponent<TextMeshProUGUI>();

            // Configure the text properties
            cardText.text = $"Name: {card.cardName}\nHealth: {card.maxHealth} Damage: {card.damage} Energy: {card.energy}";
            cardText.fontSize = 24; // Adjust font size as needed
            cardText.alignment = TextAlignmentOptions.Left; // Align text to the left
            cardText.enableWordWrapping = true; // Enable word wrapping if needed
        }
    }
}
