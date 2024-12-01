using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public OverworldAudioManager OverworldAudio;
    public AudioClip keyboard;
    public string[] lines;
    public float textSpeed;
    private int index;
    private bool isPlaying = false;


    // Start is called before the first frame update
    void Start(){
         gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlaying == true)
        {
            if (Input.GetMouseButtonDown(0))
            {//Skip lines
                if (textComponent.text == lines[index])
                {//Proceeds to next line
                    NextLine();
                }
                else
                {//Skips to the end of the line
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        isPlaying = true;
        index = 0;
        Time.timeScale = 0f;
        textComponent.text = string.Empty;
        OverworldAudio = FindObjectOfType<OverworldAudioManager>();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {//Prints char by char
            textComponent.text += c;
            OverworldAudio.PlaySFX(keyboard);
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {//Goes to next line
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {//Once lines are finished, set Dialogue to false and resume
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            isPlaying = false;
        }
    }
}
