using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[DisallowMultipleComponent]
public class EnigmaPhysics : NetworkBehaviour
{
    [System.NonSerialized]
    public Rigidbody physBody;
    public bool multiplayer;

    [Header("Physics")]
        public Vector3 referenceVector = Vector3.up;
        [Range(0,100)]
        public float gravityForce;
        public bool grounded;
        public LayerMask collisionLayers;
        public int characterState;

        [System.NonSerialized]
        public int characterState_copy;

        [Range(0.0001f,2f)]
        public float rayDistance = 0.5f;
        public bool interpolateNormals;
        [Range(0,1)]
        public float normalLerp;
        
        public float softCapDecel;
        //[SerializeField]
        public Vector3 normal;// { get; set; }

        [System.NonSerialized]
        public Vector3 raycastNormal;

        public float angleCutoff;
        [Range(0,1)]
        public float pointLerp;
        public Vector3 point;// { get; set; }

        [System.NonSerialized]
        public Vector3 raycastPoint;

        [System.NonSerialized]
        public bool groundStick = true;

        public bool movingPlatformFix;
        [Range(0,100)]
        public float movingPlatformMultiplier;

        [System.NonSerialized]
        public float activeTurnRate;

    [Header("Movement")]
        [Header("Grounded")]
        public AnimationCurve acceleration;
        public float deceleration;
        public float maxAcceleration;
        public float softSpeedCap;
        public float hardSpeedCap;
        [Space(5)]
        public float turnRate;
        [Space(10)]
        //[Range(0,100)]
        public AnimationCurve upwardsSlopeMultiplier;
        //[Range(0,100)]
        public AnimationCurve downwardsSlopeMultiplier;
        [Header("Airborne")]
        public float airAcceleration;
        [Space(5)]
        public float airTurnRate;

        [Range(-5,5)]
        public float transitionRate;
        [Range(-5,5)]
        public float groundToAirTransition;
        public bool angleTransition = true;
        
        
    
    
    public RaycastHit hit;
    public Vector3 objStandRot;
    public Vector3 objStandPos;

    [System.NonSerialized]
    public Vector3 forCopy;

    void Start()
    {
       //if(!IsOwner) return;
        //Debug.Log(IsOwner);
            //IsOwner = true;
        physBody = transform.GetComponent<Rigidbody>();
        characterState_copy = characterState;
    }

    void Update()
    {
        //if(!IsOwner) return;
        /*if(Physics.Raycast(transform.position+transform.up*.525f,-transform.up,out hit,rayDistance))
        {
            if(Vector3.Angle(normal,interpolateNormals ? Vector3.Slerp(raycastNormal,InterpolateNormal(hit),normalLerp*Time.deltaTime*60) : Vector3.Slerp(raycastNormal,hit.normal,normalLerp*Time.deltaTime*60))<angleCutoff)
            {
                raycastNormal = interpolateNormals ? Vector3.Slerp(raycastNormal,InterpolateNormal(hit),normalLerp*Time.deltaTime*60) : Vector3.Slerp(raycastNormal,hit.normal,normalLerp*Time.deltaTime*60);
                raycastPoint = hit.point;
            }
        }
        grounded = Physics.CheckCapsule(transform.position-(transform.up*(rayDistance/2))+transform.up*.525f,transform.position+transform.up*.5f,.05f,collisionLayers); //Physics.CheckCapsule(transform.position+transform.up*.25f,transform.position-transform.up*.2f,.1f,collisionLayers);

        if(characterState_copy != characterState)
        {
            StartCoroutine(StateTrigger(characterState_copy));
            characterState_copy = characterState;
        }

        switch(characterState)
        {
            case 0:

                break;
            case 1:
                if(grounded == false)
                    characterState = 2;
                
                normal = raycastNormal;
                point = raycastPoint;

                float gravityMultiplier = Mathf.Clamp(Vector3.Angle(referenceVector,normal)/90,0,1);
                
                if(movingPlatformFix == true)
                {
                    if(hit.transform.gameObject.GetComponent<MovingPlatform>())
                    {
                        MovingPlatform platformScript = hit.transform.gameObject.GetComponent<MovingPlatform>();
                        raycastPoint += -platformScript.posVelo*1; 
                    }
                    
                }

                Vector3 localPoint = transform.InverseTransformPoint(raycastPoint);
                if(!hit.transform.gameObject.GetComponent<MovingPlatform>())
                {
                    localPoint.x = 0;
                    localPoint.z = 0;
                }
                if(groundStick == true)
                    transform.position = Vector3.Lerp(transform.position,transform.TransformPoint(localPoint),pointLerp*Time.deltaTime*60);
                
                

                Vector3 velocityRight = Vector3.Cross(normal,physBody.velocity.normalized);
                Vector3 normalVelocity = Vector3.Cross(velocityRight,normal).normalized;
                physBody.velocity = normalVelocity * physBody.velocity.magnitude;
                
                transform.up = normal;
                Vector3 gravityRight = Vector3.Cross(normal,referenceVector).normalized;
                Vector3 normalGravity = Vector3.Cross(normal,gravityRight).normalized;
                Debug.DrawRay(transform.position,gravityRight,Color.magenta );
                Debug.DrawRay(transform.position,normalGravity,Color.red);
                float angleSwitch = Vector3.Angle(normalVelocity,normalGravity)/180;
                Debug.Log(angleSwitch); 
                physBody.velocity += normalGravity * Time.deltaTime * Mathf.Lerp(downwardsSlopeMultiplier*gravityMultiplier,upwardsSlopeMultiplier*gravityMultiplier,angleSwitch);
                
                break;
            case 2:
                if(grounded == true)
                    characterState = 1;
                normal = Vector3.up;
                point = transform.position;
                transform.up = Vector3.Slerp(transform.up,Vector3.up,.2f*Time.deltaTime*60);
                Vector3 turnVector = Vector3.Cross(referenceVector,Vector3.Cross(physBody.velocity,referenceVector)).normalized;
                physBody.velocity += -referenceVector * gravityForce * Time.deltaTime;

                
                
                break;
        }*/
    }

    
    void FixedUpdate()
    {
        //if(!IsOwner) return;

        if(Physics.Raycast(physBody.position+transform.up*.525f,-transform.up,out hit,rayDistance,collisionLayers))
        {
            if(Vector3.Angle(normal,interpolateNormals ? Vector3.Slerp(raycastNormal,InterpolateNormal(hit),normalLerp) : Vector3.Slerp(normal,hit.normal,normalLerp))<angleCutoff)
            {
                raycastNormal = interpolateNormals ? Vector3.Slerp(raycastNormal,InterpolateNormal(hit),normalLerp) : Vector3.Slerp(raycastNormal,hit.normal,normalLerp);
                raycastPoint = hit.point;
                grounded = Physics.CheckCapsule(physBody.position-(transform.up*(rayDistance/2))+transform.up*.525f,physBody.position+transform.up*.5f,.05f,collisionLayers); //Physics.CheckCapsule(transform.position+transform.up*.25f,transform.position-transform.up*.2f,.1f,collisionLayers);
            }
            else
            {
                Debug.Log(Vector3.Angle(raycastNormal,interpolateNormals ? Vector3.Slerp(raycastNormal,InterpolateNormal(hit),normalLerp) : Vector3.Slerp(raycastNormal,hit.normal,normalLerp)));
            }
        }
        else
        {
            grounded = false;
            //Debug.Log("Raycast Failed!");
        }

        

        if(characterState_copy != characterState)
        {
            StartCoroutine(StateTrigger(characterState_copy));
            characterState_copy = characterState;
        }


        switch(characterState)
        {
            case 0:

                break;
            case 1:
                physBody.isKinematic = false;
                if(grounded == false)
                    characterState = 2;
                
                normal = raycastNormal;
                point = raycastPoint;

                float gravityMultiplier = Mathf.Clamp(Vector3.Angle(referenceVector,normal)/90,0,1);
                //Debug.Log(gravityMultiplier);
                
                /*if(movingPlatformFix == true)
                {
                    if(hit.transform.gameObject.GetComponent<MovingPlatform>())
                    {
                        MovingPlatform platformScript = hit.transform.gameObject.GetComponent<MovingPlatform>();
                        raycastPoint += -platformScript.posVelo*1; 
                    }
                    
                } */

                Vector3 localPoint = transform.InverseTransformPoint(raycastPoint);
                //if(!hit.transform.gameObject.GetComponent<MovingPlatform>())
                //{
                    localPoint.x = 0;
                    localPoint.z = 0;
                //}
                if(groundStick == true)
                    physBody.position = Vector3.Lerp(physBody.position,transform.TransformPoint(localPoint),pointLerp);
                
                

                Vector3 velocityRight = Vector3.Cross(normal,physBody.velocity.normalized);
                Vector3 normalVelocity = Vector3.Cross(velocityRight,normal).normalized;
                Vector3 floorVelocity = hit.rigidbody.velocity;
                physBody.velocity = normalVelocity + physBody.velocity.magnitude;
                
                transform.up = normal;
                Vector3 gravityRight = Vector3.Cross(normal,referenceVector).normalized;
                Vector3 normalGravity = Vector3.Cross(normal,gravityRight).normalized;
                Debug.DrawRay(physBody.position,gravityRight,Color.magenta );
                Debug.DrawRay(physBody.position,normalGravity,Color.red);
                float angleSwitch = Vector3.Angle(normalVelocity,normalGravity)/180;
                //Debug.Log(angleSwitch); 
                float upwardsSlope = upwardsSlopeMultiplier.Evaluate(Vector3.Angle(referenceVector,normal));
                float downwardsSlope = downwardsSlopeMultiplier.Evaluate(Vector3.Angle(referenceVector,normal));
                physBody.velocity += normalGravity * Time.fixedDeltaTime * Mathf.Lerp(downwardsSlope*gravityMultiplier,upwardsSlope*gravityMultiplier,angleSwitch);
                
                //Debug.Log();

                if(physBody.velocity.magnitude > softSpeedCap)
                {
                    physBody.velocity = Vector3.Lerp(physBody.velocity,physBody.velocity.normalized * softSpeedCap,softCapDecel);
                }
                
                if(physBody.velocity.magnitude > hardSpeedCap)
                {
                    physBody.velocity = physBody.velocity.normalized * hardSpeedCap;
                }

                Vector3 localVelocity = transform.InverseTransformDirection(physBody.velocity);
                localVelocity.y = 0;
                activeTurnRate = Vector3.SignedAngle(forCopy,transform.TransformDirection(localVelocity),transform.up);
                forCopy = transform.TransformDirection(localVelocity);
                //Debug.Log(activeTurnRate);
                

                break;
            case 2:
                physBody.isKinematic = false;
                if(grounded == true)
                    characterState = 1;

                normal = Vector3.Slerp(normal,referenceVector,.06f);
                point = physBody.position;
                transform.up = Vector3.Slerp(transform.up,referenceVector,.06f);
                Vector3 turnVector = Vector3.Cross(referenceVector,Vector3.Cross(physBody.velocity,referenceVector)).normalized;
                physBody.velocity += -referenceVector * gravityForce * Time.fixedDeltaTime;

                Vector3 localVelo = transform.InverseTransformDirection(physBody.velocity);
                localVelo.y = 0;
                activeTurnRate = Vector3.SignedAngle(forCopy,transform.TransformDirection(localVelo),transform.up);
                forCopy = transform.TransformDirection(localVelo);

                break;   
        }
    }

    IEnumerator StateTrigger(float oldState)
    {
        Debug.Log("Switched from Character State " + oldState + " to " + characterState  );
        switch(characterState)
        {
            case 1:
                if(oldState == 2)
                {
                    float hitAngle = Mathf.Clamp(Vector3.Angle(physBody.velocity,normal)/90,0,1);
                    if(angleTransition == false)
                    {
                        physBody.velocity = physBody.velocity.normalized * (physBody.velocity.magnitude * transitionRate);
                    }
                    else
                    {
                        physBody.velocity = physBody.velocity.normalized * hitAngle * (physBody.velocity.magnitude * transitionRate);
                    }
                }

                break;
            case 2:
                if(oldState == 1)
                {
                    physBody.velocity = physBody.velocity.normalized * physBody.velocity.magnitude * groundToAirTransition;
                }
                break;
        }
        yield return null;
    }


    Vector3 InterpolateNormal(RaycastHit iHit)
    {

        MeshCollider meshCollider = iHit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            return iHit.normal;
            Debug.Log("Abort Mission, using hit.normal instead");
        }

        Mesh mesh = meshCollider.sharedMesh;

        if(mesh.isReadable == false)
        {
            return iHit.normal;
            Debug.Log("Abort Mission, using hit.normal instead");
        }

        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;

        Vector3 scale = hit.transform.lossyScale;
        float maxVal = Mathf.Max(Mathf.Max(scale.x, scale.y), scale.z);
        scale = new Vector3(scale.x/maxVal,scale.y/maxVal,scale.z/maxVal);
        scale = new Vector3(1/Mathf.Abs(scale.x),1/Mathf.Abs(scale.y),1/Mathf.Abs(scale.z)); //create vector to "correct" for scale
        Vector3 scaleFull = new Vector3(1/scale.x,1/scale.y,1/scale.z);
        //This is only a bandaid solution and gets less accurate the more you skew a mesh, if you know how to get the actual normals after scaling please contact me at
        // n.dx on Discord or NDXDirectorsCut on Twitter

        //Debug.Log(scale);

        // Extract local space normals of the triangle we hit
        Vector3 n0 = normals[triangles[iHit.triangleIndex * 3 + 0]];
        Vector3 n1 = normals[triangles[iHit.triangleIndex * 3 + 1]];
        Vector3 n2 = normals[triangles[iHit.triangleIndex * 3 + 2]];

        // interpolate using the barycentric coordinate of the hitpoint
        Vector3 baryCenter = iHit.barycentricCoordinate;

        n0 = Vector3.Scale(n0,scale); //scale each normal to the bandaid vector
        n1 = Vector3.Scale(n1,scale);
        n2 = Vector3.Scale(n2,scale);
        
        baryCenter = Vector3.Scale(baryCenter,scaleFull);
        // Use barycentric coordinate to interpolate normal
        Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
        //interpolatedNormal = Vector3.Scale(interpolatedNormal,scale);
        
        // normalize the interpolated normal
        interpolatedNormal = interpolatedNormal.normalized;
        
        
        // Transform local space normals to world space
        Transform hitTransform = iHit.collider.transform;
        
        interpolatedNormal = hitTransform.TransformDirection(interpolatedNormal);
        return interpolatedNormal;
    }
}
