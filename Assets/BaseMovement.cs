using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

/* Character State Reference List:

    0 - Debug
    1 - Grounded
    2 - Airborne
    
*/

public class BaseMovement : NetworkBehaviour
{
    Rigidbody physBody;
    public EnigmaPhysics physScript;
    int characterState;
    public GameObject referenceObject;
    public bool smoothInput;
    public Vector3 axisInput {get; set;}
    public bool canMove;

    void Start()
    {
        physScript = transform.GetComponent<EnigmaPhysics>();
        physBody = transform.GetComponent<Rigidbody>();
        Debug.Log(Time.fixedDeltaTime);
    }

    void Update()
    {
        characterState = physScript.characterState;
        GetInput();
        /*
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
                    if(physBody.velocity.sqrMagnitude<1f)
                        physBody.velocity = Vector3.Lerp(physBody.velocity,Vector3.zero,0.0125f*Time.deltaTime*60);
                    if(physBody.velocity.sqrMagnitude<0.2f*0.2f)
                    {
                        physBody.velocity = Vector3.zero;
                    }
                }
                
                //Debug.Log(turnAngle);
                break;
            case 2:
                float airTurnAngle = Vector3.SignedAngle(axisInput,physBody.velocity,physScript.normal);
                physBody.velocity = Quaternion.AngleAxis(-airTurnAngle * Time.deltaTime * physScript.airTurnRate,physScript.normal ) * physBody.velocity;
                physBody.velocity += axisInput*physScript.airAcceleration * Time.deltaTime;
                break;
        }
        */
    }

    void FixedUpdate()
    {
        Vector3 clampedVelocity = Vector3.ClampMagnitude(physBody.velocity,1f);

            switch(characterState)
            {
                //GetInput();

                case 0:

                    break;
                case 1:
                    if(canMove == true)
                    {
                        float turnAngle = Vector3.SignedAngle(axisInput,physBody.velocity,physScript.normal);
                        //Debug.Log();
                        //float multiplier = (2 - Mathf.Clamp(Vector3.Angle(physBody.velocity,axisInput)/90,0,2))/2;
                        //Debug.Log(multiplier);
                        physBody.velocity = Quaternion.AngleAxis(-turnAngle * Time.fixedDeltaTime * physScript.turnRate /* multiplier*/ , physScript.normal) * physBody.velocity;
                    }
                    float gravityMultiplier = Vector3.Angle(physScript.referenceVector, physScript.normal)/90;
                    float accelMultiplier = Mathf.Clamp(Vector3.Angle(axisInput,physScript.referenceVector)/90,0,1);
                    //Debug.Log(accelMultiplier);
                    if(physBody.velocity.magnitude < physScript.maxAcceleration && canMove == true )
                    {
                        physBody.velocity += axisInput*physScript.acceleration.Evaluate(physBody.velocity.magnitude/physScript.softSpeedCap) * accelMultiplier * Time.fixedDeltaTime;
                    }

                    if(axisInput.magnitude<0.2f || canMove == false )
                    {
                        physBody.velocity += -clampedVelocity * (1-gravityMultiplier) * physScript.deceleration * Time.fixedDeltaTime;
                        if(physBody.velocity.sqrMagnitude<1f)
                            physBody.velocity = Vector3.Lerp(physBody.velocity,Vector3.zero,0.1f);
                        if(physBody.velocity.sqrMagnitude<0.2f*0.2f)
                        {
                            physBody.velocity = Vector3.zero;
                        }
                    }

                    break;
                case 2:

                    float airTurnAngle = Vector3.SignedAngle(axisInput,physBody.velocity,physScript.normal);
                    physBody.velocity = Quaternion.AngleAxis(-airTurnAngle * Time.fixedDeltaTime * physScript.airTurnRate,physScript.normal ) * physBody.velocity;
                    physBody.velocity += axisInput*physScript.airAcceleration * Time.fixedDeltaTime;

                    break;
            }
    }

    void GetInput()
    {
        float hor = smoothInput ? Input.GetAxis("Horizontal") : Input.GetAxisRaw("Horizontal");
        float ver = smoothInput ? Input.GetAxis("Vertical") : Input.GetAxisRaw("Vertical");
        axisInput = new Vector3(hor,0,ver);
        axisInput = Quaternion.FromToRotation(referenceObject.transform.up,physScript.normal) * referenceObject.transform.TransformDirection(axisInput);
        axisInput = Vector3.ClampMagnitude(axisInput,1);

        Debug.DrawRay(transform.position,axisInput,Color.yellow);
    }
}
