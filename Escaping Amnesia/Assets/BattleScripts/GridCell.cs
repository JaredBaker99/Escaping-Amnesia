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

    public SacraficeDialogUI sacraficeUI;

    public GameObject sacBox;

    public GameObject Canvas;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        //sacraficeUI = FindObjectOfType<SacraficeDialogUI>(true);
        //Vector3 originalScale = sacraficeUI.rectTransform.localScale;
        // Vector3 originalPosition = sacraficeUI.transform.position;
        //Quaternion originalRotation = sacraficeUI.rectTransform.localRotation;
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

        if (sacraficeUI == null)
        {
            Debug.Log("SacraficeUI is NULL");
            Debug.Log(sacraficeUI);
            Canvas = GameObject.Find("Canvas");
            GameObject SAC = Instantiate(sacBox,new Vector3(200, 400, 0), Quaternion.identity, Canvas.transform);
            //SAC.transform.position = new Vector3(0, 0, 0);
            // SAC.transform.position.x = -600.37;
            // SAC.transform.position.y = -2.9079;
            // SAC.transform.position.z = 0;
            sacraficeUI = FindObjectOfType<SacraficeDialogUI>(true);

            Debug.Log(sacraficeUI);
            Debug.Log(SAC);
        }
// try to do what you had before but tie the class to get component of the new instantiated ui box?
        sacraficeUI.ShowQuestion("Are you sure you want to Sacrifice the card?", () => { 
            Debug.Log("We clicked yes");
            Debug.Log(objectInCell);
            Destroy(objectInCell);
            cellFull = false;
            Destroy(sacraficeUI);
            gameManager.AddEnergy();
            //Destroy(SAC);
            Debug.Log("Broke1");
            wasClicked = false;
        }, () => {
            
            Destroy(sacraficeUI);
            wasClicked = false;
            // do nothing on no
        });    
        }


    }
}
