using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPlatform : MonoBehaviour
{
    public Vector3 sinStrength;
    public Vector3 cosStrength;
    Rigidbody physBody;
    Vector3 startPos;

    // Start is called before the first frame update
    void OnEnable()
    {
        startPos = transform.position;
        physBody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float t = Time.time;
        physBody.Move(startPos + sinStrength * Mathf.Sin(t) + cosStrength*Mathf.Cos(t) ,transform.rotation);
        Debug.Log(physBody.velocity);
    }
}
