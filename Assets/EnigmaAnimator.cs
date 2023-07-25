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
        character = enigmaPhysics.transform;
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetFloat("Speed",physBody.velocity.magnitude);
        anim.SetInteger("Character State",enigmaPhysics.characterState);
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
                    /*Vector3*/ rightDir = Vector3.Cross(Vector3.up,physBody.velocity).normalized;
                    /*Vector3*/ forwardDir = -Vector3.Cross(Vector3.up,rightDir);
                    //Debug.DrawRay(character.position,forwardDir,Color.blue);
                    //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,Vector3.up),rotationLerp);
                }
                transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forwardDir,enigmaPhysics.normal),rotationLerp);
                break;
       }
        
    }
}