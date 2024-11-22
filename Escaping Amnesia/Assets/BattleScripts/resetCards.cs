using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace BattleCards
{
    public class resetCards : MonoBehaviour
    {
        [SerializeField]
        private List<Card> allCards; // Drag and drop all Card ScriptableObjects in the Inspector.

        private void Start()
        {
            LoadCards();
            ResetAllCards();
        }

        void LoadCards()
        {
            Card[] loadedCards = Resources.LoadAll<Card>("Cards");
            allCards = new List<Card>(loadedCards);
        }

        // Call this method to reset all cards to their original state
        public void ResetAllCards()
        {
            foreach (Card card in allCards)
            {
                if (card != null)
                {
                    card.resetOriginal();
                    UnityEngine.Debug.Log($"Card {card.cardName} has been reset.");
                }
            }
        }
    }
}
