using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SplineMesh {
    public class RailInteraction : MonoBehaviour
    {
        public EnigmaPhysics enigmaPhysics;
        public BaseMovement moveScript;

        public Spline rail;
        EnigmaRail railScript;

        public bool canGrind = true;
        public float grindTime = 0.1f;

        public float position;

        public bool backwards;
        public bool additiveSwaySystem;
        bool canExit;

        [Range(-100,100)]
        
        public float gravityInfluence;
        public float alongAngle;
        public float alongSpeed;
    
        public float swayAngle;
        public float swaySensitivity;
        [System.NonSerialized]
        public float animSway;
        public float turnSpeed;

        Vector3 axisInput {get; set;}
        Vector3 localAxis {get; set;}
        Vector3 oldTangent;
        Vector3 swayUp;
        float inputSway;

        // Start is called before the first frame update
        void Start()
        {
            if( gameObject.GetComponent<EnigmaPhysics>() != null )
            {
                enigmaPhysics = gameObject.GetComponent<EnigmaPhysics>();
                moveScript = gameObject.GetComponent<BaseMovement>();   
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(enigmaPhysics != null && enigmaPhysics.characterState != 3)
            {
                rail = null;
            }

            if(rail != null && rail.transform.parent.gameObject.GetComponent<EnigmaRail>() != null)
            {
                

                canExit = true;

                railScript = rail.transform.parent.gameObject.GetComponent<EnigmaRail>();
                position = Mathf.Clamp(position,0,rail.Length);

                if(transform.GetComponent<Rigidbody>())
                {
                    
                    transform.GetComponent<Rigidbody>().isKinematic = true;
                }

                CurveSample sample = rail.GetSampleAtDistance(position);//rail.GetProjectionSample(railScript.transform.InverseTransformPoint(transform.position));
                Vector3 railUp = sample.Rotation * Vector3.up;
                Vector3 railRight = Vector3.Cross(sample.tangent,Vector3.up).normalized;
                Vector3 globalForward = -Vector3.Cross(railRight,Vector3.up).normalized;

                Vector3 localRight = Vector3.Cross(railUp,sample.tangent);

                float gravityMultiplier;
                if (enigmaPhysics != null)
                    gravityMultiplier = Vector3.Angle(railUp,enigmaPhysics.referenceVector)/90;
                else
                    gravityMultiplier = Vector3.Angle(railUp,Vector3.up)/90;

            
                turnSpeed = Mathf.SmoothStep(turnSpeed,Vector3.SignedAngle(oldTangent,localRight,railUp)*swaySensitivity,Time.fixedDeltaTime*8 * railScript.handling);

                if(alongSpeed != 0)
                {
                    //turnSpeed = turnSpeed/alongSpeed;
                }

                Debug.DrawRay(transform.position,localRight,Color.red);
                Debug.DrawRay(transform.position,oldTangent,Color.blue);
                Debug.DrawRay(transform.position,railUp,Color.green);

                oldTangent = localRight;

                GetInput(railUp,railRight);

                if(additiveSwaySystem == false)
                {
                    //Debug.Log(axisInput);

                    inputSway = Mathf.SmoothStep(inputSway,-localAxis.x,Time.fixedDeltaTime*16 * railScript.handling);
                    inputSway = Mathf.Clamp(inputSway,-1,1);
                    //Debug.Log(inputSway);
                
                    float normalizedSpeed = Mathf.Clamp(alongSpeed,-1,1);

                    if(backwards == false)
                    {
                        
                        swayAngle = inputSway*90 + (turnSpeed * normalizedSpeed);
                        swayAngle = Mathf.Clamp(swayAngle,-90,90);

                        //swayUp = Vector3.Slerp(swayUp,Quaternion.AngleAxis(swayAngle,sample.tangent) * railUp,.25f);
                        transform.rotation = Quaternion.LookRotation(sample.tangent,railUp);
                        animSway = swayAngle;
                    }
                    else
                    {
                        swayAngle = -inputSway*90 + (turnSpeed * normalizedSpeed);
                        swayAngle = Mathf.Clamp(swayAngle,-90,90);

                        //swayUp = Vector3.Slerp(swayUp,Quaternion.AngleAxis(swayAngle,sample.tangent) * railUp,.25f);
                        transform.rotation = Quaternion.LookRotation(-sample.tangent,railUp);
                        animSway = -swayAngle;
                    }
                }
                else
                {

                }

                alongAngle = Vector3.SignedAngle(sample.tangent,globalForward,railRight);
                enigmaPhysics.normal = railUp;

                float swayDeviance = (1-Mathf.Abs(swayAngle)/90);
                //Debug.Log(swayDeviance);

                alongSpeed += alongAngle*Time.fixedDeltaTime*gravityInfluence;
                alongSpeed = Mathf.Lerp(alongSpeed,0,railScript.friction*(1-gravityMultiplier));
                alongSpeed = Mathf.Lerp(alongSpeed,0,(1-swayDeviance)/100);
                //Debug.Log((1-swayDeviance)/125);

                position += (Time.fixedDeltaTime * alongSpeed);
                //transform.GetComponent<Rigidbody>().velocity = sample.tangent * alongSpeed;
                  //sample.Rotation;
                transform.position = Vector3.Scale(rail.transform.localScale,rail.transform.position) + sample.location + railUp*railScript.railHeight;

                if(rail.IsLoop == false)
                {
                    if( position <= 0 || position >= rail.Length)
                    {
                        rail = null;
                        canGrind = false;
                        StartCoroutine(Regrind());
                        transform.GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetComponent<Rigidbody>().velocity = sample.tangent.normalized * alongSpeed;
                        //railExit = true;
                    }
                }
                else if(rail.IsLoop == true)
                {
                    //Debug.Log("LoopRail");
                    if(position >= rail.Length) 
                        position = position-rail.Length;
                    else if(position <= 0)
                        position = rail.Length + position;
                }

                
                //transform.position += sample.tangent.normalized * (Time.fixedDeltaTime * alongSpeed);
            }
            if (rail == null && canExit == true)
            {
                if(enigmaPhysics != null)
                {
                    enigmaPhysics.characterState = 2;
                        //enigmaPhysics.physBody.isKinematic = false;
                }
                canExit = false;
            }
        }

        IEnumerator Regrind()
        {
            if(canGrind == false)
            {
                yield return new WaitForSeconds(grindTime);
                canGrind = true;
            }
            //yield return void;
        }

        void GetInput(Vector3 up,Vector3 right)
        {
            if(enigmaPhysics != null && moveScript != null)
            {
                //Vector3 tempRight = Vector3.Cross(up,Vector3.forward);
                //Vector3 corrUp = Vector3.Cross(Vector3.forward,tempRight);
                //Vector3 normalRight = Vector3.Cross(corrUp,Vector3.forward);
                axisInput = Quaternion.FromToRotation(enigmaPhysics.normal,up) * moveScript.axisInput;
                localAxis = transform.InverseTransformDirection(axisInput);
                Debug.DrawRay(transform.position,localAxis*2,Color.red);
            }
        }

    }

    
}