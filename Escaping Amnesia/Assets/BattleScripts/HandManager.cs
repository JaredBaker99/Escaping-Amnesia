using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using BattleCards;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager;
    // Assign card prefab in inspector
    public GameObject cardPrefab;

    // Root of the hand position
    public Transform handTransform;

    // How much our hand is spread out(mess with values)
    public float fanSpread = 7.5f;

    public float cardSpacing = 100f;

    public float verticalSpacing = 100f;

    public int maxHandSize = 7;

    // Hold a list of the card objects in our hand
    public List<GameObject> cardsInHand = new List<GameObject>();

    void Start()
    {
        
        
    }

    public void AddCardToHand(Card cardData)
    {
        //Instantiate the card - (gameobject, vector position, quaternion rotation, transform parent)
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);
        
        //set the cardData of the instantiated card
        newCard.GetComponent<CardDisplay>().cardData = cardData;
        
        newCard.GetComponent<CardDisplay>().UpdateCardDisplay();
        
        // newCard.GetComponent<OnFieldDisplay>().cardData = cardData;

        // newCard.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();

        UpdateHandVisuals();
    }

    void Update(){
        //UpdateHandVisuals();
    }
    
    public void UpdateHandVisuals(){
        int cardCount = cardsInHand.Count;
        // This stops the position error when there is one card in hand.
        
        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }
        
        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
            // set card positions
            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            float normalizedPosition = (2f * i / (cardCount - 1) - 1f); // normalize card position between -1,1
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
