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
        if(canJump==true)
        {
            if(Input.GetButtonDown("Jump"))
            {
                canJump = false;
                jumped = true;
                startTime=Time.time;
                jumpDir = player.normal;
                rigidBody.velocity += jumpDir.normalized * startForce;
                rigidBody.transform.position += player.normal *.2f;
                charControl.stickToGround = false;
            }
        }
        if(jumped == true)
        {
            if(Input.GetButton("Jump"))
            {
                if(Time.time-startTime < maxJumpTime)
                {
                    rigidBody.velocity += jumpDir.normalized * jumpForce * Time.deltaTime;
                }
            }
        }
        if(Time.time-startTime > .1f)
        {
            charControl.stickToGround = true;
        }
        if(Input.GetButtonUp("Jump"))
        {
            jumped = false;
            canJump = false;
        }
        if(player.characterState == 1 || player.characterState == 2)
        {
            canJump = true;
        }
        if(player.characterState == 3 )
        {
            canJump = false;
        }
    }
}
