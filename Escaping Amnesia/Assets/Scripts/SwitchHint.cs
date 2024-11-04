using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class SwitchHint : MonoBehaviour
{
    public GameObject secret;
    public Animator switchAnimator ;
    public List<GameObject> switches ;
    public bool isHidden ;
    public int[] hiddenValues ;
    public int numSwitches ;
    public bool hintRunning ;


    
    void Start()
    {
        secret.SetActive(false) ;
        isHidden = true ;
        numSwitches =  switches.Count ;
        hiddenValues = new int[numSwitches] ;
        for (int i = 0; i < numSwitches; i++) {
            hiddenValues[i] = Random.Range(0, switches[i].GetComponent<Switch>().numberColors) ;
        }

    }

    void Update() {
        if(!hintRunning) {
            //playHint() ;
        }
        if(isHidden && checkSwitches()) {
            secret.SetActive(true) ;
            isHidden = false ;
            for(int i = 0 ; i <  numSwitches ; i++) {
                switches[i].GetComponent<Switch>().turnOff() ;
            }
        }
    }

    public bool checkSwitches() {
        //UnityEngine.Debug.Log("CheckingSwitches");
        bool allMatch = true ;
        for(int  i = 0; i < numSwitches; i++) {
            if (hiddenValues[i] != switches[i].GetComponent<Switch>().curColor) {
                allMatch = false ;
                break ;
            }
        }
        return allMatch;
    }

    public void playHint() {

        hintRunning = true ;
        for(int i = 0 ; i<  numSwitches ; i++) {
            if(hiddenValues[i] == 0) {
                switchAnimator.Play("SwitchGreen");
            }
            else if(hiddenValues[i] == 1) {
                switchAnimator.Play("SwitchRed");
            }
            else if(hiddenValues[i] == 2) {
                switchAnimator.Play("SwitchBlue");
            }
            else if(hiddenValues[i] == 3) {
                switchAnimator.Play("SwitchYellow");
            }
            else if(hiddenValues[i] == 4) {
                switchAnimator.Play("SwitchPurple");
            }
   
         
        }
        switchAnimator.Play("SwitchBlack") ;

        hintRunning = false ;

    }
}

