using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightManager : MonoBehaviour
{

    public GameObject happy;
    public GameObject sad;
    public GameObject angry;
    public GameObject scared;
    public GameObject surprised;
    private bool taskRunning = false;
    GameObject current;
    List<GameObject> lights = new List<GameObject>();
    System.Random rand = new System.Random();
    public RatingManager ratingScript;
    public GameObject endscreen;

    // Start is called before the first frame update
    void Start()
    {
        lights.Add(happy); // Add the face materials to the list
        lights.Add(sad);
        lights.Add(angry);
        lights.Add(scared);
        lights.Add(surprised);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Trigger Light Change")]
    public async void DisplayLight()
    {
        if (lights.Count == 0)
        { // check that the list isnt empty, if it is, display Log
            EndRound();
        } else if (!ratingScript.taskRunning)
        { // prevent function interrupting itself if triggered multiple times

            ratingScript.taskRunning = true;
            await Task.Delay(750);
                
                int index = rand.Next(lights.Count); // select a random material from the list
                current = lights[index]; // set current to that material
                lights.Remove(current); // remove the current material from the list, leaving one less for the next script call
                current.SetActive(true); // call ChangeMaterial to change face to current material
                
                                        // call rating GUI
                await Task.Delay(7000); // wait 5 seconds before rating
                //taskRunning = false;
                ratingScript.SetCurrentTask(current.name, "light");
                current.SetActive(false);
            
        }
        else
        {
            Debug.Log("Light Task Running");
        }
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        Debug.Log("All Emotions Displayed!!");
        endscreen.SetActive(true);
    }
}
