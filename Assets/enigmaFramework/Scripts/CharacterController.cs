using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Player player;
    Transform pTransform;
    Rigidbody physBody;
    CapsuleCollider charCol;
    public Vector3 axisInput {get; set;}
    RaycastHit hit;

    public float lerp;
    public bool stickToGround;
    public bool forceDebug;
    public bool canMove;
    public int returnState;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pTransform = player.physBody.transform;
        physBody = player.physBody;    
        charCol = pTransform.GetComponent<CapsuleCollider>();
    }
    
    void Update()
    {
        GetInput();
        player.grounded = Physics.CheckCapsule(pTransform.position-(pTransform.up*lerp),pTransform.position+pTransform.up*.5f,.05f,player.collisionLayers);

        if(Physics.Raycast(pTransform.position+pTransform.up*.25f,-pTransform.up,out hit,.5f,player.collisionLayers))
        {
            if(Vector3.Angle(pTransform.up,hit.normal)<90)
            {
                player.normal = Vector3.Slerp(player.normal,hit.normal,player.debugValue);
                //if(stickToGround==true)
                    //pTransform.position = hit.point;
            }
            if(player.grounded == true)
            {
                physBody.velocity = Quaternion.FromToRotation(pTransform.up,player.normal) * physBody.velocity;
                pTransform.up = player.normal;
            }
        }
    }

    void FixedUpdate()
    {
            if(player.grounded == true)
            {
                player.characterState = 1;
                float gravityAngle;
                float accAngle = Mathf.Clamp(Vector3.Angle(axisInput,player.referenceVector)/180,0,1);
                //Debug.Log(accAngle);

                charCol.height = player.groundColliderHeight;
                charCol.radius = player.groundColliderRadius;
                charCol.center = new Vector3(0,player.groundColliderHeight/2,0);

                if(Vector3.Angle(player.referenceVector,player.normal)<90)
                {
                    gravityAngle = Vector3.Angle(player.referenceVector,player.normal)/90;
                    gravityAngle = Mathf.Clamp(gravityAngle,0,1);
                    //physBody.AddForce(-Vector3.up * (Vector3.Angle(Vector3.up,player.normal)/90));
                }
                else
                {
                    gravityAngle = (180-Vector3.Angle(player.referenceVector,player.normal))/90;
                    gravityAngle = Mathf.Clamp(gravityAngle,0,1);
                    //physBody.AddForce( -Vector3.up * ((180-Vector3.Angle(Vector3.up,player.normal))/90) );
                }

                physBody.velocity+= -player.referenceVector * gravityAngle * Time.deltaTime * player.slopeForce;

                if(physBody.velocity.magnitude<player.maxAccel && canMove == true)
                {
                    physBody.velocity+= axisInput*player.acceleration*accAngle;
                }
                if(axisInput.magnitude>0.1f)
                {
                    float angleMultiplier = 1;//(1-(Vector3.Angle(physBody.velocity,axisInput)/160))*1.25f;
                    angleMultiplier = Mathf.Clamp(angleMultiplier,0,1);
                    //Debug.Log(angleMultiplier);
                    physBody.velocity = Vector3.Slerp(physBody.velocity,axisInput.normalized * physBody.velocity.magnitude,player.turnValue * accAngle * angleMultiplier);
                }
                else
                {
                    
                    if(physBody.velocity.magnitude>player.acceleration)
                    {
                        Vector3 decelVector = Vector3.ClampMagnitude(physBody.velocity,1);
                        physBody.velocity += -decelVector * player.deceleration * (1-gravityAngle)  ;//(1-(Vector3.Angle(player.referenceVector,player.normal)/90));
                    }
                    if(physBody.velocity.magnitude<=player.acceleration)
                        physBody.velocity = Vector3.Lerp(physBody.velocity,Vector3.zero,(1-(Vector3.Angle(player.referenceVector,player.normal)/90)));
                }
                
                /*
                if(physBody.velocity.magnitude >0.5f)
                {
                    player.characterState = 2;
                }
                else 
                {
                    player.characterState = 1;
                }*/
            }
            else
            {
                player.characterState = 2;
                player.normal = player.referenceVector;
                pTransform.up = Vector3.Slerp(pTransform.up,player.referenceVector,.2f);
                physBody.velocity+= axisInput*player.airAcceleration;
                physBody.velocity+= -player.referenceVector * player.gravityForce * Time.deltaTime;
                //physBody.AddForce(-Vector3.up*10);

                charCol.height = player.airColliderHeight;
                charCol.radius = player.airColliderRadius;
                charCol.center = new Vector3(0,player.airColliderHeight/2,0);
            }
    }

    /*
    void Update()
    {
        GetInput();
        player.grounded = Physics.CheckCapsule(pTransform.position-(pTransform.up*lerp),pTransform.position+pTransform.up*.5f,.05f,player.collisionLayers);
        if(Physics.Raycast(pTransform.position+pTransform.up*.25f,-pTransform.up,out hit, 10f,player.collisionLayers))
        {
            if(Vector3.Angle(player.normal,hit.normal)<90)
            {
                player.normal = hit.normal;
                player.point = hit.point;
            }
        }

        switch(player.characterState)
        {
            case 0: //Debugging State
                physBody.isKinematic = true;
                player.normal = Vector3.up;
                pTransform.up = player.normal;
                pTransform.position += axisInput;
                
                if(Input.GetKey(KeyCode.Space))
                {
                    pTransform.position += Vector3.up;
                }
                if(Input.GetKey(KeyCode.LeftControl))
                {
                    pTransform.position += -Vector3.up;
                }                

                break;

            case 1:
                physBody.isKinematic = false;
                if(player.grounded == false)
                {
                    player.characterState = 2;
                }
                break;

            case 2:
                physBody.isKinematic = false;
                if(player.grounded == true)
                {
                    player.characterState = 1;
                }
                break;

        }

        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            if(player.characterState == 0)
            {
                player.characterState = returnState;
            }
            else
                player.characterState = 0;
        }
    }

    void FixedUpdate()
    {
        switch(player.characterState)
        {
            case 0:

                break;

            case 1:
                float gravityAngle;

                if(canMove == true)
                {
                    physBody.velocity += axisInput * player.acceleration; 
                }

                if(Vector3.Angle(Vector3.up,player.normal)<90)
                {
                    gravityAngle = Vector3.Angle(Vector3.up,player.normal)/45;
                    gravityAngle = Mathf.Clamp(gravityAngle,0,1);
                    //physBody.AddForce(-Vector3.up * (Vector3.Angle(Vector3.up,player.normal)/90));
                }
                else
                {
                    gravityAngle = (180-Vector3.Angle(Vector3.up,player.normal))/45;
                    gravityAngle = Mathf.Clamp(gravityAngle,0,1);
                    //physBody.AddForce( -Vector3.up * ((180-Vector3.Angle(Vector3.up,player.normal))/90) );
                }
                physBody.velocity += -player.referenceVector * gravityAngle * Time.deltaTime * player.slopeForce;

                physBody.velocity = Quaternion.FromToRotation(pTransform.up,player.normal) *  physBody.velocity;
                pTransform.position = player.point;
                pTransform.up = player.normal;
                break;
            case 2:
                player.normal = Vector3.up;
                pTransform.up = Vector3.Slerp(pTransform.up,Vector3.up,.2f);
                physBody.velocity+= axisInput*player.airAcceleration;
                physBody.velocity += -player.referenceVector * Time.deltaTime * 10;
                break;
        }
    }

    */









































    /*
     //Old Physics
    void Update()
    {
        GetInput();
        player.grounded = Physics.CheckCapsule(pTransform.position,pTransform.position+pTransform.up*.5f,.05f,player.collisionLayers);
        if(player.grounded==true)
        {
            if(Physics.Raycast(pTransform.position+pTransform.up*.25f,-pTransform.up,out hit,10f,player.collisionLayers))
            {
                if(Vector3.Angle(pTransform.up,hit.normal)<50f)
                {
                    Debug.DrawRay(hit.point,hit.normal,Color.yellow);
                    player.normal = hit.normal;
                }
                else
                {
                    player.normal = Vector3.up;
                }
            }
            physBody.velocity = Quaternion.FromToRotation(pTransform.up,player.normal) * physBody.velocity;
            pTransform.up = Vector3.Slerp(pTransform.up,player.normal,player.debugValue);
            if(physBody.velocity.magnitude>.1f)
            {
                player.characterState=2;
            }
            else
            {
                player.characterState=1;
            }
        }
        else
        {
            player.normal = Vector3.up;
            pTransform.up = Vector3.up;
            player.characterState=3;
        }
    }

    void FixedUpdate()
    {
        if(player.grounded==true)
        {
            //physBody.AddForce(-player.normal*100);
            player.maxSpeed = Vector3.Angle(Vector3.up,player.normal);
            if(axisInput.magnitude>0.1f)
            {
                if(physBody.velocity.magnitude<player.maxAccel)
                    physBody.velocity += axisInput*player.acceleration;
                if(Vector3.Angle(physBody.velocity,axisInput.normalized)>125 && Vector3.Angle(Vector3.up,player.normal)<=80)
                {
                    Debug.Log("Stay In Place and Turn");
                    float holdSpeed = physBody.velocity.magnitude;
                    physBody.velocity = Vector3.zero;
                    physBody.velocity = axisInput.normalized*holdSpeed/2;
                }
                physBody.velocity = Vector3.Slerp(physBody.velocity,axisInput.normalized*physBody.velocity.magnitude,player.turnValue);
            }
            else
            {
                if(physBody.velocity.magnitude>0.25f)
                    physBody.velocity += -physBody.velocity.normalized * player.deceleration * (1-(Vector3.Angle(Vector3.up,player.normal)/90));
                if(physBody.velocity.magnitude<0.25f)
                    physBody.velocity = Vector3.Lerp(physBody.velocity,Vector3.zero,player.deceleration*(1-(Vector3.Angle(Vector3.up,player.normal)/90)));
            }
        }
        else
        {
            physBody.velocity+= axisInput*player.airAcceleration;
        }
    }*/
    
    void GetInput()
    {
        axisInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        axisInput = Quaternion.FromToRotation(player.cameraTransform.up,player.normal) *player.cameraTransform.TransformDirection(axisInput);
        axisInput = Vector3.ClampMagnitude(axisInput,1);
        player.axisInput = axisInput;
    }
}