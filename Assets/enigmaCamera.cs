using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enigmaCamera : MonoBehaviour
{
    public Transform target;
    [Range(0,1)]
    public float lerp;
    public float speed;
    float mX,mY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mX = Mathf.Lerp(mX,Input.GetAxisRaw("Mouse X"),lerp);
        mY = Mathf.Lerp(mY,Input.GetAxisRaw("Mouse Y"),lerp);
        transform.RotateAround(target.position,Vector3.up,mX * speed);
        transform.RotateAround(target.position,transform.right,mY * speed);
        transform.LookAt(target,Vector3.up);
    }
}
