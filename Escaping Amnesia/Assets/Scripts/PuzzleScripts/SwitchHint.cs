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
    public float startWaitTime ;
    public float startBlinkTime ;
    private float blinkTime ;
    private float waitTime ;
    public bool blinking ;
    public bool resetting ;
    private int currentColor ;


    
    void Start()
    {
        secret.SetActive(false) ;
        isHidden = true ;
        currentColor = -1;
        numSwitches =  switches.Count ;
        waitTime = startWaitTime ;
        blinkTime = startBlinkTime ;
        hiddenValues = new int[numSwitches] ;
        for (int i = 0; i < numSwitches; i++) {
            hiddenValues[i] = Random.Range(0, switches[i].GetComponent<Switch>().numberColors) ;
        }
        switchAnimator.Play("SwitchBlack") ;

    }

    void Update() {
        if(isHidden && !checkSwitches()) {
            if(waitTime <= 0) {
                currentColor++ ;
                if(currentColor >= numSwitches) {
                    resetting = true ;
                    currentColor = currentColor % numSwitches ;
                    currentColor--;
                }
                waitTime = startWaitTime + startBlinkTime ;
                blinkTime = startBlinkTime ; 
                changeColor() ;
                blinking = true ;
            }
            else if(blinkTime <= 0 && blinking) {
                changeColor() ;
            }
            else {
                waitTime -= Time.deltaTime ;
                blinkTime -= Time.deltaTime ;
            }
        }
        if(isHidden && checkSwitches()) {
            secret.SetActive(true) ;
            switchAnimator.Play("SwitchBlack") ;
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

    public void changeColor() {
        if(blinking || resetting) {
            resetting = false ;
            blinking = false ;
            switchAnimator.Play("SwitchBlack") ;
        }
        else if(hiddenValues[currentColor] == 0) {
            switchAnimator.Play("SwitchGreen");
        }
        else if(hiddenValues[currentColor] == 1) {
            switchAnimator.Play("SwitchRed");
        }
        else if(hiddenValues[currentColor] == 2) {
            switchAnimator.Play("SwitchBlue");
        }
        else if(hiddenValues[currentColor] == 3) {
            switchAnimator.Play("SwitchYellow");
        }
        else if(hiddenValues[currentColor] == 4) {
            switchAnimator.Play("SwitchPurple");
        }
    }
}

