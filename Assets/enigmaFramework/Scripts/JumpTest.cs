using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    float startTime;
    public Rigidbody rigidBody;
    public float maxJumpTime;
    public float jumpForce;

    void Update()
    {

        if(Input.GetButtonDown("Jump"))
        {
            startTime=Time.time;
        }
        if(Input.GetButton("Jump"))
        {
            if(Time.time-startTime < maxJumpTime)
            {
                rigidBody.velocity += Vector3.up * jumpForce * Time.deltaTime;
            }
        }
    }
}
