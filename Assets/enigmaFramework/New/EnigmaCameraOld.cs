using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaCameraOld : MonoBehaviour
{
    public Vector3 referenceVector;
    [Space(5)]
    public Transform positionTarget;
    public Transform rotationTarget;
    Camera cam;
    [Space(5)]
    public int positionMode;
    public int rotationMode;
    [Space(10)]
    public float rotationLerp;
    public float positionLerp;
    public float orbitDistance;
    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(positionTarget != null)
        {
            switch(positionMode)
            {
                case 1:
                    transform.position = positionTarget.position;
                    break;
                case 2:
                    transform.position = Vector3.Lerp(transform.position,positionTarget.position-transform.forward*orbitDistance,positionLerp);
                    break;
            }
        }
        
        if(rotationTarget != null)
        {
            switch(rotationMode)
            {
                case 1:
                    transform.rotation = rotationTarget.rotation;
                    break;
                case 2:
                    transform.LookAt(rotationTarget,referenceVector);
                    break;
            }
        }
    }
}
