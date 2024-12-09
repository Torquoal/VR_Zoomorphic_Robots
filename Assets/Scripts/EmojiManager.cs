using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EmojiManager : MonoBehaviour
{

    public GameObject happy;
    public GameObject sad;
    public GameObject angry;
    public GameObject scared;
    public GameObject surprised;
    //private bool taskRunning = false;
    GameObject current;
    List<GameObject> emojis = new List<GameObject>();
    System.Random rand = new System.Random();
    public RatingManager ratingScript;
    public GameObject endscreen;

    // Start is called before the first frame update
    void Start()
    {
        emojis.Add(happy); // Add the face materials to the list
        emojis.Add(sad);
        emojis.Add(angry);
        emojis.Add(scared);
        emojis.Add(surprised);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Trigger Emoji Change")]
    public async void DisplayEmoji()
    {
        if (!ratingScript.taskRunning)
        { // prevent function interrupting itself if triggered multiple times
            if (emojis.Count == 0)
            { // check that the list isnt empty, if it is, display Log
                EndRound();
            }
            else
            {
                ratingScript.taskRunning = true;
                await Task.Delay(750);
                
                int index = rand.Next(emojis.Count); // select a random material from the list
                current = emojis[index]; // set current to that material
                emojis.Remove(current); // remove the current material from the list, leaving one less for the next script call
                current.SetActive(true); // call ChangeMaterial to change face to current material
                
                                        // call rating GUI
                await Task.Delay(7000); // wait 5 seconds before rating
                
                ratingScript.SetCurrentTask(current.name, "emoji");
                current.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Emoji Task Running");
        }
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        Debug.Log("All Emotions Displayed!!");
        endscreen.SetActive(true);
    }
}
