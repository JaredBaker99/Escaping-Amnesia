using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SacraficeDialogUI : MonoBehaviour
{
    // so actuall this dialog box can't be used anywhere else till a bug is fixed :L
    // this way you can use this dialoge anywhere else with any question with a yes or no
    // here is an example:SacraficeDialogUI.Instance.ShowQuestion("Are you sure you want to quit the game", () => { }, () => {})
    public static SacraficeDialogUI Instance {get; private set;}
    private TextMeshProUGUI textMeshPro;
    private Button yesButton;
    private Button noButton;


    private void Awake()
    {
        Instance = this;
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesButton = transform.Find("YesButton").GetComponent<Button>();
        noButton = transform.Find("NoButton").GetComponent<Button>();

        Hide();

    }
    // passing a function as an arguemnt for action (Delegate)
    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);
        textMeshPro.text = questionText;
        yesButton.onClick.AddListener(() => {  
            Hide();
            yesAction();
        });
        noButton.onClick.AddListener(() => {  
            Hide();
            noAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
