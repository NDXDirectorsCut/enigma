using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPanel : MonoBehaviour
{
    public float force;
    public bool additive;
    public bool setPosition;
    public bool lockInput;
    public float lockInputTime;
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
        if(touch.GetComponent<Rigidbody>())
        {
            Rigidbody physBody = touch.GetComponent<Rigidbody>();
            if(additive == false)
            {
                physBody.velocity = transform.forward*force;
            }
            else
            {
                physBody.velocity = transform.forward * (force + physBody.velocity.magnitude);
            }
            if(setPosition == true)
            {
                Vector3 localPosition = transform.InverseTransformPoint(touch.transform.position);
                localPosition.x = 0; localPosition.z = 0;
                touch.transform.position = transform.TransformPoint(localPosition); 
            }
            if(lockInput == true)
            {
                if(touch.GetComponent<BaseMovement>())
                {
                    BaseMovement moveScript = touch.GetComponent<BaseMovement>();
                    StartCoroutine(InputLock(moveScript));
                }
            }
        }
    }

    IEnumerator InputLock(BaseMovement baseMovement)
    {
        baseMovement.canMove = false;
        yield return new WaitForSeconds(lockInputTime);
        baseMovement.canMove = true;
    }
}
