using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    public RaycastHit hit;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if( Physics.SphereCast(transform.position,radius,-transform.up,out hit,0) )
        {
            Debug.DrawRay(transform.position,hit.normal,Color.blue);
            Debug.Log(hit);
        }
    }
}
