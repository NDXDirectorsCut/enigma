using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerScript : MonoBehaviour
{
    public Transform camPos;
    public Transform camLook;
    public int posMode;
    public int rotMode; 
    public float rotLerp;
    public float posLerp;
    //Transform cameraTransform;

    void Start()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.parent.gameObject)
        {
            GameObject playerObj = other.transform.parent.gameObject;
            Player player = other.transform.parent.GetComponent<CharacterController>().player;
            if(player.cameraTransform.GetComponent<CameraScript>())
            {
                player.cameraTransform.GetComponent<CameraScript>().rotLerp = rotLerp;
                player.cameraTransform.GetComponent<CameraScript>().posLerp = posLerp;
                player.cameraTransform.GetComponent<CameraScript>().positionMode = posMode;
                player.cameraTransform.GetComponent<CameraScript>().rotationMode = rotMode;
                if(camPos!=null)
                    player.cameraTransform.GetComponent<CameraScript>().posTarget = camPos;
                if(camLook!=null)
                    player.cameraTransform.GetComponent<CameraScript>().rotTarget = camLook;
            }
            //player.cameraTransform.position = Vector3.Lerp(player.cameraTransform.position,camPos.position,lerp);
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if(other.transform.parent.GetComponent<CharacterController>())
            {
                cameraTransform = other.transform.parent.GetComponent<CharacterController>().player.cameraTransform;
                cameraTransform.position = Vector3.Lerp(cameraTransform.position,camPos.position,1-smoothness);
            }
            //Debug.Log();
        }
    }*/
}
