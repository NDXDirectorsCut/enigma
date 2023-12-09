using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;
    public bool useFixedUpdate;
    public Vector3 offset;
    //[Range(0,1)]
    public Vector3 posLerp;
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
        if(useFixedUpdate == false)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x,target.position.x,posLerp.x),Mathf.Lerp(transform.position.y,target.position.y,posLerp.y),Mathf.Lerp(transform.position.z,target.position.z,posLerp.z));
            transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,rotLerp);
            transform.localScale =  Vector3.Lerp(transform.localScale,target.localScale,sizLerp);
        }
    }

    void FixedUpdate()
    {
        if(useFixedUpdate == true)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x,target.position.x,posLerp.x),Mathf.Lerp(transform.position.y,target.position.y,posLerp.y),Mathf.Lerp(transform.position.z,target.position.z,posLerp.z));
            transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,rotLerp);
            transform.localScale =  Vector3.Lerp(transform.localScale,target.localScale,sizLerp);
        }
    }
}
