using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Homing : MonoBehaviour
{
    public EnigmaPhysics enigmaPhysics;
    public Transform target;
    public Transform homingReticle;
    Rigidbody physBody;
    [Header("Homing Attack")]
    public float speed;
    public float offset;
    [System.NonSerialized]
    public bool homingTrigger;
    public float range;
    float angleDeviance;
    float gravity_Copy;
    public float turnSpeed;
    public float upForce;
    float dist;
    int characterState_copy;
    float stateTime = 0.1f;
    float stateSwitchTime;
    [Header("Air Dash")]
    public float dashSpeed;
    [System.NonSerialized]
    public bool dashTrigger;
    //public Button activator;
    Vector3 dir;
    Vector3 raDir;
    Vector3 veloRef = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        //activator.onClick.AddListener(HomeOnTarget);
        physBody = enigmaPhysics.transform.GetComponent<Rigidbody>();
        gravity_Copy = enigmaPhysics.gravityForce;
        homingReticle.gameObject.SetActive(false);
    }

    void HomeOnTarget()
    {
        dir = (target.position - transform.position).normalized;
        Vector3 rightDir = Vector3.Cross(dir,Vector3.up);
        Vector3 crossVector = Vector3.Cross(dir,rightDir);
        raDir = Quaternion.AngleAxis(Random.Range(-angleDeviance,angleDeviance),crossVector) * dir;
        physBody.velocity = raDir * speed;
        homingTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(physBody.velocity);
        if(Mathf.Abs(localVelocity.x) > 0.05f || Mathf.Abs(localVelocity.z) > 0.05f)
            veloRef = Vector3.ProjectOnPlane(physBody.velocity,enigmaPhysics.referenceVector).normalized;
        
        Debug.DrawRay(transform.position,veloRef,Color.magenta);

        
        if(target == null)
        {
            homingReticle.gameObject.SetActive(false);
        }

        if(enigmaPhysics.characterState == 2 && characterState_copy != 2)
        {
            stateSwitchTime = Time.time;
        }

        if(enigmaPhysics.characterState != 2)
        {
            homingTrigger = false;
            target = null;
            dashTrigger = false;
        }

        characterState_copy = enigmaPhysics.characterState;
        float airTime = Time.time - stateSwitchTime;
        //Debug.Log(airTime);

        if(enigmaPhysics.characterState == 2 && airTime > stateTime && homingTrigger == false && dashTrigger == false)
        {
            Collider[] homingCol = Physics.OverlapSphere(transform.position+(enigmaPhysics.referenceVector*offset)+veloRef*.5f,range);
            dist = range*range+1;
            
            for(int i = 0;i<homingCol.Length;i++)
            {
                if(homingCol[i].tag == "Homing")
                {
                    if( ((transform.position+(enigmaPhysics.referenceVector*offset)+veloRef*1.5f) - homingCol[i].transform.position).sqrMagnitude < dist )
                    {
                        dist = ((transform.position+(enigmaPhysics.referenceVector*offset)+veloRef*1.5f) - homingCol[i].transform.position).sqrMagnitude;
                        target = homingCol[i].transform;
                    }
                }
            }
            if(target != null)
            {
                homingReticle.gameObject.SetActive(true);
                Vector3 forwardDir = -(target.position - (transform.position+(enigmaPhysics.referenceVector*offset)) ).normalized;
                homingReticle.position = Camera.main.WorldToScreenPoint(target.position);//target.position + forwardDir*1.25f ;
                //homingReticle.rotation = Quaternion.LookRotation(-forwardDir,enigmaPhysics.referenceVector);
                if(Input.GetButtonDown("Jump"))
                {
                    dir = (target.position - (transform.position+(enigmaPhysics.referenceVector*offset)) ).normalized;//Vector3.ProjectOnPlane(physBody.velocity.normalized,enigmaPhysics.referenceVector).normalized;
                    enigmaPhysics.physBody.velocity = Vector3.zero;
                    homingTrigger = true;
                    physBody.velocity = veloRef * speed;
                }
            }
            else
            {
                if(Input.GetButtonDown("Jump") && dashTrigger == false)
                {
                    Debug.Log("Air Dash");
                    physBody.velocity = veloRef.normalized * dashSpeed;
                    dashTrigger = true;
                }
            }
            
            
        }

        /*
        if(enigmaPhysics.characterState == 2 && Input.GetButtonDown("Jump") && target == null && airTime > stateTime )
        {
            Collider[] homingCol = Physics.OverlapSphere(transform.position,range);
            dist = range*range+1;
            gravity_Copy = enigmaPhysics.gravityForce;
            //enigmaPhysics.gravityForce = 0;
            for(int i = 0;i<homingCol.Length;i++)
            {
                if(homingCol[i].tag == "Homing")
                {
                    Debug.Log(homingCol[i].name);
                    enigmaPhysics.gravityForce = 0;

                    if( (transform.position - homingCol[i].transform.position).sqrMagnitude < dist )
                    {
                        dist = (transform.position - homingCol[i].transform.position).sqrMagnitude;
                        target = homingCol[i].transform;
                        dir = (target.position - transform.position).normalized;//Vector3.ProjectOnPlane(physBody.velocity.normalized,enigmaPhysics.referenceVector).normalized;
                        enigmaPhysics.physBody.velocity = Vector3.zero;
                        homingTrigger = true;
                        //homingret
                        physBody.velocity = dir * speed;
                    }   
                    //enigmaPhysics.physBody.velocity = -(transform.position - homingCol[i].transform.position).normalized * homingForce;
                }
            }

        }
        */

        if(homingTrigger == true && enigmaPhysics.characterState == 2)
        {
            Vector3 objDir = (target.position - (transform.position+(enigmaPhysics.referenceVector*offset)) ).normalized;
            Vector3 crossVector = Vector3.Cross(physBody.velocity,objDir);
            float clampedDist = Mathf.Clamp(Vector3.Distance(target.position,(transform.position+(enigmaPhysics.referenceVector*offset)) ),0,1);
            physBody.velocity = Quaternion.AngleAxis(Vector3.SignedAngle(physBody.velocity,objDir,crossVector)*turnSpeed*Time.deltaTime*((2-clampedDist)*(2-clampedDist)),crossVector) * physBody.velocity * clampedDist;
        }
        if(target != null && Vector3.Distance((transform.position+(enigmaPhysics.referenceVector*offset)) ,target.position)<.5f)
        {
            homingTrigger = false;
            target = null;
            physBody.velocity = enigmaPhysics.referenceVector*upForce;
            enigmaPhysics.gravityForce = gravity_Copy;
            //StartCoroutine(Reset());
        }
        if(enigmaPhysics.characterState!=2 || target == null)
        {
            enigmaPhysics.gravityForce = gravity_Copy;
        }

        /*
        Debug.DrawRay(transform.position,raDir,Color.red);
        
        */
    }
    
    //IEnumerator Reset()
    //{
    //    yield return new WaitForSeconds(1);
    //    //physBody.isKinematic = true;
    //    physBody.velocity = Vector3.zero;
    //    physBody.transform.position = Vector3.zero;
    //}
}
