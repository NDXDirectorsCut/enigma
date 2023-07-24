using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnigmaPlayer
{
    [Header("Character Settings")]
    public float acceleration;
    public float deceleration;
    [Space(5)]
    public float maxSpeed;

    [Header("Physics Settings")]
    public Vector3 referenceVector;
    public float rotationLerp;

    [Header("References")]
    public GameObject characterObject;
    public GameObject cameraObject;
    
    [Header("Debug")]
    public bool drawDebug;
    [Space(5)]
    public int characterState;
    public bool isGrounded;
    public Vector3 normal;
    public Vector3 hitPos;
}
