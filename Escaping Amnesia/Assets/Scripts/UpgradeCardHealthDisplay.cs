using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BattleCards;
using System.Diagnostics;

public class UpgradeCardHealthDisplay : MonoBehaviour
{
    public Transform contentPanel; // The parent object for card buttons
    public GameObject Canvas;      // The canvas to toggle visibility
    public GameObject coinCount;   // Reference to the player's coin counter
    public GameObject UpgradeOptions; // Other UI elements for upgrade options

    private List<Card> playerDeckCards; // Holds the player's deck cards

    void Start()
    {
        coinCount = GameObject.Find("Player Coins");
        LoadPlayerDeckCards();
        DisplayCards();
    }

    void LoadPlayerDeckCards()
    {
        // Find the player deck and fetch the cards
        playerDeck playerDeckInstance = GameObject.Find("Player Deck").GetComponent<playerDeck>();
        if (playerDeckInstance != null)
        {
            playerDeckCards = playerDeckInstance.deck;
        }
        else
        {
            UnityEngine.Debug.LogError("Player Deck not found or playerDeck script is missing!");
            playerDeckCards = new List<Card>();
        }
    }

    void DisplayCards()
    {
        // Fetch current coin count
        int playerCoins = coinCount.GetComponent<playerCoinCounter>().currentCoinCount;

        // Display each card in the player's deck
        foreach (Card card in playerDeckCards)
        {
            // Create a new GameObject for the card's button
            GameObject textObject = new GameObject(card.cardName);
            textObject.transform.SetParent(contentPanel, false);

            // Set up TextMeshProUGUI for card details
            TextMeshProUGUI cardText = textObject.AddComponent<TextMeshProUGUI>();
            cardText.text = $"Name: {card.cardName}\nHealth: {card.maxHealth} Damage: {card.damage} Energy: {card.energy}\nUpgrade Cost: {card.maxHealth + 1}";
            cardText.fontSize = 24;
            cardText.alignment = TextAlignmentOptions.Center;
            cardText.enableWordWrapping = true;

            // Set up Button component
            Button button = textObject.AddComponent<Button>();
            button.transition = Selectable.Transition.ColorTint;

            // Determine button interactivity based on player's coins
            if (playerCoins >= (card.maxHealth + 1))
            {
                cardText.color = Color.black; // Player can afford the upgrade
                button.interactable = true;
            }
            else
            {
                cardText.color = Color.red;   // Not enough coins
                button.interactable = false;
            }

            // Set button colors
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = new Color(0.9f, 0.9f, 1, 1);
            colorBlock.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1);
            colorBlock.selectedColor = Color.white;
            colorBlock.disabledColor = new Color(0.5f, 0.5f, 0.5f, 1);
            button.colors = colorBlock;

            // Add an onClick listener for upgrading the card
            button.onClick.AddListener(() => UpgradeCard(card));
        }
    }

    void UpgradeCard(Card card)
    {
        // Check if the player can afford the upgrade
        int upgradeCost = card.maxHealth + 1;
        playerCoinCounter coinCounter = coinCount.GetComponent<playerCoinCounter>();
        if (coinCounter.currentCoinCount >= upgradeCost)
        {
            // Deduct coins and increase card's health
            coinCounter.currentCoinCount -= upgradeCost;
            card.maxHealth += 1;
            card.currentHealth += 1; // Optional: Update current health as well

            UnityEngine.Debug.Log($"Upgraded Card: {card.cardName}. New Max Health: {card.maxHealth}");

            Canvas.SetActive(false);
            UpgradeOptions.SetActive(false);
        }
        else
        {
            UnityEngine.Debug.Log("Not enough coins to upgrade this card.");
        }
    }
}
