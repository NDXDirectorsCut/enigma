using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Character State Reference List:

    0 - Debug
    1 - Grounded
    2 - Airborne
    
*/

public class BaseMovement : MonoBehaviour
{
    Rigidbody physBody;
    EnigmaPhysics physScript;
    int characterState;
    public GameObject referenceObject;
    public bool smoothInput;
    public Vector3 axisInput {get; set;}

    void Start()
    {
        physScript = transform.GetComponent<EnigmaPhysics>();
        physBody = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        characterState = physScript.characterState;
        GetInput();
        switch(characterState)
        {
            //GetInput();

            case 0:

                break;
            case 1:
                
                float turnAngle = Vector3.SignedAngle(axisInput,physBody.velocity,physScript.normal);
                physBody.velocity = Quaternion.AngleAxis(-turnAngle * Time.deltaTime * physScript.turnRate ,physScript.normal ) * physBody.velocity;

                float gravityMultiplier = Vector3.Angle(physScript.referenceVector,physScript.normal)/90;
                float accelMultiplier = Mathf.Clamp(Vector3.Angle(axisInput,physScript.referenceVector)/90,0,1);
                //Debug.Log(accelMultiplier);
                if(physBody.velocity.magnitude < physScript.maxAcceleration)
                {
                    physBody.velocity += axisInput*physScript.acceleration * accelMultiplier * Time.deltaTime;
                }

                if(axisInput.magnitude<0.2f)
                {
                    physBody.velocity += -physBody.velocity.normalized * (1-gravityMultiplier) * physScript.deceleration * Time.deltaTime;
                    physBody.velocity = Vector3.Lerp(physBody.velocity,Vector3.zero,0.0125f);
                    if(physBody.velocity.magnitude<0.2f)
                    {
                        physBody.velocity = Vector3.zero;
                    }
                }
                
                //Debug.Log(turnAngle);
                break;
            case 2:

                break;
        }
    }

    void FixedUpdate()
    {
        switch(characterState)
        {
            //GetInput();

            case 0:

                break;
            case 1:
                

                break;
            case 2:

                break;
        }
    }

    void GetInput()
    {
        float hor = smoothInput ? Input.GetAxis("Horizontal") : Input.GetAxisRaw("Horizontal");
        float ver = smoothInput ? Input.GetAxis("Vertical") : Input.GetAxisRaw("Vertical");
        axisInput = new Vector3(hor,0,ver);
        axisInput = Quaternion.FromToRotation(referenceObject.transform.up,physScript.normal) * referenceObject.transform.TransformDirection(axisInput);
        Debug.DrawRay(transform.position,axisInput,Color.yellow);
    }
}
