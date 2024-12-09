using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCalibrate : MonoBehaviour

        
{

    public GameObject CameraRig;
    Vector3 CameraPosition;
    public GameObject Qoobo;
    public GameObject Hand;
    float QooboHeight = 0.041f;
    float QooboWidth = 0.104f;
    // 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("1"))
        {
            CalibrateHeight();
        }

        if (Input.GetKeyUp("2"))
        {
            CalibrateDistance();
        }

        if (Input.GetKeyUp("3"))
        {
            CalibrateWidth();
        }
    }


    [ContextMenu("Reset Camera Position")]
    public void ResetCamera()
    {
        
    }

    [ContextMenu("Calibrate Height")]
    public void CalibrateHeight()
    {
        
        float heightDifference = ((Qoobo.transform.position.y + QooboHeight)  - Hand.transform.position.y);
        Vector3 positionDifference = transform.position;
        positionDifference.y += heightDifference;
        CameraRig.transform.position = positionDifference;
        DontDestroyOnLoad(CameraRig.transform.gameObject);
        

    }

    [ContextMenu("Calibrate Distance")]
    public void CalibrateDistance()
    {

        float distanceDifference = ((Qoobo.transform.position.x + QooboWidth) - Hand.transform.position.x);
        Vector3 positionDifference = transform.position;
        positionDifference.x += distanceDifference;
        CameraRig.transform.position = positionDifference;
        CameraPosition = CameraRig.transform.position;
    }

    [ContextMenu("Calibrate Width")]
    public void CalibrateWidth()
    {

        float widthDifference = ((Qoobo.transform.position.z + QooboWidth) - Hand.transform.position.z);
        Vector3 positionDifference = transform.position;
        positionDifference.z += widthDifference;
        CameraRig.transform.position = positionDifference;
        CameraPosition = CameraRig.transform.position;
    }
}
