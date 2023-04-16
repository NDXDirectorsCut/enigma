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
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            if(forceDebug == true)
            {
                forceDebug = false;
                physBody.isKinematic = false;
            }
            else
            {
                forceDebug = true;
                player.characterState = 0;
                physBody.isKinematic = true;
                player.normal = Vector3.up;
                pTransform.position = new Vector3(Mathf.Floor(pTransform.position.x),Mathf.Floor(pTransform.position.y),Mathf.Floor(pTransform.position.z));
            }
        }
        if(player.characterState == 0)
        {
            pTransform.position += axisInput*1;
            if(Input.GetKey(KeyCode.Space))
            {
                pTransform.position += Vector3.up*1;
            }
            if(Input.GetKey(KeyCode.LeftControl))
            {
                pTransform.position += -Vector3.up*1;
            }
        }

        if(forceDebug == false)
        {
            player.grounded = Physics.CheckCapsule(pTransform.position-(pTransform.up*lerp),pTransform.position+pTransform.up*.5f,.05f,player.collisionLayers);
            if(Physics.Raycast(pTransform.position+pTransform.up*.25f,-pTransform.up,out hit,.5f,player.collisionLayers))
            {
                if(Vector3.Angle(pTransform.up,hit.normal)<90)
                {
                    player.normal = Vector3.Slerp(player.normal,hit.normal,player.debugValue);
                    if(stickToGround==true)
                        pTransform.position = hit.point;
                }
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
        if(forceDebug == false)
        {
            if(player.grounded == true)
            {
                float gravityAngle;
                float accAngle = Mathf.Clamp(Vector3.Angle(axisInput,Vector3.up)/180,0,1);
                //Debug.Log(accAngle);

                charCol.height = 1;
                charCol.radius = .25f;
                charCol.center = new Vector3(0,.5f,0);

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
                physBody.AddForce(-Vector3.up * 22.5f * gravityAngle);
                if(physBody.velocity.magnitude<player.maxAccel)
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
                        physBody.velocity += -decelVector * player.deceleration * (1-(Vector3.Angle(Vector3.up,player.normal)/90));
                    }
                    if(physBody.velocity.magnitude<=player.acceleration)
                        physBody.velocity = Vector3.Lerp(physBody.velocity,Vector3.zero,(1-(Vector3.Angle(Vector3.up,player.normal)/90)));
                }

                if(physBody.velocity.magnitude >0.5f)
                {
                    player.characterState = 2;
                }
                else 
                {
                    player.characterState = 1;
                }
            }
            else
            {
                player.characterState = 3;
                player.normal = Vector3.up;
                pTransform.up = Vector3.Slerp(pTransform.up,Vector3.up,.2f);
                physBody.velocity+= axisInput*player.airAcceleration;
                physBody.AddForce(-Vector3.up*10);

                charCol.center = new Vector3(0,.25f,0);
                charCol.height = .5f;
                charCol.radius = .25f;
            }
        }
    }


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