using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerCoinCounter : MonoBehaviour
{
    public int currentCoinCount;
    [SerializeField] private TMP_Text coinText;

    //void Update()
    //{
    //    coinText.text = currentCoinCount.ToString();
    //}
}
