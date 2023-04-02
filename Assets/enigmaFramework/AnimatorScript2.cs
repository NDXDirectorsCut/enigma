using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript2 : MonoBehaviour
{
    Animator anim;
    public CharacterController characterController;
    Player plyr;
    public Transform characterTransform;
    [Space(10)]
    public Vector3 offset;
    [Space(5)]
    public float lookLerp;
    public float upLerp;
    Vector3 look; Vector3 up;
    void Start()
    {
        anim = gameObject.GetComponent(typeof(Animator)) as Animator;
        plyr = characterController.player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVelo = transform.InverseTransformDirection(plyr.physBody.velocity);
        anim.SetInteger("CharacterState",plyr.characterState);
        anim.SetFloat("Velocity",plyr.physBody.velocity.magnitude);
        anim.SetFloat("UpVelo",localVelo.y);
        transform.position = characterTransform.position + transform.up*offset.y + transform.right*offset.x + transform.forward*offset.z;
        if(plyr.axisInput.magnitude>0.1f)
        {
             if(Vector3.Angle(plyr.physBody.velocity,plyr.axisInput.normalized)>125 && Vector3.Angle(Vector3.up,plyr.normal)<45)
            {
                anim.SetTrigger("Turn");
            }
        }
        if(Mathf.Abs(localVelo.z)>.2f)
            look = Vector3.Slerp(transform.forward,Quaternion.FromToRotation(Vector3.Cross(plyr.physBody.velocity.normalized,Vector3.Cross(plyr.normal,plyr.physBody.velocity.normalized)).normalized,plyr.physBody.transform.up)*plyr.physBody.velocity,lookLerp);
        up = Vector3.Slerp(transform.up,plyr.physBody.transform.up,upLerp);

        if(plyr.physBody.velocity.magnitude>.1f)
        {
            transform.rotation = Quaternion.LookRotation(look,up);
        }
        //Debug.Log(transform.forward-yRot);
    }
}