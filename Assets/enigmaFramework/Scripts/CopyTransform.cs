using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;
    public bool useFixedUpdate;
    public Vector3 offset;
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
        if(useFixedUpdate == false)
        {
            transform.position = Vector3.Lerp(transform.position, target.position+target.up*offset.y + target.right * offset.x + target.forward *offset.z ,posLerp);
            transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,rotLerp);
            transform.localScale =  Vector3.Lerp(transform.localScale,target.localScale,sizLerp);
        }
    }

    void FixedUpdate()
    {
        if(useFixedUpdate == true)
        {
            transform.position = Vector3.Lerp(transform.position, target.position+target.up*offset.y + target.right * offset.x + target.forward *offset.z ,posLerp);
            transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,rotLerp);
            transform.localScale =  Vector3.Lerp(transform.localScale,target.localScale,sizLerp);
        }
    }
}
