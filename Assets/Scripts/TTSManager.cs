using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class TTSManager : MonoBehaviour
{

    public AudioSource happy;
    public AudioSource sad;
    public AudioSource angry;
    public AudioSource scared;
    public AudioSource surprised;
    private bool taskRunning = false;
    AudioSource current;
    List<AudioSource> tts = new List<AudioSource>();
    System.Random rand = new System.Random();
    public RatingManager ratingScript;
    public GameObject endscreen;

    // Start is called before the first frame update
    void Start()
    {
        tts.Add(happy); // Add the face materials to the list
        tts.Add(sad);
        tts.Add(angry);
        tts.Add(scared);
        tts.Add(surprised);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Trigger TTS Change")]
    public async void DisplayTTS()
    {
        if (!ratingScript.taskRunning)
        { // prevent function interrupting itself if triggered multiple times
            if (tts.Count == 0)
            { // check that the list isnt empty, if it is, display Log
                EndRound();
            }
            else
            {
                ratingScript.taskRunning = true;
                await Task.Delay(750);
                
                int index = rand.Next(tts.Count); // select a random material from the list
                current = tts[index]; // set current to that material
                tts.Remove(current); // remove the current material from the list, leaving one less for the next script call
                current.Play(); // call ChangeMaterial to change face to current material
                
                                        // call rating GUI
                await Task.Delay(7000); // wait 5 seconds before rating
                //taskRunning = false;
                ratingScript.SetCurrentTask(current.name, "tts");
                //current.SetActive(false);
            }
        }
        else
        {
            Debug.Log("TTS Task Running");
        }
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        Debug.Log("All Emotions Displayed!!");
        endscreen.SetActive(true);
    }
}
