using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug message before trigger check
        UnityEngine.Debug.Log("OnTriggerEnter2D started.");

        if (collision.CompareTag("Player"))
        {
            string[] scenes = { "RectangleBattle-1", "RectangleBattle-2", "RectangleBattle-3", "RectangleBattle-4", "RectangleBattle-5", "ElbowBattle-1", "ElbowBattle-2", "CircleBattle-1", "CircleBattle-2" };
            int randomIndex = Random.Range(0, scenes.Length - 1); // Corrected range
            UnityEngine.Debug.Log("Scenes array length: " + scenes.Length);
            UnityEngine.Debug.Log("Random index: " + randomIndex);
            UnityEngine.Debug.Log("Selected scene: " + scenes[randomIndex]);

            string randomScene = scenes[randomIndex];

            SceneManager.LoadScene(randomScene);
        }

        // Debug message after trigger check
        UnityEngine.Debug.Log("OnTriggerEnter2D finished.");
    }
}
