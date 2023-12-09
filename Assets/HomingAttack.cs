using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAttack : MonoBehaviour
{
    public EnigmaPhysics enigmaPhysics;
    [Range(0,25)]
    public float range;
    public float homingForce;
    public float upForce;
    float dist;
    float origGravity;
    public bool homing;
    Transform target;
    //public LayerMask layers;
    // Start is called before the first frame update
    void Start()
    {
        enigmaPhysics = gameObject.GetComponent<EnigmaPhysics>();
        origGravity = enigmaPhysics.gravityForce;
    }

    // Update is called once per frame
    void Update()
    {   
        if(enigmaPhysics.characterState == 2 && Input.GetButtonDown("Jump") && target == null)
        {
            Collider[] homingCol = Physics.OverlapSphere(transform.position,range);
            dist = range*range+1;
                origGravity = enigmaPhysics.gravityForce;
                //enigmaPhysics.gravityForce = 0;
                for(int i = 0;i<homingCol.Length;i++)
                {
                    if(homingCol[i].tag == "Homing")
                    {
                        enigmaPhysics.gravityForce = 0;
                        //Debug.Log(homingCol[i].name);
                        //Debug.Log( (transform.position - homingCol[i].transform.position).sqrmagnitude );
                        if( (transform.position - homingCol[i].transform.position).sqrMagnitude < dist )
                        {
                            dist = (transform.position - homingCol[i].transform.position).sqrMagnitude;
                            target = homingCol[i].transform;
                            enigmaPhysics.physBody.velocity = Vector3.zero;
                        }
                        
                        //enigmaPhysics.physBody.velocity = -(transform.position - homingCol[i].transform.position).normalized * homingForce;
                    }
                }
        }

        if(target != null && enigmaPhysics.characterState == 2)
        {
            transform.position = Vector3.Slerp(transform.position,target.position,homingForce*Time.deltaTime);
            homing = true;
            if( (transform.position - target.position).sqrMagnitude < 0.4f*0.4f)
            {
                target = null;
                homing = false;
                enigmaPhysics.gravityForce = origGravity;
                enigmaPhysics.physBody.velocity = Vector3.up * upForce;
            }
        }
    }
}
