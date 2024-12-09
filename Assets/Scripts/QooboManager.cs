using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class QooboManager : MonoBehaviour
{

    public GameObject thisObject;
    public GameObject tail;
    public float tailSpeed;
    public float wagDistance;
    public float tailHeight;
    List<string> emotions = new List<string>(5);
    private Vector3 neutral;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 tailAngle;
    //private bool taskRunning = false;
    string current;
    System.Random rand = new System.Random();
    public RatingManager ratingScript;
    public GameObject endscreen;


    // Start is called before the first frame update
    void Start()
    {
        // neutral position
        neutral = new Vector3(tail.transform.eulerAngles.x, tail.transform.eulerAngles.y, tail.transform.eulerAngles.z);
        emotions.Add("happy");
        emotions.Add("sad");
        emotions.Add("angry");
        emotions.Add("scared");
        emotions.Add("surprised");
        foreach (string emotion in emotions)
        {
            Debug.Log(emotion);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //PingPong between 0 and 1
        //float time = Mathf.PingPong(Time.time * tailSpeed, 1);
        //transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
    }

    [ContextMenu("Trigger Tail Change")]
    public async void DisplayTail()
    {
        if (!ratingScript.taskRunning)
        { // prevent function interrupting itself if triggered multiple times
            if (emotions.Count == 0)
            { // check that the list isnt empty, if it is, display Log
                Debug.Log(emotions.Count);
                EndRound();
            }
            else
            {
                ratingScript.taskRunning = true;
                await Task.Delay(750); // wait 7 seconds before rating
                
                int index = rand.Next(emotions.Count); // select a random string from the list
                current = emotions[index]; // set current to that string
                emotions.Remove(current); // remove the current material from the list, leaving one less for the next script call
                ChangeTail(current); // call ChangeMaterial to change face to current material
                
                                       // call rating GUI
                await Task.Delay(7000); // wait 7 seconds before rating
                //taskRunning = false;
                ratingScript.SetCurrentTask(current, "tail");
                StopAllCoroutines();
                ResetTail();
            }
        }
        else
        {
            Debug.Log("Tail Task Running");
        }
    }

    public void ChangeTail(string emotion)
    {
        switch (emotion)
        {
            case "happy":
                HappyTailWag();
                break;
            case "sad":
                SadTailWag();
                break;
            case "angry":
                AngryTailWag();
                break;
            case "surprised":
                SurprisedTailWag();
                break;
            case "scared":
                ScaredTailWag();
                break;
            default:
                Debug.Log("no tail emotion case found");
                break;
        }
    }

    [ContextMenu("EndRound")]
    public void EndRound()
    {
        Debug.Log("All Emotions Displayed!!");
        endscreen.SetActive(true);
    }

    public void ToggleObjectActive()
    {
        thisObject.SetActive(!thisObject.activeInHierarchy);
    }




    public void ResetTail()
    {
        tail.transform.eulerAngles = neutral;
    }

    public void HappyTailWag()
    {
        StartCoroutine(HappyTailWagCo());
    }

    IEnumerator HappyTailWagCo()
    {
        ResetTail();
        wagDistance = 35;
        tailSpeed = 2;
        tailHeight = -15;

        pointA = transform.eulerAngles + new Vector3(0f, wagDistance, tailHeight);
        pointB = transform.eulerAngles + new Vector3(0f, -wagDistance, tailHeight);

        while (true)
        {
            float time = Mathf.PingPong(Time.time * tailSpeed, 1);
            tail.transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
            //timePassed += Time.deltaTime;
            yield return null;
        }
    }
    public void SadTailWag()
    {
        StartCoroutine(SadTailWagCo());
    }

    IEnumerator SadTailWagCo()
    {
        ResetTail();
        wagDistance = 14f;
        tailSpeed = 0.3f;
        tailHeight = 10f;

        pointA = transform.eulerAngles + new Vector3(0f, 0f, (tailHeight - wagDistance));
        pointB = transform.eulerAngles + new Vector3(0f, -0f, (tailHeight + wagDistance));

        while (true)
            {
            float time = Mathf.PingPong(Time.time * tailSpeed, 1);
            tail.transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
            //timePassed += Time.deltaTime;
            yield return null;
        }
    }

    public void AngryTailWag()
    {
        StartCoroutine(AngryTailWagCo());
    }

    IEnumerator AngryTailWagCo()
    {
        ResetTail();
        wagDistance = 5f;
        tailSpeed = 5f;
        tailHeight = -25f;

        pointA = transform.eulerAngles + new Vector3(0f, 0f, (tailHeight - wagDistance));
        pointB = transform.eulerAngles + new Vector3(0f, -0f, (tailHeight + wagDistance));

        while (true)
        {
            float time = Mathf.PingPong(Time.time * tailSpeed, 1);
            tail.transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
            //timePassed += Time.deltaTime;
            yield return null;
        }
    }

    public void SurprisedTailWag()
    {
        StartCoroutine(SurprisedTailWagCo());
    }

    IEnumerator SurprisedTailWagCo()
    {
        ResetTail();
        wagDistance = 2f;
        tailSpeed = 5f;
        tailHeight = -35f;

        pointA = transform.eulerAngles + new Vector3(0f, wagDistance, (tailHeight));
        pointB = transform.eulerAngles + new Vector3(0f, -wagDistance, (tailHeight));


        while (true)
        {
            float time = Mathf.PingPong(Time.time * tailSpeed, 1);
            tail.transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
            //timePassed += Time.deltaTime;
            yield return null;
        }
    }

    public void ScaredTailWag()
    {
        StartCoroutine(ScaredTailWagCo());
    }

    IEnumerator ScaredTailWagCo()
    {
        ResetTail();
        wagDistance = 1f;
        tailSpeed = 5f;
        tailHeight = 20f;

        pointA = transform.eulerAngles + new Vector3(0f, wagDistance, (tailHeight));
        pointB = transform.eulerAngles + new Vector3(0f, -wagDistance, (tailHeight));


        while (true)
        {
            float time = Mathf.PingPong(Time.time * tailSpeed, 1);
            tail.transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
            //timePassed += Time.deltaTime;
            yield return null;
        }
    }

}
