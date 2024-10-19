using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoNotDestroy2 : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] uiObject = GameObject.FindGameObjectsWithTag("UI");

        //If more than one object exists
        if (uiObject.Length > 1)
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
