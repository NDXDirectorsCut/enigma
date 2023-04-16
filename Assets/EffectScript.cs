using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public GameObject jumpEffect;
    public JumpTest jumpScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpScript.jumped == true)
        {
            jumpEffect.SetActive(true);
        }
        if(jumpScript.canJump == true)
        {
            jumpEffect.SetActive(false);
        }
    }
}
