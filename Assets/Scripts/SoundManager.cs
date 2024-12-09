using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class SoundManager : MonoBehaviour
{

    public AudioSource happy;
    public AudioSource sad;
    public AudioSource angry;
    public AudioSource scared;
    public AudioSource surprised;
    private bool taskRunning = false;
    AudioSource current;
    List<AudioSource> sounds = new List<AudioSource>();
    System.Random rand = new System.Random();
    public RatingManager ratingScript;
    public GameObject endscreen;



    // Start is called before the first frame update
    void Start()
    {
        sounds.Add(happy); // Add the face materials to the list
        sounds.Add(sad);
        sounds.Add(angry);
        sounds.Add(scared);
        sounds.Add(surprised);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ContextMenu("Trigger Sound Change")]
    public async void DisplaySound()
    {
        if (!ratingScript.taskRunning)
        { // prevent function interrupting itself if triggered multiple times
            if (sounds.Count == 0)
            { // check that the list isnt empty, if it is, display Log
                EndRound();
            }
            else
            {
                ratingScript.taskRunning = true;
                await Task.Delay(750);
                
                int index = rand.Next(sounds.Count); // select a random material from the list
                current = sounds[index]; // set current to that material
                sounds.Remove(current); // remove the current material from the list, leaving one less for the next script call
                current.Play(); // call ChangeMaterial to change face to current material
                
                                       // call rating GUI
                await Task.Delay(7000); // wait 5 seconds before rating
                //taskRunning = false;
                ratingScript.SetCurrentTask(current.name, "sound");
                //current.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Sound Task Running");
        }
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        Debug.Log("All Emotions Displayed!!");
        endscreen.SetActive(true);
    }
}
