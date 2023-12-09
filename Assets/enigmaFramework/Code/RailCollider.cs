using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class RailCollider : MonoBehaviour
{
    public EnigmaRail enigmaRail; 
    bool backwards;
    // Start is called before the first frame update
    void Start()
    {
        enigmaRail = transform.parent.parent.parent.GetComponent<EnigmaRail>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        CurveSample testSample = enigmaRail.rail.GetSampleAtDistance(dist);

        Debug.DrawRay(enigmaRail.transform.position + testSample.location,testSample.Rotation * Vector3.up,Color.green);
        Debug.DrawRay(enigmaRail.transform.position + testSample.location,testSample.tangent,Color.blue);
        */
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.gameObject);
        if(col.GetComponent<RailInteraction>() && col.GetComponent<RailInteraction>().rail == null)
        {
            RailInteraction railScript = col.GetComponent<RailInteraction>();
            backwards = false;
            CurveSample sample = enigmaRail.rail.GetProjectionSample( enigmaRail.transform.InverseTransformPoint(col.transform.position));
            //Debug.DrawRay(sample.location)
            int i;
            //Debug.Log(sample.distanceInCurve);
            float sumLength = 0;
            for(i=0;i<enigmaRail.rail.curves.Count;i++)
            {
                if(enigmaRail.rail.curves[i] == sample.curve)
                {
                    break;
                }
                else
                {
                    sumLength += enigmaRail.rail.curves[i].Length;
                }
                //Debug.Log(sample.curve.samples[i].curve.Length);
            }
            sumLength += sample.distanceInCurve;
            Debug.Log(sumLength);
            //dist = sumLength;
            Debug.Log(enigmaRail.rail.Length);

            //col.transform.position = enigmaRail.transform.position + sample.location;
            //float distanceInSpline;
            /*
            int i;
            for(i=0;i<sample.curve.samples.Count;i++)
            {
                //Debug.Log(i);
                if(sample.curve.samples[i].curve.Length == sample.curve.Length)
                {
                    Debug.Log("found " + i);
                    break;
                }
            }*/
            

            //railInt.position = sample.distanceInCurve;
            
            if(col.GetComponent<EnigmaPhysics>())
            {
                EnigmaPhysics enigmaPhysics = col.GetComponent<EnigmaPhysics>();
                if(enigmaPhysics.characterState != 3 && railScript.canGrind == true)
                {
                    enigmaPhysics.characterState = 3;
                    //projectedObject.rotation = sample.Rotation;
                }
            }
            Vector3 objSpeed = col.GetComponent<Rigidbody>().velocity;
            if(Vector3.Angle(sample.tangent,objSpeed.normalized) > 90 )
            {
                backwards = true;
            }
            
            railScript.rail = enigmaRail.rail;
            railScript.position = sumLength;
            railScript.alongSpeed = backwards ? -objSpeed.magnitude : objSpeed.magnitude;
            railScript.backwards = backwards;
        }
    }
}
