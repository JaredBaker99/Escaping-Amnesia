using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GridCell : MonoBehaviour
{
    public Vector2 gridIndex;

    public bool cellFull = false;

    public GameObject objectInCell;

    // to get to the player's energy
    public GameManager gameManager;

    public bool wasClicked = false;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        
    }
    void OnMouseUp()
    {  

        if (wasClicked == true || objectInCell == null || gridIndex.y == 1)
        {
            //do nothing
        }
        else
        {
        wasClicked = true;
        SacraficeDialogUI.Instance.ShowQuestion("Are you sure you want to Sacrafice the card?", () => { 
        Debug.Log("We clicked yes");
        Destroy(objectInCell);
        cellFull = false;
        Debug.Log("Broke1");
        //gameManager.AddEnergy();
        Debug.Log("broke2");
        wasClicked = false;
        }, () => {
        // do nothing on no
        });    
        }


    }
}
