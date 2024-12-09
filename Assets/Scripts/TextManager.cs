using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class TextManager : MonoBehaviour
{

    public GameObject happy;
    public GameObject sad;
    public GameObject angry;
    public GameObject scared;
    public GameObject surprised;
    private bool taskRunning = false;
    GameObject current;
    List<GameObject> texts = new List<GameObject>();
    System.Random rand = new System.Random();
    public RatingManager ratingScript;
    public GameObject endscreen;

    // Start is called before the first frame update
    void Start()
    {
        texts.Add(happy); // Add the face materials to the list
        texts.Add(sad);
        texts.Add(angry);
        texts.Add(scared);
        texts.Add(surprised);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Trigger Text Change")]
    public async void DisplayText()
    {
        if (!ratingScript.taskRunning)
        { // prevent function interrupting itself if triggered multiple times
            if (texts.Count == 0)
            { // check that the list isnt empty, if it is, display Log
                EndRound();
            }
            else
            {
                ratingScript.taskRunning = true;
                await Task.Delay(750);
                
                int index = rand.Next(texts.Count); // select a random material from the list
                current = texts[index]; // set current to that material
                texts.Remove(current); // remove the current material from the list, leaving one less for the next script call
                current.SetActive(true); // call ChangeMaterial to change face to current material
                
                                        // call rating GUI
                await Task.Delay(7000); // wait 5 seconds before rating
                //taskRunning = false;
                ratingScript.SetCurrentTask(current.name, "text");
                current.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Text Task Running");
        }
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        Debug.Log("All Emotions Displayed!!");
        endscreen.SetActive(true);
    }
}
