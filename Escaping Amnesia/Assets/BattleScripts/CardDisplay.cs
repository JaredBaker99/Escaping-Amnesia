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
    // cards in hand info
    public Image cardImage; // card color
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text energyText;
    public TMP_Text discriptionText;
    public Image displayImage;

    public Image[] rareTypeImage;
    
    
    void Start()
    {
        UpdateCardDisplay();

    }
    public void UpdateCardDisplay()
    {
        nameText.text = cardData.cardName;
        discriptionText.text = cardData.description;
        healthText.text = cardData.maxHealth.ToString();
        damageText.text = cardData.damage.ToString();
        energyText.text = cardData.energy.ToString();
        displayImage.sprite = cardData.cardSprite;

        // for (int i = 0; i < rareTypeImage.Length; i++)
        // {
        //     if ( i == cardData.cardType.Count )
        //     {
        //         rareTypeImage[i].gameObject.SetActive(true);
        //     }
        // }
        

    }
}
