using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SwitchInteractable : MonoBehaviour
{
    public bool isInRange;
    public bool ineracted = false;
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
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !ineracted)
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