using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    // Static counter to keep track of scene changes across all scenes
    private static int sceneChangeCount = 0;

    // A flag to prevent multiple triggers
    private bool isSceneChanging = false;

    // Cached references to the Renderer and Collider components
    private Renderer objectRenderer;
    private Collider2D objectCollider;

    private void Awake()
    {
        // Cache references to the object's renderer and collider
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider2D>();

        // Make sure this object is not destroyed when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug message before trigger check
        UnityEngine.Debug.Log("OnTriggerEnter2D started.");

        // Prevent multiple scene changes by checking the flag
        if (!isSceneChanging && collision.CompareTag("Player"))
        {
            isSceneChanging = true;  // Set the flag to true when the scene change starts

            // Disable the object visually and physically
            HideAndDisableObject();

            // Define the different types of scenes
            string[] battleScenes = { "RectangleBattle-1", "RectangleBattle-2", "RectangleBattle-3", "RectangleBattle-4", "RectangleBattle-5", "ElbowBattle-1", "ElbowBattle-2", "CircleBattle-1", "CircleBattle-2" };
            string[] shopScenes = { "UpgradeRoom-1", "UpgradeRoom-2" };
            string[] bossRoom = { "BossRoom-1", "BossRoom-2" };

            // Increment the scene change counter
            sceneChangeCount++;
            UnityEngine.Debug.Log("Scene Change Count: " + sceneChangeCount);

            // Always load a boss room on the 21st scene change
            if (sceneChangeCount >= 21)
            {
                int randomBossIndex = UnityEngine.Random.Range(0, bossRoom.Length);
                UnityEngine.Debug.Log("Loading boss room: " + bossRoom[randomBossIndex]);
                SceneManager.LoadScene(bossRoom[randomBossIndex]);
            }
            else if (sceneChangeCount % 5 == 0)
            {
                // Load a random shop scene every 5th scene change
                int randomShopIndex = UnityEngine.Random.Range(0, shopScenes.Length);
                UnityEngine.Debug.Log("Loading shop scene: " + shopScenes[randomShopIndex]);
                SceneManager.LoadScene(shopScenes[randomShopIndex]);
            }
            else
            {
                // Load a random battle scene
                int randomBattleIndex = UnityEngine.Random.Range(0, battleScenes.Length);
                UnityEngine.Debug.Log("Loading battle scene: " + battleScenes[randomBattleIndex]);
                SceneManager.LoadScene(battleScenes[randomBattleIndex]);
            }
        }

        // Debug message after trigger check
        UnityEngine.Debug.Log("OnTriggerEnter2D finished.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the flag once the new scene has loaded
        isSceneChanging = false;
    }

    private void OnEnable()
    {
        // Register the OnSceneLoaded callback
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unregister the OnSceneLoaded callback
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method hides and disables the object after a scene change
    private void HideAndDisableObject()
    {
        // Disable the renderer so the object is no longer visible
        if (objectRenderer != null)
        {
            objectRenderer.enabled = false;
        }

        // Disable the collider so the object can no longer be collided with
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }
    }
}
