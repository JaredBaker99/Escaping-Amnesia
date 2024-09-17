using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCards {
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class card : ScriptableObject {
        public string cardName;
        public int health;
        public int damage;
        public int energy;
        public string description;
        public string special;
        public Sprite cardSprite;
        // could make special enum but would only have 
        // specific types of specials, public enum specials 
    }
}
