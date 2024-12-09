using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class RatingManager : MonoBehaviour
{
    // define the UI objects we draw values from or update.
    public int participantID;
    public int age;
    public string gender;
    public Slider emotionPercieved;
    public Slider emotionEffectiveness;
    public Slider empathyFelt;
    private string emotionShown;
    private string modalityUsed;
    public GameObject thisObject;
    public bool taskRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        thisObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetCurrentTask(string emotion, string modality)
    {
        emotionShown = emotion;
        modalityUsed = modality;
        thisObject.SetActive(true);
    }

    // Function runs when Save Response is clicked, added to the button by adding the Game Object Save Button Manager to the OnClick()
    // element in the inspector

    [ContextMenu("Trigger Confirm Press")]
    public void ConfirmPress()
    {
        // Get the log file name for this participant
        string filename = "P" + participantID.ToString();

        // Append PartID, gender, rating, the image and newline to the log file in CSV format
        File.AppendAllText("ParticipantLogs/" + filename + ".txt", // save as txt file, import to excel as CSV
                            filename + ", " +
                            gender + ", " +
                            age.ToString() + ", " +
                            modalityUsed + ", " +
                            emotionShown + ", " +
                            emotionPercieved.value + ", " +
                            emotionEffectiveness.value + ", " +
                            empathyFelt.value + 
                            "\n");

        // Reset the Likert Scales to neutral 
        emotionPercieved.value = 3;
        emotionEffectiveness.value = 3;
        empathyFelt.value = 3;

        thisObject.SetActive(false);
        taskRunning = false;
    }
}
