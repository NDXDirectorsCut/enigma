using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISpeed : MonoBehaviour
{
    public Rigidbody physBody;
    public TMP_Text speedUIElement;
    // Start is called before the first frame update
    void Start()
    {
        speedUIElement.text = "sex";
    }

    // Update is called once per frame
    void Update()
    {
        int speed = (int)physBody.velocity.magnitude;
        speedUIElement.text = speed.ToString();
    }
}
