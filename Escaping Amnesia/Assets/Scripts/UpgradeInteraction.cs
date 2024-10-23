using UnityEngine;
using UnityEngine.UI;

public class UpgradeInteraction : MonoBehaviour
{
    public GameObject popupText;  // The "Press E to open" popup
    public string currentUpgrade; // The name of the upgrade the player is near
    private bool isPlayerNearby = false;  // Whether the player is near an upgrade choice

    void Start()
    {
        popupText.SetActive(false);  // Hide the popup at the start
        currentUpgrade = "";  // No upgrade selected initially
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && currentUpgrade != "")
        {
            popupText.SetActive(false);  // Hide the popup
            LogSelection();  // Log which option was selected
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("here");
        if (other.CompareTag("Player"))
        {
            // Show the popup when the player is near an upgrade choice
            isPlayerNearby = true;
            currentUpgrade = other.gameObject.name;  // Set the current upgrade to the name of the object
            popupText.SetActive(true);  // Show the popup
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            currentUpgrade = "";  // Reset the upgrade selection
            popupText.SetActive(false);  // Hide the popup
        }
    }

    void LogSelection()
    {
        Debug.Log("Player selected: " + currentUpgrade);  // Log the name of the upgrade choice
    }
}
