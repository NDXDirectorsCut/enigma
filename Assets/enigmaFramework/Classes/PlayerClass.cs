using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Player
{
    [Header("Objects")]
    public Rigidbody physBody;
    public Transform cameraTransform;
    [Header("Physics")]
    public Vector3 referenceVector;
    [Header("Grounded")]
    public float acceleration;
    public float deceleration;
    public float turnValue;
    public float slopeForce;
    public bool grounded;
    [Space(2.5f)]
    public float maxAccel;
    public float maxSpeed;
    [Space(2.5f)]
    public Vector3 normal;
    public Vector3 point;
    [Space(2.5f)]
    public float groundColliderHeight;
    public float groundColliderRadius;
    [Space(2.5f)]
    [Header("Air")]
    public float airAcceleration;
    public float gravityForce;
    [Space(2.5f)]
    public float maxAirSpeed;
    [Space(2.5f)]
    public float airColliderHeight;
    public float airColliderRadius;


    [Header("Debug")]
    public float debugValue;
    public int characterState;
    public Vector3 axisInput;
    public LayerMask collisionLayers;

}
