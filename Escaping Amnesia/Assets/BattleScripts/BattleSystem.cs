using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// states start. playerturn. enemy turn. WON. LOSE
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public BattleHUDManager battleHUDManager;

    public TMP_Text dialougeText;

    public GridManager gridManager;

    public HandManager handmanager;

    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {  
        Debug.Log("BATTLESYSTEM START");
        state = BattleState.START;
        // technically the DeckManager starts the deck for player except energy/hp
        // SetUpDeck();
        // gridManager = GetComponent<GridManager>();
        SetUpHUD();
        PlayerTurn();
    }

    public void SetUpHUD()
    {
        battleHUDManager.StartHUD();
        Debug.Log("In settuphud");
        state = BattleState.PLAYERTURN;
        // Debug.Log(gridManager.gridCells[0,0].GetComponent<GridCell>().cellFull);
        // Debug.Log(gridManager.gridCells[1,0].GetComponent<GridCell>().cellFull);
    }

    public void PlayerAttack()
    {
        Debug.Log("we are in player attack()");
        gridManager = FindObjectOfType<GridManager>();
        for (int x = 0; x < gridManager.width; x++)
        {  
            //we have a card in our cell
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {   
                // they have a card in the opposite cell y = 1
                if (gridManager.gridCells[x,1].GetComponent<GridCell>().cellFull == true)
                {
                    int difference = gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth - gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
                    // only does damage to the card
                    if (difference > 0)
                    {
                        // gridManager.gridCells[x,1].objectInCell.cardData.CardHealth - difference;
                        gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth = gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth - gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
                        gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                    }
                    // Does enough damage to destroy the card
                    if (difference == 0)
                    {
                        Destroy(gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell);
                        gridManager.gridCells[x,1].GetComponent<GridCell>().cellFull = false;
                    }
                    // Does enough damage to destroy the card and the opponent
                    if (difference < 0)
                    {
                        Destroy(gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell);
                        gridManager.gridCells[x,1].GetComponent<GridCell>().cellFull = false;
                        battleHUDManager.enemy.EnemyHealth = battleHUDManager.enemy.EnemyHealth + difference;
                    }
                }
                else // they don't have a card in their cell
                {
                    // enemy hp = enemy hp - card damage
                    battleHUDManager.enemy.EnemyHealth = battleHUDManager.enemy.EnemyHealth - gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;;
                } 
            }
        }   

        if (battleHUDManager.enemy.EnemyHealth <= 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            Debug.Log("enemy isn't dead yet");
        }
        EnemyTurn();
        
    }
    public void EnemyAttack()
    {
        Debug.Log("we are in EnemyAttack");
        gridManager = FindObjectOfType<GridManager>();
        for (int x = 0; x < gridManager.width; x++)
        { 
            //check enemy if they have a card in Their cell
            if (gridManager.gridCells[x,1].GetComponent<GridCell>().cellFull == true)
            {   
                // Do we have a card in our cell 
                if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
                {
                    int difference = gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth - gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
                    // only does damage to the card
                    if (difference > 0)
                    {
                        gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth = gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth - gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
                        gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().UpdateFieldDisplay();
                    }
                    // Does enough damage to destroy the card
                    if (difference == 0)
                    {
                        Destroy(gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell);
                        gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull = false;
                    }
                    // Does enough damage to destroy the card and the opponent
                    if (difference < 0)
                    {
                        Destroy(gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell);
                        gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull = false;
                        battleHUDManager.gameManager.PlayerHealth = battleHUDManager.gameManager.PlayerHealth + difference;
                    }
                }
                else // we don't have a card in our cell
                {
                    // my hp = my hp - enemyCardDamage
                    battleHUDManager.gameManager.PlayerHealth = battleHUDManager.gameManager.PlayerHealth - gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
                } 
            }
        }
        if (battleHUDManager.gameManager.PlayerHealth <= 0)
        {
            state = BattleState.LOST;
            EndBattle();
        } else 
        {
            state = BattleState.PLAYERTURN;
            Debug.Log("player(us) aren't dead, so state=playerturn");
        }
        PlayerTurn();
        
    }
    public void EnemyTurn()
    {
        Debug.Log("we are in enemy turn()");
        if (state == BattleState.ENEMYTURN)
        {
            Debug.Log("BattleState has to be enemyturn here");
            Debug.Log(state);
            battleHUDManager.enemy.enemyCurrentEnergy = battleHUDManager.enemy.enemyCurrentEnergy + 2;
            Debug.Log("Enemey got plus 2 Eneregy");
            battleHUDManager.enemy.PlayCard();
            EnemyAttack();
        }

    }
    public void EndBattle()
    {
        Debug.Log("we are in state: ");
        Debug.Log(state);
        if (state == BattleState.WON)
        {
            //dialogueText.text = "you won";
            SceneManager.LoadScene (GameObject.Find("To Battle").GetComponent<ToBattleArea>().sceneName) ;
        } else 
        {
            //dialogueText.text = "you lose!"
            SceneManager.LoadScene ("MainMenu") ;
        }
    }
    public void PlayerTurn()
    {
        //dialougeText.text = "Your Turn";
        //DrawCard(HandManager handManager)
        Debug.Log("In Player Turn, we get +2 energy");
        battleHUDManager.gameManager.currentEnergy = battleHUDManager.gameManager.currentEnergy + 2;
    }
    public void OnEndTurnButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        Debug.Log("we clicked end  turn button");
        PlayerAttack();
    }

}
