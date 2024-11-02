using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerHealthTracker : MonoBehaviour
{
    public int maxPlayerHealth;
    public int currentPlayerHealth;
    public Slider uiSlider;

    void Update()
    {
        uiSlider.maxValue = maxPlayerHealth;
        uiSlider.value = currentPlayerHealth;

        uiSlider.value = Mathf.Clamp(currentPlayerHealth, uiSlider.minValue, uiSlider.maxValue);
    }
}
