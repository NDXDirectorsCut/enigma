using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 referenceVector; 
    public CharacterController charControl;
    Player player;
    public float turnSpeed;
    public Transform rotTarget;
    public Transform posTarget;
    public int positionMode; 
    public int rotationMode;
    public float orbitDistance;
    public float collisionMargin;
    public LayerMask collisionLayers;
    public float rotLerp;
    public float posLerp;
    public float targetAngle;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        player = charControl.player;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        referenceVector = player.normal;
        switch(rotationMode)
        {
            case 1: // Copy
            transform.rotation = Quaternion.Slerp(transform.rotation,rotTarget.rotation,rotLerp);
            break;

            case 2: // LookAt
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(rotTarget.position-transform.position,referenceVector),rotLerp);
            float cHor = Input.GetAxis("CameraAxis");
            /*Vector3 rotVector = new Vector3(0,cHor*turnSpeed,0);
            rotVector = Quaternion.FromToRotation(Vector3.up,referenceVector) * rotVector;
            transform.rotation *= Quaternion.Euler(rotVector);*/
            transform.RotateAround(rotTarget.position,referenceVector,cHor*turnSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.FromToRotation(transform.up,rotTarget.up) * transform.rotation * Quaternion.Euler(new Vector3(targetAngle,0,0)),0.05f) ; // 
            break;

        }
        switch(positionMode)
        {
            case 1: // Copy
            transform.position = Vector3.Lerp(transform.position,posTarget.position,posLerp);
            break;

            case 2: // Orbit
            if(Physics.Raycast(posTarget.position,-transform.forward,out hit,orbitDistance,collisionLayers))
            {
                Debug.DrawRay(posTarget.position,-transform.forward*orbitDistance,Color.red);
                float dist = Vector3.Distance(posTarget.position,hit.point);
                transform.position = Vector3.Lerp(transform.position,posTarget.position-transform.forward*(dist-collisionMargin),posLerp);
            }
            else
                transform.position = Vector3.Lerp(transform.position,posTarget.position-transform.forward*orbitDistance,posLerp);
            break;

        }
    }
}
