using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BattleCards;
using System.Diagnostics;

public class ShopDisplay : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject cardShopCanvas;
    private List<Card> cards;
    public GameObject coinCount;
    public GameObject UpgradeOptions;

    void Start()
    {
        coinCount = GameObject.Find("Player Coins");
        LoadCards();
        DisplayCards();
    }

    void LoadCards()
    {
        Card[] loadedCards = Resources.LoadAll<Card>("Cards");
        cards = new List<Card>(loadedCards);
    }

    void DisplayCards()
    {
        int playerCoins = coinCount.GetComponent<playerCoinCounter>().currentCoinCount;

        foreach (Card card in cards)
        {
            // Create a GameObject for each card button
            GameObject textObject = new GameObject(card.cardName);
            textObject.transform.SetParent(contentPanel, false);

            // Set up the TextMeshProUGUI component to display card details and cost
            TextMeshProUGUI cardText = textObject.AddComponent<TextMeshProUGUI>();
            cardText.text = $"Name: {card.cardName}\nHealth: {card.maxHealth} Damage: {card.damage} Energy: {card.energy}\nCost: {card.energy}";
            cardText.fontSize = 24;
            cardText.alignment = TextAlignmentOptions.Center;
            cardText.enableWordWrapping = true;

            // Set up the Button component
            Button button = textObject.AddComponent<Button>();
            button.transition = Selectable.Transition.ColorTint;

            // Check if the player has enough coins for the card
            if (playerCoins >= card.energy)
            {
                // Player can afford the card, set the color to default
                cardText.color = Color.black; // or whatever default color you want
                button.interactable = true;
            }
            else
            {
                // Player cannot afford the card, set the color to red and disable the button
                cardText.color = Color.red;
                button.interactable = false;
            }

            // Define button color settings
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = new Color(0.9f, 0.9f, 1, 1);
            colorBlock.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1);
            colorBlock.selectedColor = Color.white;
            colorBlock.disabledColor = new Color(0.5f, 0.5f, 0.5f, 1);
            button.colors = colorBlock;

            button.onClick.AddListener(() => OnCardSelected(card));
        }
    }

    void OnCardSelected(Card card)
    {
        UnityEngine.Debug.Log(card);
        if (coinCount.GetComponent<playerCoinCounter>().currentCoinCount >= card.energy)
        {
            coinCount.GetComponent<playerCoinCounter>().currentCoinCount -= card.energy;

            playerDeck playerDeckInstance = GameObject.Find("Player Deck").GetComponent<playerDeck>();
            if (playerDeckInstance != null)
            {
                playerDeckInstance.AddCardToDeck(card);
                //playerDeckInstance.DisplayDeck();
            }
            else
            {
                UnityEngine.Debug.LogError("Player Deck instance not found!");
            }

            UpgradeOptions.SetActive(false);
            if (cardShopCanvas != null)
            {
                cardShopCanvas.SetActive(false);
            }
        }
        else
        {
            UnityEngine.Debug.Log("Not enough coins to purchase this card.");
        }
    }


}
