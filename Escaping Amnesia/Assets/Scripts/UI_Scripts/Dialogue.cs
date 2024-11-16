using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public OverworldAudioManager OverworldAudio;
    public AudioClip keyboard;
    public string[] lines;
    public float textSpeed;
    private int index;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){//Skip lines
            if(textComponent.text == lines[index]){//Proceeds to next line
                NextLine();
            }
            else{//Skips to the end of the line
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray()){//Prints char by char
            textComponent.text += c;
            OverworldAudio.PlaySFX(keyboard);
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine(){//Goes to next line
        if (index < lines.Length - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{//Once lines are finished, set Dialogue to false and resume
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
