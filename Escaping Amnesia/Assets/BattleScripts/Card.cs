using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace BattleCards 
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public int maxHealth;
        public int currentHealth;
        public int originalHealth;
        public int damage;
        public int originalDamage;
        public int energy;
        public string description;
        public string special;
        public Sprite cardSprite;
        public GameObject prefab;

        public CardType cardType;

        public enum CardType
        {
            Common,
            Rare,
            Legendary
        }

        public void TakeDamage(int damage)
        {

            currentHealth -= damage;

        }

        public void resetOriginal()
        {
            maxHealth = originalHealth;
            currentHealth = originalHealth;
            damage = originalDamage;
        }
    }
}
