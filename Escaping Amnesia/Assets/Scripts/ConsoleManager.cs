using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    public GameObject consolePanel; // Reference to the ConsolePanel GameObject

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) // `~` key
        {
            if (consolePanel != null)
            {
                consolePanel.SetActive(!consolePanel.activeSelf);
            }
        }
    }
}
