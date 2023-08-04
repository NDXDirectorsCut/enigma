using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody physBody;
    Vector3 posCopy;
    Vector3 rotCopy;
    [Range(0,1)]
    public float veloLerp;
    public Vector3 posVelo;
    public Vector3 rotVelo;

    // Start is called before the first frame update
    void Start()
    {
        posCopy = transform.position;
        rotCopy = transform.eulerAngles;
        physBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        posVelo = Vector3.Slerp(posVelo,posCopy - transform.position,veloLerp);
        rotVelo = rotCopy - transform.eulerAngles;
        posCopy = transform.position;
        rotCopy = transform.eulerAngles;
    }
}
