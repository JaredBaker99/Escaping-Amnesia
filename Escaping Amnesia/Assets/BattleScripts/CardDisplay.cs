using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BattleCards;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    // where we will assign card data itll represent for later

    public Image cardImage; // card color

    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text energyText;

    /*
    public Image[] tierImages;
    // If we want to change the card to a tier or type

    private Color[] typeColors = {
        // Classic example of tier/rarity
        Color.grey 
        Color.green
        Color.blue
        Color.purple
        Color.orange
    } 
    */

    void Start()
    {
        UpdateCardDisplay();
    }
    public void UpdateCardDisplay()
    {
        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = cardData.damage.ToString();
        energyText.text = cardData.energy.ToString();
    }
}
