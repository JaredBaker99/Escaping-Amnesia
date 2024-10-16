using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("OverworldAudio");

        // //When loading back into the MainMenu scene, destroy objectd
        // Scene scene = SceneManager.GetActiveScene();
        // Debug.Log("" + scene.name);
        // if (scene.name == "MainMenu")
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        //If more than one object exists
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);




    }

    public void ChangedScenes()//Used to destroy objects when needed (mainmenu)
    {
        Destroy(this.gameObject);
    }
}
