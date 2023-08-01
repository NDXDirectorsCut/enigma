using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public EnigmaPhysics enigmaPhysics;
    public bool canJump;
    public bool jumped;
    public float jumpTime;
    float startTime;
    public float startForce;
    public float jumpForce;
    Vector3 jumpDir;

    float rayDistance;



    // Start is called before the first frame update
    void Start()
    {
        enigmaPhysics = gameObject.GetComponent<EnigmaPhysics>();
        rayDistance = enigmaPhysics.rayDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(canJump == true && Input.GetButtonDown("Jump"))
        {
            canJump = false;
            startTime = Time.time;
            jumped = true;
            jumpDir = enigmaPhysics.normal;
            enigmaPhysics.characterState = 2;
            enigmaPhysics.rayDistance = 0.1f;
            enigmaPhysics.grounded = false;
            enigmaPhysics.groundStick = false;

            enigmaPhysics.physBody.velocity += jumpDir * startForce;
            
        }
        float currentJumpTime = Time.time - startTime;
        
        if(jumped == true && Input.GetButton("Jump") && currentJumpTime < jumpTime)
        {
            Debug.Log(currentJumpTime);
            enigmaPhysics.rayDistance = 0.1f;
            enigmaPhysics.grounded = false;
            enigmaPhysics.physBody.velocity += jumpDir* jumpForce*Time.deltaTime;
            
        }
        if(currentJumpTime > jumpTime || Input.GetButtonUp("Jump"))
        {
            jumped = false;
            enigmaPhysics.groundStick = true;
            enigmaPhysics.rayDistance = rayDistance;
        }

        if(enigmaPhysics.grounded == true)
        {
            canJump = true;
            enigmaPhysics.rayDistance = rayDistance;
            enigmaPhysics.groundStick = true;
        }
        else
        {
            canJump = false;
        }
    }
}
