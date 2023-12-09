using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkDisable : NetworkBehaviour
{
    public List<GameObject> disableOnline = new List<GameObject>();
    public List<GameObject> enableOnline = new List<GameObject>();
    public EnigmaAnimator anim;
    // Start is called before the first frame update
    void Start()
    {
        if(!IsOwner)
        {
            int i;
            for(i=0;i<disableOnline.Count;i++)
            {
                disableOnline[i].SetActive(false);
            }
            for(i=0;i<enableOnline.Count;i++)
            {
                enableOnline[i].SetActive(true);
            }
            anim.enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
 