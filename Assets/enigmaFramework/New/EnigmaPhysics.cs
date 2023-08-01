using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnigmaPhysics : MonoBehaviour
{
    [System.NonSerialized]
    public Rigidbody physBody;

    [Header("Physics")]
        public Vector3 referenceVector = Vector3.up;
        [Range(0,100)]
        public float gravityForce;
        public bool grounded;
        public LayerMask collisionLayers;
        public int characterState;
        int characterState_copy;
        [Range(0.0001f,2f)]
        public float rayDistance = 0.5f;
        public bool interpolateNormals;
        [Range(0,1)]
        public float normalLerp;
        //[SerializeField]
        public Vector3 normal;// { get; set; }
        Vector3 raycastNormal;
        public float angleCutoff;
        [Range(0,1)]
        public float pointLerp;
        public Vector3 point;// { get; set; }
        Vector3 raycastPoint;
        //[System.NonSerialized]
        public bool groundStick = true;

    [Header("Movement")]
        [Header("Grounded")]
        public float acceleration;
        public float deceleration;
        public float maxAcceleration;
        [Space(5)]
        public float turnRate;
        [Header("Airborne")]
        public float airAcceleration;
        [Space(5)]
        public float airTurnRate;

        [Range(-5,5)]
        public float transitionRate;
        public bool angleTransition = true;
        
        
    
    
    public RaycastHit hit;
    public Vector3 objStandRot;

    void Start()
    {
        physBody = transform.GetComponent<Rigidbody>();
        characterState_copy = characterState;
    }

    void Update()
    {
        if(Physics.Raycast(transform.position+transform.up*.525f,-transform.up,out hit,rayDistance))
        {
            if(Vector3.Angle(normal,interpolateNormals ? Vector3.Slerp(raycastNormal,InterpolateNormal(hit),normalLerp*Time.deltaTime*60) : Vector3.Slerp(raycastNormal,hit.normal,normalLerp*Time.deltaTime*60))<angleCutoff)
            {
                raycastNormal = interpolateNormals ? Vector3.Slerp(raycastNormal,InterpolateNormal(hit),normalLerp*Time.deltaTime*60) : Vector3.Slerp(raycastNormal,hit.normal,normalLerp*Time.deltaTime*60);
                raycastPoint = hit.point;
                //Debug.DrawRay(hit.point,normal,Color.red);
            }
            //Debug.DrawRay(hit.point,hit.normal,Color.blue);
        }
        //Debug.Log(gravityMultiplier);
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
                
                Vector3 localPoint = transform.InverseTransformPoint(raycastPoint);
                localPoint.x = 0;
                localPoint.z = 0;
                //Debug.Log(localPoint);
                if(groundStick == true)
                    transform.position = Vector3.Lerp(transform.position,transform.TransformPoint(localPoint),pointLerp*Time.deltaTime*60);
                
                if(hit.rigidbody != null)
                {
                    Debug.Log((objStandRot - hit.transform.eulerAngles) * Time.deltaTime*60);
                    hit.rigidbody.angularVelocity = (objStandRot - hit.transform.eulerAngles) * Time.deltaTime*60;
                    objStandRot = hit.transform.eulerAngles;
                }

                //physBody.velocity = Quaternion.FromToRotation(transform.up,normal) * physBody.velocity;
                Vector3 velocityRight = Vector3.Cross(normal,physBody.velocity.normalized);
                //Debug.DrawRay(transform.position,velocityRight,Color.green);
                Vector3 normalVelocity = Vector3.Cross(velocityRight,normal).normalized;
                //Debug.DrawRay(transform.position,normalVelocity * physBody.velocity.magnitude,Color.blue);
                //Debug.DrawRay(transform.position,physBody.velocity.normalized * physBody.velocity.magnitude,Color.cyan);
                //Debug.DrawRay(transform.TransformPoint(physBody.centerOfMass),Vector3.Cross(physBody.velocity,referenceVector).normalized,Color.magenta);
                physBody.velocity = normalVelocity * physBody.velocity.magnitude;
                
                transform.up = normal;
                physBody.velocity += -referenceVector.normalized * gravityForce * gravityMultiplier * Time.deltaTime;
                
                break;
            case 2:
                if(grounded == true)
                    characterState = 1;
                normal = Vector3.up;
                point = transform.position;
                transform.up = Vector3.Slerp(transform.up,Vector3.up,.2f*Time.deltaTime*60);
                Vector3 turnVector = Vector3.Cross(referenceVector,Vector3.Cross(physBody.velocity,referenceVector)).normalized;
                //Debug.DrawRay(transform.TransformPoint(physBody.centerOfMass),turnVector,Color.magenta);
                //transform.RotateAround(physBody.worldCenterOfMass,turnVector,Vector3.Angle(transform.up,referenceVector));
                physBody.velocity += -referenceVector * gravityForce * Time.deltaTime;

                
                
                break;
        }
    }

    void FixedUpdate()
    {
        switch(characterState)
        {
            case 0:

                break;
            case 1:
                
                //physBody.velocity = Quaternion.FromToRotation(transform.up,normal) * physBody.velocity;
                //transform.position = Vector3.Lerp(transform.position,point,pointLerp);
                break;
            case 2:

                break;   
        }
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position+transform.up*.2f,.22f);
    }
    */

    IEnumerator StateTrigger(float oldState)
    {
        Debug.Log("Switched from Character State " + oldState + " to " + characterState  );
        switch(characterState)
        {
            case 1:
                if(oldState == 2)
                {
                    //Debug.Log();
                    float hitAngle = Mathf.Clamp(Vector3.Angle(physBody.velocity,-normal)/90,0,1);
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
        }
        yield return null;
    }


    Vector3 InterpolateNormal(RaycastHit iHit)
    {
        // Just in case, also make sure the collider also has a renderer
        // material and texture
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

        //Debug.Log(Vector3.ClampMagnitude(hit.transform.lossyScale,Mathf.Sqrt(3) ));
        Vector3 scale = hit.transform.lossyScale;
        float maxVal = Mathf.Max(Mathf.Max(scale.x, scale.y), scale.z);
        //Debug.Log( );
        scale = new Vector3(scale.x/maxVal,scale.y/maxVal,scale.z/maxVal);
        //Debug.Log(scale);
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
        
        //Debug.DrawRay(iHit.point,interpolatedNormal,Color.green);

        interpolatedNormal = hitTransform.TransformDirection(interpolatedNormal);
        //Debug.DrawRay(baryCenter,interpolatedNormal,Color.red);
        // Display with Debug.DrawLine
        return interpolatedNormal;
    }
}
