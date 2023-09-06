using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float radius;
    public Animator springAnim;
    public float force;
    public bool lockPosition;
    public bool yLock;
    public bool lockVelocity;
    public bool lockInput;
    public float lockInputTime;
    float corrector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider touch)
    {
        if(touch.GetComponent<Rigidbody>() != null)
        {
            springAnim.SetTrigger("Extend");
            Rigidbody physBody = touch.GetComponent<Rigidbody>();
            corrector = 1;
            if(touch.GetComponent<EnigmaPhysics>() != null)
            {
                EnigmaPhysics enigmaPhysics = touch.GetComponent<EnigmaPhysics>();
                if(enigmaPhysics.characterState == 1)
                {
                    corrector = 1/enigmaPhysics.groundToAirTransition;
                }
                enigmaPhysics.characterState = 2;
                //enigmaPhysics.rayDistance = 0f;
                enigmaPhysics.grounded = false;
            }
            
            if(lockPosition == true)
            {
                touch.transform.position = transform.position;
            }
            else
            {
                Vector3 localPos = transform.InverseTransformPoint(touch.transform.position);
                //Debug.Log(localPos.y - transform.localPosition.y);
                if(localPos.y - transform.localPosition.y > 0)
                    localPos.y = yLock ? 0 : localPos.y ;
                else
                    localPos.y = 0;


                touch.transform.position = transform.TransformPoint(localPos);
                
            }
            physBody.velocity = lockVelocity ? transform.up * force*corrector : transform.up * (physBody.velocity.magnitude + force) * corrector;
            
        }
    }
}
