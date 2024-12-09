using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCondition : MonoBehaviour
{

    public string scene;
    // Start is called before the first frame update
    void Start()
    {

    }    // Update is called once per frame
    void Update()
    {
       
    }

    [ContextMenu("Load Scene")]
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }


}
