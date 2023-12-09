using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Jump : MonoBehaviour
{
    public CharacterController characterController;
    public Animator anim;
    Player player;
    [Space(10)]
    public float jumpForce;
    void Start()
    {
        player = characterController.player;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.grounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                player.physBody.velocity = player.physBody.velocity/2 + player.normal*jumpForce;
                anim.SetTrigger("Jump");
            }
        }
    }
}
