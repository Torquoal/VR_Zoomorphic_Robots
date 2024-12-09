using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class FaceManager : MonoBehaviour
{
    public GameObject QooboTouch;
    public DecalProjector FaceProjector;

    public Material happy;
    public Material sad;
    public Material angry;
    public Material scared;
    public Material surprised;
    public Material neutral;
    //private bool taskRunning = false;
    Material current;
    List<Material> faces = new List<Material>();
    System.Random rand = new System.Random();
    public RatingManager ratingScript;
    public GameObject endscreen;


    // Script which takes all 5 facial decals, then presents them in a random order one at a time each time the script is called until all have been shown
    // TODO: Call rating scales 10s after each face is called and then present a CONDITION OVER screen once exhausted, rather than a log

    // Start is called before the first frame update
    void Start()
    {
        faces.Add(happy); // Add the face materials to the list
        faces.Add(sad);
        faces.Add(angry);
        faces.Add(scared);
        faces.Add(surprised);

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Trigger Face Change")]
    public async void DisplayFace()
    {
        if (!ratingScript.taskRunning) { // prevent function interrupting itself if triggered multiple times
            if (faces.Count == 0) { // check that the list isnt empty, if it is, display Log
                EndRound();
            } else {
                ratingScript.taskRunning = true;
                await Task.Delay(750);
                
                int index = rand.Next(faces.Count); // select a random material from the list
                current = faces[index]; // set current to that material
                faces.Remove(current); // remove the current material from the list, leaving one less for the next script call
                ChangeMaterial(current); // call ChangeMaterial to change face to current material
                
                                       // call rating GUI
                await Task.Delay(7000); // wait 5 seconds before rating
                //taskRunning = false;
                ratingScript.SetCurrentTask(current.name,"face");
                ChangeMaterial(neutral);
            }
        } else {
            Debug.Log("Face Task Running");
        }
    }

    public void ChangeMaterial( Material face)
    {
        FaceProjector.material = face;
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        Debug.Log("All Emotions Displayed!!");
        endscreen.SetActive(true);
    }
}