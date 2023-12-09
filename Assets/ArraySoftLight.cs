using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySoftLight : MonoBehaviour
{
    public float lightNumber;
    float lightNumber_copy;
    [Range(0,25)]
    public float intensity;
    [Range(0,100)]
    public float radius;
    [Range(0.05f,10)]
    public float spread;

    int i;

    List<GameObject> Lights = new List<GameObject>();

    // Start is called before the first frame update
    void OnEnable()
    {
        for(i=0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for(i=0;i<lightNumber;i++)
        {
            GameObject light = new GameObject("ArrayLight " + i,typeof(Light));
            light.transform.SetParent(transform);
            light.transform.position = transform.position + Random.insideUnitSphere * spread;
            transform.GetChild(i).GetComponent<Light>().shadows = LightShadows.Soft;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i;
        for(i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).GetComponent<Light>().intensity = intensity/transform.childCount;
            transform.GetChild(i).GetComponent<Light>().range = radius;
            transform.GetChild(i).transform.position = transform.position + -(transform.position - transform.GetChild(i).transform.position).normalized * spread;
            //Lights[i].arrayLight.intensity = intensity/lightNumber_copy;
        }    
    }
}
