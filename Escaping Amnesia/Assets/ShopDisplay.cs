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
    public GameObject cardShopCanvas; // Assign the card shop canvas in the Inspector
    private List<Card> cards;
    //private List<Card> playerDeck;

    void Start()
    {
        //playerDeck = new List<Card>();
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
        foreach (Card card in cards)
        {
            GameObject textObject = new GameObject(card.cardName);
            textObject.transform.SetParent(contentPanel, false);

            TextMeshProUGUI cardText = textObject.AddComponent<TextMeshProUGUI>();
            cardText.text = $"Name: {card.cardName}\nHealth: {card.maxHealth} Damage: {card.damage} Energy: {card.energy}";
            cardText.fontSize = 24;
            cardText.alignment = TextAlignmentOptions.Center;
            cardText.enableWordWrapping = true;

            Button button = textObject.AddComponent<Button>();
            button.transition = Selectable.Transition.ColorTint;

            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = new Color(0.9f, 0.9f, 1, 1);
            colorBlock.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1);
            colorBlock.selectedColor = Color.white;
            colorBlock.disabledColor = new Color(0.5f, 0.5f, 0.5f, 1);
            colorBlock.colorMultiplier = 1;
            button.colors = colorBlock;

            button.onClick.AddListener(() => OnCardSelected(card));
        }
    }

    void OnCardSelected(Card card)
    {
        //playerDeck.Add(card);
        UnityEngine.Debug.Log($"Added Card to Deck: {card.cardName}\nHealth: {card.currentHealth}/{card.maxHealth}\nDamage: {card.damage}\nEnergy: {card.energy}");
        //Debug.Log($"Current Deck Size: {playerDeck.Count}");

        // Hide the card shop canvas after selecting a card
        if (cardShopCanvas != null)
        {
            cardShopCanvas.SetActive(false);
        }
    }
}
