using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    public GameObject consolePanel; // Reference to the ConsolePanel GameObject

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) // `~` key
        {
            ToggleConsole();
        }
    }

    private void ToggleConsole()
    {
        if (consolePanel != null)
        {
            bool isConsoleActive = !consolePanel.activeSelf;

            consolePanel.SetActive(isConsoleActive);

            // Pause or resume the game based on console visibility
            Time.timeScale = isConsoleActive ? 0f : 1f;
        }
    }
}
