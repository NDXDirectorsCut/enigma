using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject sonic;
    public GameObject shadow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Keypad1))
        {
            shadow.SetActive(false);
            sonic.SetActive(true);
        }    
        if(Input.GetKey(KeyCode.Keypad2))
        {
            sonic.SetActive(false);
            shadow.SetActive(true);
        }
    }
}
