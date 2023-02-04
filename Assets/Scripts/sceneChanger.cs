using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void singlePlayer()
    {
        SceneManager.LoadScene(1);
    }
    public void multiPlayer()
    {
        Debug.Log("That Feature is not available yet");
        //SceneManager.LoadScene(2);
    }
    public void options()
    {
        Debug.Log("That Feature is not available yet");
    }
    public void quit()
    {
        Application.Quit();
    }
}
