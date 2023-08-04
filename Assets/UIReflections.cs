using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReflections : MonoBehaviour
{
    public Toggle uiToggle;
    public GameObject valueObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        valueObject.SetActive(uiToggle.isOn);
        if(Input.GetKeyDown(KeyCode.R))
        {
            uiToggle.isOn = !uiToggle.isOn;
        }
    }
}
