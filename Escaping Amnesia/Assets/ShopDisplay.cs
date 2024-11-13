using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BattleCards;

public class ShopDisplay : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject cardShopCanvas; // Assign the card shop canvas in the Inspector
    private List<Card> cards;
    public GameObject coinCount;

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

            // Add an onClick listener to check if the player can buy the card
            button.onClick.AddListener(() => OnCardSelected(card));
        }
    }

    void OnCardSelected(Card card)
    {
        // Check if the player has enough coins to buy the card
        if (coinCount.GetComponent<playerCoinCounter>().currentCoinCount >= card.energy)
        {
            // Deduct card cost from player's coins
            coinCount.GetComponent<playerCoinCounter>().currentCoinCount -= card.energy;

            // Log the purchase and hide the card shop canvas
            UnityEngine.Debug.Log($"Added Card to Deck: {card.cardName} for {card.energy} coins");

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
