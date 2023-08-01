using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;
    [Range(0,1)]
    public float posLerp;
    [Range(0,1)]
    public float rotLerp;
    [Range(0,1)]
    public float sizLerp;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,target.position,posLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,rotLerp);
        transform.localScale =  Vector3.Lerp(transform.localScale,target.localScale,sizLerp);
    }
}
