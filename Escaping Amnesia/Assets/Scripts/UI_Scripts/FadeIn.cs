using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    //private CanvasGroup fadeToBlack = GameObject.Find("FadeToBlack").GetComponent<CanvasGroup>();
    private CanvasGroup fadeToBlack;
    
    public void Fade(int fadeOption){
        if (fadeOption == 0){
            StartCoroutine(FadeToBlack());
        }
        else{
            StartCoroutine(FadeFromBlack());
        }
    }
    private IEnumerator FadeFromBlack()//Fade the scene when the quit button is clikced
    {
        
        while (fadeToBlack.alpha > 0)
        {
            float opacity = fadeToBlack.alpha - .03f;
            Mathf.Clamp(opacity, 1, 0);
            fadeToBlack.alpha = opacity;
            yield return new WaitForSecondsRealtime(.01f);
        }
    }

    private IEnumerator FadeToBlack()//Fade the scene when the quit button is clikced
    {
        Debug.Log("Inside Coroutine");
        while (fadeToBlack.alpha < 1)
        {
            Debug.Log("Inside Loop Coroutine");
            float opacity = fadeToBlack.alpha + .01f;
            Mathf.Clamp(opacity, 0, 1);
            fadeToBlack.alpha = opacity;
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeToBlack = GameObject.Find("FadeToBlack").GetComponent<CanvasGroup>();
        fadeToBlack.alpha = 1;
        StartCoroutine(FadeFromBlack());
    }
}
