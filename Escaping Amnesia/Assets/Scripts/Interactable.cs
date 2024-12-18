using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject uiElement;

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                HideUI();
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            ShowUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInRange = false;
        HideUI();
    }

    void ShowUI()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
    }

    void HideUI()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }
}