using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySwitcher : MonoBehaviour
{
    int i = 0;
   // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Changing Quality...");
            i--;
            QualitySettings.SetQualityLevel(i, true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Changing Quality...");
            i++;
             QualitySettings.SetQualityLevel(i, true);
        }
        i = Mathf.Clamp(i,0,QualitySettings.names.Length-1);
    }
}
