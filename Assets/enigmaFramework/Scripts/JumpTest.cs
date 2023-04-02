using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    float startTime;
    public Rigidbody rigidBody;
    public CharacterController charControl;
    Player player;
    public float maxJumpTime;
    public float jumpForce;
    public float startForce;
    public bool jumped;
    public bool canJump;
    Vector3 jumpDir;

    void Start()
    {
        player = charControl.player;
    }

    void Update()
    {

        if(Input.GetButtonDown("Jump"))
        {
            startTime=Time.time;
            jumpDir = player.normal;
            rigidBody.velocity += jumpDir.normalized * startForce;
            charControl.stickToGround = false;
        }
        if(Input.GetButton("Jump"))
        {
            if(Time.time-startTime < maxJumpTime)
            {
                rigidBody.velocity += jumpDir.normalized * jumpForce * Time.deltaTime;
            }
            if(Time.time-startTime > .5f)
            {
                charControl.stickToGround = true;
            }
        }
    }
}
