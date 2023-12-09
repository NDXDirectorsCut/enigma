using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skid : MonoBehaviour
{
    public EnigmaPhysics enigmaPhysics;
    public BaseMovement baseMove;

    Vector3 axisInput;
    public float angleRequired = 75f;
    public float minSpeed;
    public bool skidding = false;
    public bool turned;
    public bool ended;
    float angle;
    public float lockInputTime = 0.125f;
    Vector3 skidAxis;
    public float holdSpeed;
    public bool holdChange;
    [Range(0,1)]
    public float speedPercentage;

    


    // Start is called before the first frame update
    void Start()
    {
        angleRequired = Mathf.Clamp(angleRequired,0,90);
    }

    // Update is called once per frame
    void Update()
    {
        float normalAngle = Vector3.Angle(enigmaPhysics.referenceVector,enigmaPhysics.normal);
        if(enigmaPhysics.characterState == 1)
        {
            
            if(normalAngle <= 75f && enigmaPhysics.physBody.velocity.magnitude > minSpeed)
            {
                
                axisInput = baseMove.axisInput;
                angle = Vector3.Angle(axisInput,enigmaPhysics.physBody.velocity);
                if(angle >angleRequired)
                {
                    if(holdChange == false)
                    {
                        holdChange = true;
                        holdSpeed = enigmaPhysics.physBody.velocity.magnitude;
                    }
                    skidAxis = axisInput;
                    skidding = true;
                    baseMove.canMove = false;
                    //Debug.Log(angle);

                }
                
            }
            if(skidding == true )
            {
                enigmaPhysics.physBody.velocity += -Vector3.ClampMagnitude(enigmaPhysics.physBody.velocity,1f) * (1-Vector3.Angle(enigmaPhysics.referenceVector, enigmaPhysics.normal)/90) * enigmaPhysics.deceleration * Time.fixedDeltaTime;//Vector3.Lerp(enigmaPhysics.physBody.velocity,Vector3.zero,.025f*Time.deltaTime*60);
                if(enigmaPhysics.physBody.velocity.magnitude < minSpeed)
                {
                    enigmaPhysics.physBody.velocity = Vector3.Slerp(enigmaPhysics.physBody.velocity,Vector3.zero,.125f);
                    StartCoroutine(EndSkid());
                }
            } 
        }
        else if ( enigmaPhysics.characterState != 1 || normalAngle > 75f)
        {
            baseMove.canMove = true;
            skidding = false;
        }
    }

    IEnumerator EndSkid()
    {
        if(ended == false)
        {
            ended = true;
            if(Vector3.Angle(axisInput.normalized,skidAxis) < 20f && axisInput.sqrMagnitude != 0 )
            {
                turned = true;
            }
            yield return new WaitForSeconds(lockInputTime);
            holdChange = false;
            enigmaPhysics.physBody.velocity = skidAxis.normalized * holdSpeed * speedPercentage; 
            baseMove.canMove = true;
            turned = false;
            skidding = false;
            ended = false;
        }
    }
}
