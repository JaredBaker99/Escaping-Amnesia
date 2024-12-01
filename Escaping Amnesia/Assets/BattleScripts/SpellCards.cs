using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCards;

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Card/Spell")]
public class SpellCards : Card
{
    //public SpellType spellType;

    // what this will effect from the card HP or dmg
    public List<AttributeTarget> attributeTarget;

    //  How much the effect will change #
    public int attributeChangeAmount;

    public enum AttributeTarget
    {
        Attack,
        Health
    }
    // Spell will have
    // NAME
    // DISCRIPTION
    // ENERGY 


    public int WhatAttribute(SpellCards SpellData)
    {

        if (SpellData.attributeTarget[0] == AttributeTarget.Attack)
        {
            Debug.Log("we are returning 0 in what attribute");
            return 0;
        }
        if (SpellData.attributeTarget[0] == AttributeTarget.Health)
        {
            Debug.Log("we are returning 1 in what attribute");
            return 1;
        }
        else return -1;
    }
}
