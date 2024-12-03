using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCards;

public class GridManager : MonoBehaviour
{
    public int width = 4;

    public int height = 2;

    public GameObject gridCellPrefab;

    public List<GameObject> gridObjects = new List<GameObject>();

    public GameObject[,] gridCells;

    public OnFieldDisplay onFieldDisplay;

    public DrawPileManager drawPileManager;

    public HandManager handManager;

    public BattleHUDManager battleHUDManager;

    void Start()
    {
        CreateGrid();
        // this is how you change the scale on the grid
        transform.localScale = new Vector3(1.35f,2.35f,1f);
        // this is how to change the position of the scale (x,y,z)
        transform.localPosition = new Vector3(0f,.50f,0f);
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        Vector2 centerOffset = new Vector2(width/2.0f - 0.5f, height/2.0f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPosition = new Vector2(x,y);
                Vector2 spawnPosition = gridPosition - centerOffset;

                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
    
                gridCell.transform.SetParent(transform);
                

                gridCell.GetComponent<GridCell>().gridIndex = gridPosition;


                gridCells[x,y] = gridCell;
            }
        }
    }

    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition, Card data)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();

            if (cell.cellFull) return false;
            else
            {
                Debug.Log("Inside of AddObjectToGrid ");
                Debug.Log(obj);
                GameObject newObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
                Debug.Log("afasfafa ");
                newObj.GetComponent<OnFieldDisplay>().cardData = data;
                newObj.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                newObj.transform.SetParent(transform);
                gridObjects.Add(newObj);
                cell.objectInCell = newObj;
                cell.cellFull = true;
                return true;
            }  
        }
        else return false;
    }
    public bool AddSpellToGrid(SpellCards data, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();
            // special case for the DRAW 2 card
            int attribute = data.WhatAttribute(data);
            int target = data.WhatTarget(data);

            if (attribute == 3 && target == 0 & data.howManyTargets == 1)
            {
                // this is affecting draw, for the player, 1 target (ourself)
                Debug.Log("special case #1");
                for ( int i = 0; i < data.attributeChangeAmount; i++)
                {
                    //draw 2 cards
                    drawPileManager = FindObjectOfType<DrawPileManager>();
                    handManager = FindObjectOfType<HandManager>();
                    drawPileManager.DrawCard(handManager);
                }
                return true;
            }

            // second special case sacraficing energy for hp
            if (attribute == 2 && target == 0 & data.howManyTargets == 1)
            {
                // this is affecting energy, for the player, 1 target (ourself)
                Debug.Log("special case #2");
                battleHUDManager = FindObjectOfType<BattleHUDManager>();
                Debug.Log(battleHUDManager);
                //battleHUDManager.gameManager.AddEnergy(); // + 2
                battleHUDManager.UpdateHUDManager();
                Debug.Log(battleHUDManager.gameManager.currentEnergy);
                battleHUDManager.playerhealthTracker.currentPlayerHealth -= 2;
                return true;
            }

            if (cell.cellFull == false)
            {
              return false;  
            } else
            {
                Debug.Log("Inside of AddSpellToGrid ");
                
                // int attribute = data.WhatAttribute(data);
                // int target = data.WhatTarget(data);
                
                Debug.Log("What is the value of attribute: " + attribute);
                Debug.Log("What is the value of target: " + target);
                // The CHEETSHEET:
                // if attribute is: 0 = attack. 1 = hp, 2 = energy, 3 = draw
                // If target is : 0 = player, 1 = enemy, 2 = all player, 3 = all enemy
                // if howManyTargets is : 1 = chosen card/nothing, 2 = ???, 3 = ajacent, 4 = all. 

                if (attribute == 0 && target == 0 & data.howManyTargets == 1)
                {
                    // this is affecting attack, targeting player, 1 card
                    Debug.Log("we are in first addpelltogrid");
                    cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.damage += data.attributeChangeAmount;
                    cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                    return true;
                    
                } else if (attribute == 1 && target == 0 & data.howManyTargets == 1)
                {
                    // this is affecting HP, target player, 1 card 
                    Debug.Log("we are in second addpelltogrid");
                    cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                    cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                    return true;
                } else if (attribute == 0 && target == 2 & data.howManyTargets == 4)
                {
                    Debug.Log("we are in third addpelltogrid");
                    //this is affecting attack, targeting all player cards, 4 cards
                    for (int i = 0; i < 4; i++)
                    {
                        cell = gridCells[(int) i, (int) 0].GetComponent<GridCell>();
                        if (cell.cellFull == true)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.damage += data.attributeChangeAmount;
                            cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        }
                    }
                    return true;

                } else if (attribute == 1 && target == 2 & data.howManyTargets == 3)
                { 
                    // we are affecting hp, all player, ajacently
                    // if it's in slot 0, affect only slot 0 and 1
                    Debug.Log("we are in fourthd addpelltogrid");
                    if (cell.gridIndex.x == 0)
                    {
                        Debug.Log("Fourth. x == 0");
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        cell = gridCells[(int) 1, (int) 0].GetComponent<GridCell>();
                        if (cell.cellFull == true)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                            cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        }
                        return true;
                    }
                    // if it's in slot 1, affect only slot 0, 1, 2
                    if (cell.gridIndex.x == 1)
                    {
                        Debug.Log("Fourth. x == 1");
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        cell = gridCells[(int) 0, (int) 0].GetComponent<GridCell>();
                        if (cell.cellFull == true)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                            cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        }
                        cell = gridCells[(int) 2, (int) 0].GetComponent<GridCell>();
                        if (cell.cellFull == true)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                            cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        }
                        return true;
                    }
                    // if it's in slot 2, affect only slot 1, 2, 3
                    if (cell.gridIndex.x == 2)
                    {
                        Debug.Log("Fourth. x == 2");
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        cell = gridCells[(int) 1, (int) 0].GetComponent<GridCell>();
                        if (cell.cellFull == true)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                            cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        }
                        cell = gridCells[(int) 3, (int) 0].GetComponent<GridCell>();
                        if (cell.cellFull == true)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                            cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        }
                        return true;
                    }
                    // if it's in slot 3 , affect only slot 2, 3
                    if (cell.gridIndex.x == 3)
                    {
                        Debug.Log("Fourth. x == 3");
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        cell = gridCells[(int) 2, (int) 0].GetComponent<GridCell>();
                        if (cell.cellFull == true)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth += data.attributeChangeAmount;
                            cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        }
                        return true;
                    }
                } else if (attribute == 1 && target == 1 && data.howManyTargets == 1)
                {
                    // we are targeting HP, for the enenemy, 1 chosen target
                    Debug.Log("we are in SIXTH addpelltogrid");
                    cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth -= data.attributeChangeAmount;
                    // the card's health being reduce can not kill.
                    if (cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth <= 0)
                    {
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth = 1;
                    }
                    cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                    return true;
                } else if (attribute == 1 && target == 3 && data.howManyTargets == 4)
                {
                    // we are targeting HP, for all the enemies, and 4 are chosen
                    Debug.Log("we are in SEVENTH addpelltogrid");
                    int count = 0;
                    cell = gridCells[(int) 0, (int) 1].GetComponent<GridCell>();
                    if (cell.cellFull == true)
                    {
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth -= data.attributeChangeAmount;
                        if (cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth <= 0)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth = 1;
                        }
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        count++;
                    }
                    cell = gridCells[(int) 1, (int) 1].GetComponent<GridCell>();
                    if (cell.cellFull == true)
                    {
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth -= data.attributeChangeAmount;
                        if (cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth <= 0)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth = 1;
                        }
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        count++;
                    }
                    cell = gridCells[(int) 2, (int) 1].GetComponent<GridCell>();
                    if (cell.cellFull == true)
                    {
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth -= data.attributeChangeAmount;
                        if (cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth <= 0)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth = 1;
                        }
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        count++;
                    }
                    cell = gridCells[(int) 3, (int) 1].GetComponent<GridCell>();
                    if (cell.cellFull == true)
                    {
                        cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth -= data.attributeChangeAmount;
                        if (cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth <= 0)
                        {
                            cell.objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth = 1;
                        }
                        cell.objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                        count++;
                    }
                    if (count == 0)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
        } else
        {
            return false;
        }
    }
}
