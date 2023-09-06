using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public EnigmaPhysics enigmaPhysics;
    public bool canJump;
    public bool jumped;
    public float minJumpTime;
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
            enigmaPhysics.rayDistance = 0.25f;
            enigmaPhysics.grounded = false;
            //enigmaPhysics.groundStick = false;

            enigmaPhysics.physBody.velocity += jumpDir * startForce;
            
        }
        float currentJumpTime = Time.time - startTime;
        
        if(jumped == true && Input.GetButton("Jump") && currentJumpTime < jumpTime && currentJumpTime > minJumpTime)
        {
            //Debug.Log(currentJumpTime);
            //enigmaPhysics.rayDistance = 0.25f;
            jumped = true;
            enigmaPhysics.grounded = false;
            enigmaPhysics.physBody.velocity += jumpDir* jumpForce*Time.deltaTime;
            
        }

        if(currentJumpTime > minJumpTime)
        {
            enigmaPhysics.rayDistance = rayDistance;
        }

        if(currentJumpTime > jumpTime || !Input.GetButton("Jump") && currentJumpTime > minJumpTime)
        {
            jumped = false;

            //enigmaPhysics.groundStick = true;
            //enigmaPhysics.rayDistance = rayDistance;
        }
        
        if(enigmaPhysics.grounded == true)
        {
            canJump = true;
            jumped = false;
            enigmaPhysics.rayDistance = rayDistance;
            //enigmaPhysics.groundStick = true;
        }
        else
        {
            canJump = false;
        }
    }
}
