using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Switch : MonoBehaviour
{
    public GameObject me ;
    public Animator switchAnimator ;
    public int numberColors ;
    public bool On ;
    public int curColor ;

    void Start() {
        curColor = Random.Range(0, numberColors) ; 
        changeState() ;
    }

    public void changeState()
    {
        if(On) {
            curColor++ ;
            curColor = curColor%numberColors ;
            if(curColor == 0) {
                switchAnimator.Play("SwitchGreen");
            }
            else if(curColor == 1) {
                switchAnimator.Play("SwitchRed");
            }
            else if(curColor == 2) {
                switchAnimator.Play("SwitchBlue");
            }
            else if(curColor == 3) {
                switchAnimator.Play("SwitchYellow");
            }
            else if(curColor == 4) {
                switchAnimator.Play("SwitchPurple");
            }
        }

    }

    public void turnOff() {
        On = false ;
        switchAnimator.Play("SwitchBlack") ;
        me.GetComponent<SwitchInteractable>().ineracted = true;
    }
}
