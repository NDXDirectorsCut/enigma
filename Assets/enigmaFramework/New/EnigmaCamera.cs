using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaCamera : MonoBehaviour
{
    public Transform posTarget;
    public Transform lookTarget;
    public Vector3 referenceVector;
    public EnigmaPhysics enigmaPhysics;
    [Range(0.0f, 10.0f)]
    public float cameraDistance = 3.5f;
    public float cameraSpeed;
    [Range(0.0f, 10.0f)]
    public float buffer;

    public LayerMask layers;

    public Vector3 offset;
    [Range(0,1)]
    public float lerp;
    //[Range(0,1)]
    public AnimationCurve rotLerp;
    [Range(0,1)]
    public float horRotLerp;
    [Range(0,1)]
    public float posLerp;
    public bool invertX;
    public bool invertY;
    float mouseX,mouseY;
    RaycastHit hit;
    [Header("Debug")]
    [Range(0, 120)]
    public int targetFPS;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        mouseX = Mathf.Lerp(mouseX,Input.GetAxis("Mouse X"),lerp); 
        mouseY = Mathf.Lerp(mouseY,Input.GetAxis("Mouse Y"),lerp);
        Vector3 offsetPos = posTarget.position + (posTarget.right*offset.x) + (posTarget.up*offset.y) + (posTarget.forward*offset.z);
        Vector3 offsetRot = lookTarget.position + (lookTarget.right*offset.x) + (lookTarget.up * offset.y) + (lookTarget.forward*offset.z);

        //if(posTarget.gameObject.GetComponent<Rigidbody>())
        //{  
            //Vector3 dir = (target.position+offsetPos) + velo;
            //if(velo.magnitude > 0.2f)
            //{
                //Debug.Log(Vector3.Angle())
                //transform.forward = Vector3.RotateTowards(transform.forward,target.forward)
                float rotTransition = rotLerp.Evaluate(Vector3.Angle(enigmaPhysics.referenceVector,lookTarget.up));
                //Debug.Log(Vector3.Angle(referenceVector,lookTarget.up));
                //Debug.DrawRay(transform.position,,Color.blue);
                Vector3 projectedRight =  Vector3.Cross(transform.up,offsetRot-transform.position);
                Vector3 projectedForward = Vector3.Cross(projectedRight,transform.up);
                Debug.DrawRay(transform.position,projectedForward,Color.blue);
                

                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(offsetRot-transform.position,referenceVector),horRotLerp);
                //Debug.DrawRay(transform.position,(offsetRot.position-transform.position).normalized,Color.blue);
                referenceVector = Vector3.Slerp(referenceVector,lookTarget.up,rotTransition);
                //transform.right = Vector3.Lerp(transform.right,projectedRight ,horRotLerp);
                

                ///////transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(offsetPos-transform.position,target.up),forwardLerp);
                //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(velo,target.up),forwardLerp*Time.deltaTime*60); //.LookAt(target.position,target.up);// = Vector3.Slerp(transform.forward,velo,forwardLerp);

            //}
            //transform.RotateAround(target.position,Vector3.Cross(transform.forward,velo),Vector3.SignedAngle(transform.forward,velo,Vector3.Cross(transform.forward,velo))*forwardLerp );
            //transform.up = target.up;
        //}
        //transform.rotation = Quaternion.AngleAxis(mouseX*cameraSpeed,referenceVector) * Quaternion.AngleAxis(mouseY*cameraSpeed,transform.right) * transform.rotation;
        transform.RotateAround(offsetPos,referenceVector,mouseX*cameraSpeed);
        transform.RotateAround(offsetPos,transform.right,mouseY*cameraSpeed);
        //transform.rotation
        
        //Debug.Log(transform.localRotation);
        if(Physics.Raycast(offsetPos,-transform.forward,out hit, cameraDistance,layers))
        {
            transform.position = Vector3.Lerp(transform.position,hit.point+transform.forward*buffer,posLerp);           //hit.point+transform.forward*buffer;//Vector3.MoveTowards(transform.position,hit.point + transform.forward*buffer,60*Time.deltaTime);
            Debug.DrawRay(hit.point,hit.normal,Color.blue);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position,offsetPos - (transform.forward * cameraDistance),posLerp);//Vector3.Lerp(transform.position,offsetPos - (transform.forward * cameraDistance),posLerp*Time.deltaTime*60);
        }
        Application.targetFrameRate = targetFPS;
    }
}