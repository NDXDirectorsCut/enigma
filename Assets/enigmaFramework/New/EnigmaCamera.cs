using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 referenceVector;
    [Range(0.0f, 10.0f)]
    public float cameraDistance = 3.5f;
    public float cameraSpeed;
    [Range(0.0f, 10.0f)]
    public float buffer;

    public LayerMask layers;

    public Vector3 offset;
    [Range(0,1)]
    public float lerp;
    [Range(0,1440)]
    public float forwardAngleDelta;
    //[Range(0,)]
    public float distanceDelta;
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
    void LateUpdate()
    {
        
        mouseX = Mathf.Lerp(mouseX,Input.GetAxis("Mouse X"),lerp*Time.deltaTime*60); 
        mouseY = Mathf.Lerp(mouseY,Input.GetAxis("Mouse Y"),lerp*Time.deltaTime*60);
        Vector3 offsetPos = target.position + (target.right*offset.x) + (target.up*offset.y) + (target.forward*offset.z);

        if(target.gameObject.GetComponent<Rigidbody>())
        {
            Vector3 velo = target.gameObject.GetComponent<Rigidbody>().velocity;
            //Vector3 dir = (target.position+offsetPos) + velo;
            //if(velo.magnitude > 0.2f)
            //{
                //Debug.Log(Vector3.Angle())
                //transform.forward = Vector3.RotateTowards(transform.forward,target.forward)
                transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(offsetPos-transform.position,target.up),forwardAngleDelta*60*Time.deltaTime);
                referenceVector = Vector3.RotateTowards(referenceVector,target.up,forwardAngleDelta*Mathf.Deg2Rad*60*Time.deltaTime,0);
                ///////transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(offsetPos-transform.position,target.up),forwardLerp);
                //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(velo,target.up),forwardLerp*Time.deltaTime*60); //.LookAt(target.position,target.up);// = Vector3.Slerp(transform.forward,velo,forwardLerp);

            //}
            //transform.RotateAround(target.position,Vector3.Cross(transform.forward,velo),Vector3.SignedAngle(transform.forward,velo,Vector3.Cross(transform.forward,velo))*forwardLerp );
            //transform.up = target.up;
        }

        transform.RotateAround(offsetPos,target.up,mouseX*cameraSpeed * Time.deltaTime*60);
        transform.RotateAround(offsetPos,transform.right,mouseY*cameraSpeed * Time.deltaTime*60);
        //transform.rotation
        
        //Debug.Log(transform.localRotation);
        if(Physics.Raycast(offsetPos,-transform.forward,out hit, cameraDistance,layers))
        {
            transform.position = Vector3.Lerp(transform.position,hit.point,0.5f*Time.deltaTime*60);//Vector3.MoveTowards(transform.position,hit.point + transform.forward*buffer,60*Time.deltaTime);
            Debug.DrawRay(hit.point,hit.normal,Color.blue);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,offsetPos - (transform.forward * cameraDistance),distanceDelta*Time.deltaTime*60);//Vector3.Lerp(transform.position,offsetPos - (transform.forward * cameraDistance),posLerp*Time.deltaTime*60);
        }
        Application.targetFrameRate = targetFPS;
    }
}