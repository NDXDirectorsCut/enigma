using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Networking.Transport;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class NetButtonScript : MonoBehaviour
{
    public Button button;
    public float mode;
    public TMP_InputField ipBox;
    public TMP_InputField portBox;

    string ip;
    int port;
    
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    void Click()
    {
        if(ipBox != null && portBox != null)
        {
            Debug.Log(ipBox.text);
            if(int.TryParse(portBox.text, out port))
            {
                //Debug.Log("Sex");
                Debug.Log(port);
            }
        }
        
        switch(mode)
        {
            case 0: // Host Multiplayer
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ipBox.text,(ushort)port);
                NetworkManager.Singleton.StartHost();  
                break;
            case 1: // Singleplayer
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1",(ushort)0);
                NetworkManager.Singleton.StartHost();  
                break;
            case 2: // Connect to Multiplayer
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ipBox.text,(ushort)port);
                NetworkManager.Singleton.StartClient(); 
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
