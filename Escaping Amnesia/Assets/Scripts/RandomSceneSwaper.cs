using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    private GameObject sceneCounter;
    private GameObject enemyStats;
    private GameObject toBattle;
    private GameObject overWorldGameAudio;
    private GameObject fade;

    //private CanvasGroup fadeToBlack = GameObject.Find("FadeToBlack").GetComponent<CanvasGroup>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("OnTriggerEnter2D started.");

        if (collision.CompareTag("Player"))
        {
            // Define the different types of scenes
            string[] battleScenes = { "RectangleBattle-1", "RectangleBattle-2", "RectangleBattle-3", "RectangleBattle-4", "RectangleBattle-5", "ElbowBattle-1", "ElbowBattle-2", "CircleBattle-1", "CircleBattle-2", "QuadBattleRoom-1", "QuadBattleRoom-2" };
            string[] shopScenes = { "UpgradeRoom-1", "UpgradeRoom-2" };
            string[] bossRoom = { "BossRoom-1", "BossRoom-2" };

            sceneCounter = GameObject.Find("Scene Counter");
            overWorldGameAudio = GameObject.FindGameObjectWithTag("OverworldAudio");
            fade = GameObject.FindGameObjectWithTag("Game Manager");
            // Debug.Log(fade);

            if (sceneCounter != null)
            {
                sceneCounter.GetComponent<SceneCounter>().counter += 1;
            }

            // Fade(0);

            enemyStats = GameObject.Find("Enemy Stats");
            if (enemyStats != null)
            {
                enemyStats.GetComponent<EnemyStats>().setAllAlive();
            }

            toBattle = GameObject.Find("To Battle");
            if (toBattle != null)
            {
                toBattle.GetComponent<ToBattleArea>().setToBattle(false);
            }

            if (sceneCounter != null)
            {
                UnityEngine.Debug.Log("Scene Change Count: " + sceneCounter.GetComponent<SceneCounter>().counter);
            }

            //Fade to black
            // Fade(0);


            // Always load a boss room on the 21st scene change
            if (sceneCounter != null && sceneCounter.GetComponent<SceneCounter>().counter >= 21)
            {
                int randomBossIndex = UnityEngine.Random.Range(0, bossRoom.Length);
                UnityEngine.Debug.Log("Loading boss room: " + bossRoom[randomBossIndex]);
                SceneManager.LoadScene(bossRoom[randomBossIndex]);
            }
            else if (sceneCounter != null && sceneCounter.GetComponent<SceneCounter>().counter % 5 == 0)
            {
                int randomShopIndex = UnityEngine.Random.Range(0, shopScenes.Length);
                UnityEngine.Debug.Log("Loading shop scene: " + shopScenes[randomShopIndex]);
                SceneManager.LoadScene(shopScenes[randomShopIndex]);
            }
            else
            {
                int randomBattleIndex = UnityEngine.Random.Range(0, battleScenes.Length);

                if(battleScenes[randomBattleIndex] == "QuadBattleRoom-1")
                {
                    if(sceneCounter.GetComponent<SceneCounter>().counter <= 10)
                    {
                        sceneCounter.GetComponent<SceneCounter>().quadFirst = true;
                    }
                    else
                    {
                        sceneCounter.GetComponent<SceneCounter>().quadSecond = true;
                    }
                    SceneManager.LoadScene("quadBattleRoom-1");
                }
                else if(sceneCounter.GetComponent<SceneCounter>().counter == 9 && !sceneCounter.GetComponent<SceneCounter>().quadFirst)
                {
                    sceneCounter.GetComponent<SceneCounter>().quadFirst = true;
                    SceneManager.LoadScene("QuadBattleRoom-1");
                }
                else if(sceneCounter.GetComponent<SceneCounter>().counter == 19 && !sceneCounter.GetComponent<SceneCounter>().quadSecond)
                {
                    sceneCounter.GetComponent<SceneCounter>().quadSecond = true;
                    SceneManager.LoadScene("quadBattleRoom-1");
                }
                else
                {
                    UnityEngine.Debug.Log("Loading battle scene: " + battleScenes[randomBattleIndex]);
                    SceneManager.LoadScene(battleScenes[randomBattleIndex]);
                }

            }

        }
        UnityEngine.Debug.Log("OnTriggerEnter2D finished.");
        overWorldGameAudio.GetComponent<OverworldAudioManager>().changeMusic(sceneCounter.GetComponent<SceneCounter>().counter);
        // Fade(1);
    }

    // public void Fade(int fadeOption)
    // {
    //     if (fadeOption == 0)
    //     {
    //         StartCoroutine(FadeToBlack());
    //     }
    //     else
    //     {
    //         StartCoroutine(FadeFromBlack());
    //     }
    // }
    // private IEnumerator FadeFromBlack()//Fade the scene when the quit button is clikced
    // {
    //     Debug.Log("FromBlack: Inside Coroutine");
    //     while (fadeToBlack.alpha > 0)
    //     {
    //         Debug.Log("FromBlack: Inside Loop Coroutine");
    //         float opacity = fadeToBlack.alpha - .03f;
    //         Mathf.Clamp(opacity, 1, 0);
    //         fadeToBlack.alpha = opacity;
    //         yield return new WaitForSecondsRealtime(.01f);
    //     }

    // }

    // private IEnumerator FadeToBlack()//Fade the scene when the quit button is clikced
    // {
    //     while (fadeToBlack.alpha < 1)
    //     {
    //         Debug.Log("ToBlack: Inside Loop Coroutine");
    //         float opacity = fadeToBlack.alpha + .01f;
    //         Mathf.Clamp(opacity, 0, 1);
    //         fadeToBlack.alpha = opacity;
    //         yield return new WaitForSecondsRealtime(.01f);
    //     }

    // }

}


