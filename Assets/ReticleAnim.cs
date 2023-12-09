using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleAnim : MonoBehaviour
{
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            transform.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(transform.GetComponent<RectTransform>().sizeDelta,Vector2.one * 100,speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("YAh");

            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(200,200);
        }
    }
}
