using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public GameObject explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject explosion;
        explosion = Instantiate(explosionParticle,transform.position,Quaternion.identity);
        gameObject.SetActive(false);//Destroy(gameObject,.25f);
        Destroy(gameObject,1f);
        Destroy(explosion,3);
    }
}
