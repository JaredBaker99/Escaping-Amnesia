using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionSacrafice : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Button yesButton;
    private Button noButton;


    private void Awake()
    {
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesButton = transform.Find("YesButton").GetComponent<Button>();
        noButton = transform.Find("NoButton").GetComponent<Button>();

        ShowQuestion("RAFICE THE CARD FOR TWO ENERGY?", () => {
            Debug.Log("Yes!");
        }, () => {
            Debug.Log("No!");
        });
    }

    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        textMeshPro.text = questionText;
        yesButton.onClick.AddListener(new UnityEngine.Events.UnityAction(yesAction));
        noButton.onClick.AddListener(new UnityEngine.Events.UnityAction(noAction));
    }


}
