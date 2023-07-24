using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJiggleScript : MonoBehaviour
{
    Renderer materialRend;

    [Range(0,1)]
    public float jiggleLerp;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        materialRend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 prevVector = materialRend.material.GetVector("_Position");
        materialRend.material.SetVector("_Position",Vector3.Slerp(prevVector,transform.position*scale,jiggleLerp*Time.deltaTime*60));
    }
}
