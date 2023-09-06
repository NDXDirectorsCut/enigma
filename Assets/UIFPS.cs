using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFPS : MonoBehaviour
{
    public TMP_Text fpsUIElement;
    public TMP_Text maxFpsUIElement;
    public float refreshRate;
    int curFPS;
    int min,max;
    bool canUpdate = true;
    // Start is called before the first frame update
    void Start()
    {
        max = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curFPS = (int)(1f / Time.deltaTime);
        fpsUIElement.text = curFPS.ToString();
        if(curFPS > max)
        {
            max = curFPS;
            
        }
        if(canUpdate == true)
            StartCoroutine(RefreshValues());
    }

    IEnumerator RefreshValues()
    {
        if(canUpdate == true)
        {
            canUpdate = false;
            maxFpsUIElement.text = max.ToString();
            max = 0;
            yield return new WaitForSecondsRealtime(refreshRate);
            canUpdate = true;
        }
    }
}
