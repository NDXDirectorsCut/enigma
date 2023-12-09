using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;


public class AutostartNet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1",(ushort)0);
        NetworkManager.Singleton.StartHost();  
        //Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
