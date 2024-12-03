using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCards;


// health 0-25 == 1.25, 25-50 == 1.15, 50-75 == 1, 75-100 
public enum HealthState {CRITICAL, ALMOSTCRITICAL, HURT, FINE}

public class ChoicesAI : MonoBehaviour
{
    public BattleHUDManager battleHUDManager;
    public GridManager gridManager;
    double currentHealthPercent;
    int healthValue;
    int attackValue;
    int healthWeight;
    int energyWeight;
    // health 0-25 == 1.25, 25-50 == 1.15, 50-75 == 1, 75-100 
    public enum HealthState {CRITICAL, ALMOSTCRITICAL, HURT, FINE}

    public HealthState state;

    public void Start()
    {
        battleHUDManager = FindObjectOfType<BattleHUDManager>();
        gridManager = FindObjectOfType<GridManager>();
    }
    
    public void CheckHealthValue(int currentHealth, int maxHealth)
    {
        //currentHealthPercent = battleHUDManager.enemy.EnemyHealth/battleHUDManager.enemy.enemyMaxHealth;
        Debug.Log(currentHealth);
        Debug.Log(maxHealth);
        double currentHealthPercent = (double) currentHealth / (double) maxHealth;
        Debug.Log("CurrentHealthValue is :");
        Debug.Log(currentHealthPercent);
        // Ai has critical health, almost critical, hurt, fine
        if (currentHealthPercent <=.25)
        {
            state = HealthState.CRITICAL;
            //return 1.00;
        } else if (currentHealthPercent <= .50)
        {
            state = HealthState.ALMOSTCRITICAL;
            //return 0.85;
        } else if (currentHealthPercent <= .75)
        {
            state = HealthState.HURT;
            //return 0.70;
        } else if (currentHealthPercent <= .100)
        {
            state = HealthState.FINE;
            //return 0.55
        } else 
        {
            // error
            Debug.Log("Therre was an error in CheckHealthValue");
        }
        Debug.Log(currentHealth);
        Debug.Log(maxHealth);
        Debug.Log("Therre was an error in CheckHealthValue (in the negatives?)");
    }

    // calculates the damage the enemy will take 
    public int FutureDamageCheck()
    {
        gridManager = FindObjectOfType<GridManager>();
        int totalDamage = 0;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if human player has cards down, and if we it does we add up total damage next turn
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {
                totalDamage += gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
            }
            
        }
        return totalDamage;
    }

    // calculates how much the enemy will do to the player
    public int FutureEnemyDamageCheck()
    {
        gridManager = FindObjectOfType<GridManager>();
        int totalDamage = 0;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if enemy player has cards down, and if we it does we add up total damage next turn
            if (gridManager.gridCells[x,1].GetComponent<GridCell>().cellFull == true)
            {
                totalDamage += gridManager.gridCells[x,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
            }
            
        }
        return totalDamage;
    }

    // This is the function where the move gets decided
    public void CalculateFutureDecision()
    {
         Debug.Log("In CALCULATEFUTUREDECISION" + state);
         Debug.Log(state);
        if (state == HealthState.CRITICAL)
        {
            int futureDamage = FutureDamageCheck();

            //This means the AI will die next turn and try to save itself by attacking the card
            if (futureDamage >= battleHUDManager.enemy.EnemyHealth)
            {
                //first kill the highest attack card and just start attacking with all energy
                KillStrongestDamage();
                KillWeakestHealh();
                KillWeakestDamage();

            }
            // this means AI will live and will try to do  A. Attack directly orAttack cards. or B. Kil to win but we are critical...
            if (futureDamage < battleHUDManager.enemy.EnemyHealth)
            {
                // grabbing how much damage enemy can do.
                int currentDamage = FutureEnemyDamageCheck();
                if (currentDamage == 0)
                {
                    // the enemy has no cards on the field... do some damage
                    // by attacking lowest health first, and high damage card that probably has low hp
                    int randomOne = Random.Range(0,4);
                    PlayStrongest(randomOne);
                    KillWeakestHealh();
                    KillStrongestDamage();
                } else 
                {
                    // we do have cards on the field so attack where we can
                    int randomOne = Random.Range(0,4);
                    PlayWeakest(randomOne);
                    KillWeakestDamage();
                    KillWeakestHealh();
                }
               
            }

        }

        // this is mid to late game... the enemy has 25-50% hp left and could possibly die next turn too
        if (state == HealthState.ALMOSTCRITICAL)
        {
            // by now the what was the lowest player health is probably dead...
            KillStrongestDamage();
            // we can do some damage here
            KillWeakestDamage();
        }

        // the enemy is somewhere between 50-75% hp left.
        // damage should have been done on both sides, so some of the player 
        // cards could be hurt...
        if (state == HealthState.HURT)
        {
            // first lets kill of the weakest health card...
            // may we already have a card here and it won't do anything
            KillWeakestHealh();

            // since it's almost mid game, and it was probably one or two turns to get here...
            // the enemy will attack the next high hp card...
            KillStrongestHealth();
            // if we still have energy to play probably play a new card randomly..,
            // playDefault();

        }

        // this should be the case if the human doesn't attack with something too crazy...
        // The enemy should open with weak cards regardless if they can beat the human cards.
        if (state == HealthState.FINE)
        {
            int playerCards = PlayerCardCount();
            // since we are FINE player probably started with a weak card

            if (playerCards == 0)
            {
                int randomness;
                randomness = Random.Range(0,2);
                if (randomness == 0)
                {
                    // AI will idle with player
                }
                if (randomness  == 1)
                {
                    // ai will play cards
                    int play = CurrentWeakestCard();
                    if (play == -1)
                    {
                        // dont have the enrgy to play
                    } else
                    {
                        int randomTwo = Random.Range(0,4);
                        if (gridManager.gridCells[randomTwo,1].GetComponent<GridCell>().cellFull == false)
                        {
                            PlayCardSlot(randomTwo, play);
                        }
                    }
                    
                }


            }
            if (playerCards == 1)
            {
                // this should always grab something 
                int playerCardSlot = PlayerStrongestSlot();
                // we jsut want to grab enemy's weakest hand card against player's one card if we are healthy.
                int enemyWeakCard = CurrentWeakestCard();

                if (enemyWeakCard == -1)
                {
                    // we have no card to play...
                    Debug.Log("Enemy has no energy and is FINE");
                } else
                {
                    if (gridManager.gridCells[playerCardSlot,1].GetComponent<GridCell>().cellFull == false)
                    {   
                        if (PlayCardSlot(playerCardSlot, enemyWeakCard))
                        {
                            Debug.Log("We are in FINE, and playerCards = 1");
                        } else
                        {
                            Debug.Log("Enemy has no energy and is FINE");
                        }
                    }
                }
            }

            // player played two low energy cards...
            if (playerCards == 2)
            {
                
                KillWeakestHealh();
                KillStrongestHealth();
            }

            // IF SOMEONEHOW player has 3 cards and enemy HP is still FINE...
            // first plays weakest card which should have low energy, then plays next best card to beat weak
            if (playerCards == 3)
            {
                // this should always grab something 
                int playerCardSlot = PlayerStrongestSlot();
                // we jsut want to grab enemy's weakest hand card against player's one card if we are healthy.
                int enemyWeakCard = CurrentWeakestCard();

                if (enemyWeakCard == -1)
                {
                    // we have no card to play...
                    Debug.Log("Enemy has no energy and is FINE");
                } else
                {
                    if (gridManager.gridCells[playerCardSlot,1].GetComponent<GridCell>().cellFull == false)
                    {   
                        if (PlayCardSlot(playerCardSlot, enemyWeakCard))
                        {
                            Debug.Log("We are in FINE, and playerCards = 1");
                        } else
                        {
                            Debug.Log("Enemy has no energy and is FINE");
                        }
                    }
                }
                KillWeakestHealh();
                KillStrongestHealth();
            }
            //if (playercards = 4) THIS 99% SURE WONT HAPPEN
        }
    }

    public int SecondPlayerStrongest(int IgnoreSlot)
    {
        // we are checking to see the second strongest while ignoring IgnoreSlot(which is strongest)
        gridManager = FindObjectOfType<GridManager>();
        int slot = -1;
        int highestDamage = 0;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if human player has cards down to find highest Attack
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {
                // get the slot where player strongest card is, (grabs first strongest if damages are the same)
                if (highestDamage < gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage)
                {
                    if (x != IgnoreSlot)
                    {
                        slot = x;
                    }
                }
            }
        }
        return slot;
    }

    public int PlayerCardCount()
    {
        gridManager = FindObjectOfType<GridManager>();
        int totalCards = 0;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if human player has cards down, and if we it does we add up total damage next turn
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {
                totalCards = totalCards + 1;
            }
            
        }
        return totalCards;
    }

    // public bool CheckForSlotKill()
    // {
    //     //
    // }

    // grabs strongest dmg player slot from the field
    public int PlayerStrongestSlot()
    {
        gridManager = FindObjectOfType<GridManager>();
        int slot = -1;
        int highestDamage = -1;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if human player has cards down to find highest Attack
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {
                // get the slot where player strongest card is, (grabs first strongest if damages are the same)
                if (highestDamage < gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage)
                {
                    slot = x;
                    highestDamage = gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
                }
            }
        }
        return slot;
    }
    // grabs player highest hp card on field
    public int PlayerStrongestHealthtSlot()
    {
        gridManager = FindObjectOfType<GridManager>();
        int slot = -1;
        int highestHealth = -1;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if human player has cards down to find highest Attack
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {
                // get the slot where player strongest card is, (grabs first strongest if damages are the same)
                if (highestHealth < gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth)
                {
                    slot = x;
                    highestHealth = gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth;
                }
            }
        }
        return slot;
    }

    // grabs the player weakest health card on the field
    public int PlayerWeakestHealthSlot()
    {
        gridManager = FindObjectOfType<GridManager>();
        int slot = -1;
        int lowestHealth = 100;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if human player has cards down to find highest Attack
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {
                // get the slot where player weakest card is, (grabs first weakest if damages are the same)
                if (lowestHealth > gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth)
                {
                    slot = x;
                    lowestHealth = gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth;
                }
            }
        }
        if (lowestHealth == 100)
        {
            return -1;
        }
        return slot;
    }


    // grabs the weakest dmg player slot from the field.
    public int PlayerWeakestSlot()
    {
        gridManager = FindObjectOfType<GridManager>();
        int slot = -1;
        int highestDamage = 100;
        for (int x = 0; x < gridManager.width; x++)
        {  
            // check to see if human player has cards down to find highest Attack
            if (gridManager.gridCells[x,0].GetComponent<GridCell>().cellFull == true)
            {
                // get the slot where player weakest card is, (grabs first weakest if damages are the same)
                if (highestDamage > gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage)
                {
                    slot = x;
                    highestDamage = gridManager.gridCells[x,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
                }
            }
        }
        if (highestDamage == 100)
        {
            return -1;
        }
        return slot;
    }

    // enemy is trying to kill the weakest damage card on the player's field
    // using the CARDTOBEAT function
    public void KillWeakestDamage()
    {
        gridManager = FindObjectOfType<GridManager>();
        int weakestPlayerDamage = 0;
        int weakestPlayerSlot = PlayerWeakestSlot();
        //There are no cards on the player field... so play default()
        if (weakestPlayerSlot == -1)
        {
            int whateverIndex;
            whateverIndex = Random.Range(0,4);
            PlayWeakest(whateverIndex);
        } 
        else 
        {
            // We store the HP of the weakest card
            weakestPlayerDamage = gridManager.gridCells[weakestPlayerSlot,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
            int weakestplayerDamageHP =gridManager.gridCells[weakestPlayerSlot,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth;
             
            // may not always choose the strongest card in our hand due to energy.
            int winningIndex = CardToBeat(weakestplayerDamageHP);

            if (winningIndex == -1) // out of cards or no energy or no card can beat it
            {
                // We don't have sufficent energy to play the StrongestCard
                // if the ai has 0 cards to play next turn it?
                // if we have no energy to play any card... next turn it?
                PlayWeakest(weakestPlayerSlot);
            } else
            {
                // checking to see if the slot for the enemy is empty apposing the strongest human slot
                if (gridManager.gridCells[weakestPlayerSlot,1].GetComponent<GridCell>().cellFull == false)
                {
                
                    // if this is true it means the enemy succesfully put a card down.
                    if (PlayCardSlot(weakestPlayerSlot, winningIndex))
                    {
                        Debug.Log("Enemy played the cardtobeat() card to beat Human weakest damage");
                        // we did it we played the strongest card, now what...
                    }else
                    {
                        // we should already have enough energy to play the "current strong card"
                        Debug.Log("Something went wrong playing the KillWeakestDamage()");
                    }
                }
            }
        }

    }

    // the enemy is looking to kill the weakest HP card on the player field
    // USing the CARDTOBEAT function
    public void KillWeakestHealh()
    {
        gridManager = FindObjectOfType<GridManager>();
        int weakestPlayerHealth = 0;
        int weakestPlayerSlot = PlayerWeakestHealthSlot();
        //There are no cards on the player field... so play default()
        if (weakestPlayerSlot == -1)
        {
            int whateverIndex;
            whateverIndex = Random.Range(0,4);
            PlayWeakest(whateverIndex);
        } 
        else 
        {
            // We store the HP of the weakest card
            weakestPlayerHealth = gridManager.gridCells[weakestPlayerSlot,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth;
             
            // grabs a card to beat the weakest playerhealth
            int winningIndex = CardToBeat(weakestPlayerHealth);

            if (winningIndex == -1) // out of cards or no energy
            {
                // We don't have sufficent energy to play the better card
                // Or we are out of cards
                PlayWeakest(weakestPlayerSlot);

            } else
            {
                // checking to see if the slot for the enemy is empty apposing the strongest human slot
                if (gridManager.gridCells[weakestPlayerSlot,1].GetComponent<GridCell>().cellFull == false)
                {
                
                    // if this is true it means the enemy succesfully put a card down.
                    if (PlayCardSlot(weakestPlayerSlot, winningIndex))
                    {
                        Debug.Log("Enemy played the cardtobeat card to beat Human weak hp");
                        // we did it we played the strongest card, now what...
                    }else
                    {
                        // we should already have enough energy to play the "current strong card"
                        Debug.Log("Something went wrong playing the card to beat Human weak hp");
                    }
                }
            }
        }

    }
    
    // enemy is lokking to kill the higest damage player card 
    // using the CURRENTSTRONGEST CARD..(may have to change to cardtobeat)
    public void KillStrongestDamage()
    {
        gridManager = FindObjectOfType<GridManager>();
        int highestPlayerDamage = 0;
        int strongestPlayerSlot = PlayerStrongestSlot();
        //There are no cards on the player field... so play default()
        if (strongestPlayerSlot == -1)
        {
            int whateverIndex;
            whateverIndex = Random.Range(0,4);
            PlayStrongest(whateverIndex);
        } 
        else 
        {
            // We can store the human's highest damage card on the field
            highestPlayerDamage = gridManager.gridCells[strongestPlayerSlot,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.damage;
            int highestPlayerHealth = gridManager.gridCells[strongestPlayerSlot,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth;
             
            // may not always choose the strongest card in our hand due to energy.
            int highestCurrentEnemyIndex = CardToBeat(highestPlayerHealth);

            if (highestCurrentEnemyIndex == -1) // out of cards or no energy
            {
                // We don't have sufficent energy to play the StrongestCard
                // if the ai has 0 cards to play next turn it?
                // if we have no energy to play any card... next turn it?
                PlayStrongest(strongestPlayerSlot);

            }  else 
            {
                // checking to see if the slot for the enemy is empty apposing the strongest human slot
                if (gridManager.gridCells[strongestPlayerSlot,1].GetComponent<GridCell>().cellFull == false)
                {
                    // checking the strongest human card's HP is <= strongest AI card in hand
                    if (gridManager.gridCells[strongestPlayerSlot,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth <= battleHUDManager.enemy.enemyCards[highestCurrentEnemyIndex].damage)
                    {   // if this is true it means the enemy succesfully put a card down.
                        if (PlayCardSlot(strongestPlayerSlot, highestCurrentEnemyIndex))
                        {
                            Debug.Log("Enemy played the strongest current card to beat Human strong Card");
                            // we did it we played the strongest card, now what...
                        }else
                        {
                            // we should already have enough energy to play the "current strong card"
                            Debug.Log("Something went wrong playing the strongest current card");
                        }
                    }
                }
            }
        }
    }

    // trying to beat the strongest player health card
    // using the CARDTOBEAT function
    public void KillStrongestHealth()
    {
        gridManager = FindObjectOfType<GridManager>();
        int highestPlayerHealth = 0;
        int strongestPlayerHealthSlot = PlayerStrongestHealthtSlot();
        //There are no cards on the player field... so play default()
        if (strongestPlayerHealthSlot == -1)
        {
            int whateverIndex;
            whateverIndex = Random.Range(0,4);
            PlayStrongest(whateverIndex);
        } 
        else 
        {
            // We can store the human's highest hp card on the field
            highestPlayerHealth = gridManager.gridCells[strongestPlayerHealthSlot,0].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth;
             
            // may not always choose the strongest card in our hand due to energy.
            // int highestCurrentEnemyIndex = CurrentStrongestCard();

            int winningIndex = CardToBeat(highestPlayerHealth);

            if (winningIndex == -1) // out of cards or no energy
            {
                // We don't have sufficent energy to play the better card
                // Or we are out of cards
                PlayStrongest(strongestPlayerHealthSlot);

            } else
            {
                // checking to see if the slot for the enemy is empty apposing the strongest human slot
                if (gridManager.gridCells[strongestPlayerHealthSlot,1].GetComponent<GridCell>().cellFull == false)
                {
                
                    // if this is true it means the enemy succesfully put a card down.
                    if (PlayCardSlot(strongestPlayerHealthSlot, winningIndex))
                    {
                        Debug.Log("Enemy played the cardtobeat card to beat Human weak hp");
                        // we did it we played the strongest card, now what...
                    }else
                    {
                        // we should already have enough energy to play the "current strong card"
                        Debug.Log("Something went wrong playing the card to beat Human weak hp");
                    }
                }
            }
        }
    }

    // just grabs weakest available card and plays it instead.
    public void PlayWeakest(int Slot)
    {
        int weakIndex = CurrentWeakestCard();
        if (weakIndex == -1)
        {
            // we don't have the energy to play the weakest card....
            return;
        }
        if (gridManager.gridCells[Slot,1].GetComponent<GridCell>().cellFull == false)
        {
            if (PlayCardSlot(Slot, weakIndex))
            {
                Debug.Log("Enemy played the cardtobeat card to beat Human weak hp");
                        // we did it we played the strongest card, now what...
            }else
            {
                // we should already have enough energy to play the "current strong card"
                Debug.Log("Something went wrong playing the card to beat Human weak hp");
            }
        }
    }

    //grabs the strongest available card and plays it instead
    public void PlayStrongest(int Slot)
    {
        int weakIndex = CurrentStrongestCard();
        if (weakIndex == -1)
        {
            // we don't have the energy to play the weakest card....
            return;
        }
        if (gridManager.gridCells[Slot,1].GetComponent<GridCell>().cellFull == false)
        {
            if (PlayCardSlot(Slot, weakIndex))
            {
                Debug.Log("Enemy played the cardtobeat card to beat Human weak hp");
                        // we did it we played the strongest card, now what...
            }else
            {
                // we should already have enough energy to play the "current strong card"
                Debug.Log("Something went wrong playing the card to beat Human weak hp");
            }
        }
    }

    public int ActualStrongestCard()
    {
        // -1 if out of cards
        int strength = -1;
        int strongestCardIndex = -1;
        for (int x = 0; x < battleHUDManager.enemy.enemyCards.Count && x < battleHUDManager.enemy.usableCards; x++)
        {
            if (battleHUDManager.enemy.enemyCards[x].damage > strength)
            {
                strongestCardIndex = x;
                strength = battleHUDManager.enemy.enemyCards[x].damage;
            }
        }
        return strongestCardIndex;
    }

    // grabing the last card to beat the opposing card's hp
    public int CardToBeat(int PlayerCardHealth)
    {
        // -1 if out of cards or no energy
        int cardIndex = -1;

        // looking for a card to beat the damagetobeat


        for (int x = 0; x < battleHUDManager.enemy.enemyCards.Count && x < battleHUDManager.enemy.usableCards; x++)
        {
            if (IsCardPlayable(battleHUDManager.enemy.enemyCards[x]) == true && battleHUDManager.enemy.enemyCards[x].damage > PlayerCardHealth)
            {
                cardIndex = x;
            }
        }

        return cardIndex;
    }
    // // This is checking if it should sacrafice the current card in the index to take on the enemy Card or empty slot.
    // // We for sure know that there is a player card in the (index,0) slot.
    // public bool SacraficeOrNot(int index)
    // {
    //     // if AI has no card in the slot it'll play true
    //     if (gridManager.gridCells[Index,1].GetComponent<GridCell>().cellFull == false)
    //     {
    //         return true;
    //     }
    //     if (gridManager.gridCells[Index,1].GetComponent<GridCell>().objectInCell.GetComponent<OnFieldDisplay>().cardData.currentHealth)
    // }

    // we are collecting the strongest playable card index from the ai playable hand

    // grab the Weakest card from the enemy hand. returns -1 if we can't
    public int CurrentWeakestCard()
    {
        // -1 if out of cards or no energy
        int WeakestCardIndex = -1;
        int strength = 100;
        // won't consider the second card that has the same damage and playable energy...
        for (int x = 0; x < battleHUDManager.enemy.enemyCards.Count && x < battleHUDManager.enemy.usableCards; x++)
        {
            if (IsCardPlayable(battleHUDManager.enemy.enemyCards[x]) == true && battleHUDManager.enemy.enemyCards[x].damage < strength)
            {
                WeakestCardIndex = x;
                strength = battleHUDManager.enemy.enemyCards[x].damage;
            }
        }

        if (strength == 100)
        {
            return -1;
        }

        return WeakestCardIndex;
    }
    // grab the strongest card from the enemy hand. returns -1 if we cant
    public int CurrentStrongestCard()
    {
        // -1 if out of cards or no energy
        int strongestCardIndex = -1;
        int strength = -1;
        // won't consider the second card that has the same damage and playable energy...
        for (int x = 0; x < battleHUDManager.enemy.enemyCards.Count && x < battleHUDManager.enemy.usableCards; x++)
        {
            if (IsCardPlayable(battleHUDManager.enemy.enemyCards[x]) == true && battleHUDManager.enemy.enemyCards[x].damage > strength)
            {
                strongestCardIndex = x;
                strength = battleHUDManager.enemy.enemyCards[x].damage;
            }
        }
        return strongestCardIndex;
    }

    // checks energy of AI to see if it's playable, returns true
    public bool IsCardPlayable(Card card)
    {   
        //we have 10 energy card is 5
        if (card.energy <= battleHUDManager.enemy.enemyCurrentEnergy)
        {
            return true;
        }
        return false;
    }

    //ai will put the card in one of their slots [0,1,2,3], and return true if card is placed
    public bool PlayCardSlot(int Slot, int CardIndex)
    {
        // We don't have to check if there is a card playing here. we will have before hand check
        // Same for energy, i think we should always have enought to call this 
        // SECRET FAIL SAFE,
        if (CardIndex == -1)
        {
            // Debug.Log("For somewhere a return -1 :/")
            return false;
        }
        int energyLeft = battleHUDManager.enemy.enemyCurrentEnergy - battleHUDManager.enemy.enemyCards[CardIndex].energy;
        // the card is legal to play for the AI 
        if(energyLeft >= 0)
        {
            // making sure card was placed by it being true, and the method is a bool
            bool placedCard = gridManager.AddObjectToGrid(battleHUDManager.enemy.enemyCards[CardIndex].prefab, gridManager.gridCells[Slot,1].GetComponent<GridCell>().gridIndex, battleHUDManager.enemy.enemyCards[CardIndex]);
            // if it was true, we update enemy's energy and remove the card from deck
            if (placedCard == true)
            {
                battleHUDManager.enemy.enemyCurrentEnergy = energyLeft;
                battleHUDManager.enemy.enemyCards.RemoveAt(CardIndex);
                battleHUDManager.enemy.usableCards--;
                return true;
            } else
            {
                return false;
            }
        }
        return false;
    }

    //?????
    public int CheckEnergyCard(Card data)
    {
        if (data == null)
        {
            return -1;
        }
       return data.energy;
    }
}
