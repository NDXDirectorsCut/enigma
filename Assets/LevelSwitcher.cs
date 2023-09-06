using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    int currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Loaded Level " + currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Loading New Scene...");
            SceneManager.LoadScene(currentScene-1,LoadSceneMode.Single);
        }
        if(Input.GetKey(KeyCode.E))
        {
            Debug.Log("Loading New Scene...");
            SceneManager.LoadScene(currentScene+1,LoadSceneMode.Single);
        }
    }
}
