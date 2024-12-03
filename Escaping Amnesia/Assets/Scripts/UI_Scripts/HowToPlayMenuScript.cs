using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TMP_Text bookText;
    public void Start(){
        
    }
    // Start is called before the first frame update
    public void NextPage(){
        if(bookText.pageToDisplay == 4){
            //Nothing
        }
        else{
            bookText.pageToDisplay++;
        }
    }
    public void BackPage(){
        if(bookText.pageToDisplay == 1){
            //Nothing
        }
        else{
            bookText.pageToDisplay--;
        }
    }
}
