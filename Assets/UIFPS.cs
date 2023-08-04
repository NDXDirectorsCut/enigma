using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFPS : MonoBehaviour
{
    public TMP_Text fpsUIElement;
    int curFPS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curFPS = (int)(1f / Time.deltaTime);
        fpsUIElement.text = curFPS.ToString();
    }
}
