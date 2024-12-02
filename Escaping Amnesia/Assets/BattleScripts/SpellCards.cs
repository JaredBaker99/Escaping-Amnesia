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

    public List<Target> target;

    public int howManyTargets;

    //  How much the effect will change #
    public int attributeChangeAmount;

    // Spell will have
    // NAME
    // DISCRIPTION
    // ENERGY 

    public enum AttributeTarget
    {
        Attack,
        Health,
        Energy,
        Draw
    }

    public enum Target
    {
        Player,
        Enemy,
        AllPlayer,
        AllEnemy
    }

    public int WhatTarget(SpellCards SpellData)
    {

        if (SpellData.target[0] == Target.Player)
        {
            Debug.Log("we are returning 0 in Target");
            return 0;
        }
        if (SpellData.target[0] == Target.Enemy)
        {
            Debug.Log("we are returning 1 in Target");
            return 1;
        }
        if (SpellData.target[0] == Target.AllPlayer)
        {
            Debug.Log("we are returning 2 in Target");
            return 2;
        }
        if (SpellData.target[0] == Target.AllEnemy)
        {
            Debug.Log("we are returning 3 in Target");
            return 3;
        }
        else return -1;
    }

    public int WhatAttribute(SpellCards SpellData)
    {

        if (SpellData.attributeTarget[0] == AttributeTarget.Attack)
        {
            Debug.Log("we are returning 0 in attribute target");
            return 0;
        }
        if (SpellData.attributeTarget[0] == AttributeTarget.Health)
        {
            Debug.Log("we are returning 1 in attribute target");
            return 1;
        }
        if (SpellData.attributeTarget[0] == AttributeTarget.Energy)
        {
            Debug.Log("we are returning 2 in attribute target");
            return 2;
        }
        if (SpellData.attributeTarget[0] == AttributeTarget.Draw)
        {
            Debug.Log("we are returning 3 in attribute target");
            return 3;
        }
        else return -1;
    }
}
