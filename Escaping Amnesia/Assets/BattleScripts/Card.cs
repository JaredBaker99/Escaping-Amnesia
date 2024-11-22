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
        // could make special enum but would only have 
        // specific types of specials, public enum specials

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
