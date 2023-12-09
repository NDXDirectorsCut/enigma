using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class EnigmaAnimator : MonoBehaviour
{
    [Range(0,1)]
    public float positionLerp;
    [Range(0,1)]
    public float rotationLerp;
    public Vector3 modelOffset;
    public EnigmaPhysics enigmaPhysics;
    public Jump jumpScript;
    public Homing homingScript;
    public Skid skidScript;
    public RailInteraction railInt;
    [Range(0,1)]
    public float railAngleMultiplier;
    Rigidbody physBody;
    Transform character;
    [System.NonSerialized]
    public Animator anim;
    Vector3 veloRef;
    Vector3 rightDir;
    Vector3 forwardDir = Vector3.forward;
    [Header("Debug")]
    bool drawDebug;

    // Start is called before the first frame update
    void Start()
    {
        physBody = enigmaPhysics.transform.GetComponent<Rigidbody>();
        jumpScript = enigmaPhysics.transform.GetComponent<Jump>();
        homingScript = enigmaPhysics.transform.GetComponent<Homing>();
        skidScript = enigmaPhysics.transform.GetComponent<Skid>();
        railInt = enigmaPhysics.transform.GetComponent<RailInteraction>();
        character = enigmaPhysics.transform;
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed",physBody.velocity.magnitude);
        anim.SetInteger("Character State",enigmaPhysics.characterState);
        
        Vector3 localVelocity = transform.InverseTransformDirection(physBody.velocity);

        anim.SetFloat("VelocityVertical",localVelocity.y);
        anim.SetFloat("VelocityHorizontal",localVelocity.x);
        anim.SetFloat("VelocityForward",localVelocity.z);
        if(jumpScript != null)
            anim.SetBool("Jumped",jumpScript.jumped);
        if(skidScript != null)
        {
            anim.SetBool("Skidding",skidScript.skidding);
            anim.SetBool("SkidTurn",skidScript.turned);
        }
        if(railInt != null)
            anim.SetFloat("RailSway",railInt.animSway);
        if(homingScript != null)
        {
            anim.SetBool("Homing",homingScript.homingTrigger);
            if(homingScript.dashTrigger == true)
                anim.SetBool("Homing",homingScript.dashTrigger);
        }

        //Vector3 posVector = ;
        //Vector3.Lerp(transform.position,character.position,positionLerp);
       // Debug.DrawRay(character.position,Vector3.Cross(enigmaPhysics.normal,physBody.velocity).normalized,Color.blue);
       switch(enigmaPhysics.characterState)
       {
            case 0:

                break;
            case 1:
            //Debug.Log(localVelocity);
                if(Mathf.Abs(localVelocity.x) > 0.05f || Mathf.Abs(localVelocity.z) > 0.05f)
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
                if(Mathf.Abs(localVelocity.x) > 0.05f || Mathf.Abs(localVelocity.z) > 0.05f)
                {
                    veloRef = physBody.velocity.normalized;

                    /*Vector3*/ 
                    //Debug.DrawRay(character.position,forwardDir,Color.blue);
                    //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,Vector3.up),rotationLerp);
                }
                rightDir = Vector3.Cross(physBody.transform.up,veloRef).normalized;
                forwardDir = -Vector3.Cross(physBody.transform.up,rightDir);
                transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,physBody.transform.up),rotationLerp);
                break;
            case 3:
                forwardDir = physBody.transform.forward;
                //upDir = enigmaPhysics.transform.up;
                transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,Quaternion.AngleAxis(railInt.animSway*railAngleMultiplier,forwardDir) * physBody.transform.up),rotationLerp);
                break;
       }
        if(drawDebug == true)
        {
            Debug.DrawRay(transform.position,veloRef,Color.yellow);
            Debug.DrawRay(transform.position,forwardDir,Color.blue);
            Debug.DrawRay(transform.position,rightDir,Color.red);
            Debug.DrawRay(transform.position,enigmaPhysics.normal,Color.green);
        }
        transform.position = character.position + character.right * modelOffset.x + character.up * modelOffset.y + character.forward * modelOffset.z;
    }
}
