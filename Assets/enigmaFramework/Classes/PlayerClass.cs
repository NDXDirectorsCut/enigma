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
    [Header("Grounded")]
    public float acceleration;
    public float deceleration;
    public float turnValue;
    public bool grounded;
    [Space(2.5f)]
    [Header("Air")]
    public float airAcceleration;
    [Space(5)]
    public float maxAccel;
    public float maxSpeed;
    [Space(2.5f)]
    public float maxAirSpeed;
    [Header("Debug")]
    public float debugValue;
    public int characterState;
    public Vector3 axisInput;
    public Vector3 normal;
    public LayerMask collisionLayers;

}
