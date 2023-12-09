using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineMesh {
    public class EnigmaRailOld : MonoBehaviour
    {
        public Spline rail;
        public Transform railObject;
        public float railHeight = .2f;
        [Range(-100,100)]
        public float speed;
        public float gravityInfluence;
        [Range(0,1)]
        public float friction;
        [Range(0,1)]
        public float pos;
        public float length;
        public float angle;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
            pos+= (Time.fixedDeltaTime * speed)/rail.Length;
            pos = Mathf.Clamp(pos,0,1);
            CurveSample sample = rail.GetSampleAtDistance(pos * rail.Length);
            Vector3 railUp = sample.Rotation * Vector3.up;
           
            railObject.position = transform.position + sample.location + railUp*railHeight;
            railObject.rotation = Quaternion.LookRotation(sample.tangent,railUp);//sample.Rotation;

            Vector3 railRight = Vector3.Cross(sample.tangent,Vector3.up);
            Vector3 globalForward = -Vector3.Cross(railRight,Vector3.up).normalized;

            Debug.DrawRay(transform.position + sample.location,sample.tangent,Color.magenta);
            Debug.DrawRay(transform.position + sample.location,globalForward,Color.red);
            angle = Vector3.SignedAngle(sample.tangent,globalForward,railRight);

            speed += angle*Time.fixedDeltaTime*gravityInfluence;
            speed = Mathf.Lerp(speed,0,friction);   

            if(rail.IsLoop == true && pos == 1)
            {
                pos = 0;
            }
            else if (rail.IsLoop == true && pos == 0)
            {
                pos = 1;
            }

            if(railObject.gameObject.GetComponent<EnigmaPhysics>())
            {
                EnigmaPhysics enigmaPhysics = railObject.gameObject.GetComponent<EnigmaPhysics>();
                enigmaPhysics.normal = railUp;
            }
            //Debug.Log(rail.Length);
        }
    }
}
