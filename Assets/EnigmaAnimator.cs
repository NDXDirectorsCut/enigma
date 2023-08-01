using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaAnimator : MonoBehaviour
{
    [Range(0,1)]
    public float positionLerp;
    [Range(0,1)]
    public float rotationLerp;
    public EnigmaPhysics enigmaPhysics;
    public Jump jumpScript;
    Rigidbody physBody;
    Transform character;
    Animator anim;
    Vector3 veloRef;
    Vector3 rightDir;
    Vector3 forwardDir;
    // Start is called before the first frame update
    void Start()
    {
        physBody = enigmaPhysics.transform.GetComponent<Rigidbody>();
        jumpScript = enigmaPhysics.transform.GetComponent<Jump>();
        character = enigmaPhysics.transform;
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetFloat("Speed",physBody.velocity.magnitude);
        anim.SetInteger("Character State",enigmaPhysics.characterState);
        anim.SetBool("Jumped",jumpScript.jumped);
        Vector3 localVelocity = transform.InverseTransformDirection(physBody.velocity);
        anim.SetFloat("VelocityVertical",localVelocity.y);
        anim.SetFloat("VelocityHorizontal",localVelocity.x);
        anim.SetFloat("VelocityForward",localVelocity.z);
        Vector3 posVector = Vector3.Lerp(transform.position,character.position,positionLerp);
        transform.position = posVector;
       // Debug.DrawRay(character.position,Vector3.Cross(enigmaPhysics.normal,physBody.velocity).normalized,Color.blue);
       switch(enigmaPhysics.characterState)
       {
            case 0:

                break;
            case 1:
                if(physBody.velocity.magnitude > 0.2f)
                {
                    veloRef = physBody.velocity.normalized;
                    // /*Vector3*/ rightDir = Vector3.Cross(enigmaPhysics.normal,physBody.velocity).normalized;
                    // /*Vector3*/ forwardDir = -Vector3.Cross(enigmaPhysics.normal,Vector3.Cross(enigmaPhysics.normal,physBody.velocity).normalized);
                    //Debug.DrawRay(character.position,forwardDir,Color.blue);
                    //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,enigmaPhysics.normal),rotationLerp);
                }
                rightDir = Vector3.Cross(enigmaPhysics.normal,veloRef).normalized;
                forwardDir = -Vector3.Cross(enigmaPhysics.normal,rightDir).normalized;
                
                transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,enigmaPhysics.normal),rotationLerp);
                break;
            case 2:
                if(physBody.velocity.magnitude > 0.2f)
                {
                    veloRef = physBody.velocity.normalized;
                    /*Vector3*/ 
                    //Debug.DrawRay(character.position,forwardDir,Color.blue);
                    //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,Vector3.up),rotationLerp);
                }
                rightDir = Vector3.Cross(Vector3.up,veloRef).normalized;
                forwardDir = -Vector3.Cross(Vector3.up,rightDir);
                transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,enigmaPhysics.normal),rotationLerp);
                break;
       }
        Debug.DrawRay(transform.position,veloRef,Color.yellow);
        Debug.DrawRay(transform.position,forwardDir,Color.blue);
        Debug.DrawRay(transform.position,rightDir,Color.red);
        Debug.DrawRay(transform.position,enigmaPhysics.normal,Color.green);
    }
}
