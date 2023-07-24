using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleScript : MonoBehaviour
{
    public bool Additive;
    public bool startWithRandomOffset;

    [Range(-10,10)]
    public float offset;
    [Space(5)]
    [Header("Position")]
    public Vector3 sinPos;
    public float sinPosSpeed;
    [Space(5)]
    public Vector3 cosPos;
    public float cosPosSpeed;
    [Space(10)]
    [Header("Rotation")]
    public Vector3 sinRot;
    public float sinRotSpeed;
    [Space(5)]
    public Vector3 cosRot;
    public float cosRotSpeed;

    Quaternion startRot;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        if(startWithRandomOffset)
        {
            offset = Random.Range(-10f,10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Additive ? transform.position : startPos;
        Quaternion rot = Additive ? transform.rotation : startRot;
        float time = Time.time + offset;
        transform.position = pos + (sinPos*Mathf.Sin(time*sinPosSpeed)) + (cosPos*Mathf.Cos(time*cosPosSpeed));
        transform.rotation = rot * Quaternion.Euler(sinRot*Mathf.Sin(time*sinRotSpeed))* Quaternion.Euler(cosRot*Mathf.Cos(time*cosRotSpeed));
    }
}
