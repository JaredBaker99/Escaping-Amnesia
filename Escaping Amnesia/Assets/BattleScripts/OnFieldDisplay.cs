using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BattleCards;

public class OnFieldDisplay : MonoBehaviour
{
    public Card cardData;

    public Image cardImage; // card color

    public Image fieldDisplayImage;
    public TMP_Text fieldNameText;
    public TMP_Text fieldHealthText;
    public TMP_Text fieldDamageText;   
    // Start is called before the first frame update
    void Start()
    {
       UpdateFieldDisplay();
    }
    
public void UpdateFieldDisplay()
    {
        fieldNameText.text = cardData.cardName;
        fieldHealthText.text = cardData.currentHealth.ToString();
        fieldDamageText.text =  cardData.damage.ToString();
        fieldDisplayImage.sprite = cardData.cardSprite;
    }
}
