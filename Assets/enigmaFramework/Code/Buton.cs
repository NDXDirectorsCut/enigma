using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton : MonoBehaviour
{
    public Button butt;
    public List<GameObject> activateOnClick = new List<GameObject>();
    public List<GameObject> deactivateOnClick = new List<GameObject>();
    public List<GameObject> toggleOnClick = new List<GameObject>();
    int i;

    // Start is called before the first frame update
    void Start()
    {
        butt = gameObject.GetComponent<Button>();
        butt.onClick.AddListener(PeClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PeClick() //public List<Tip_Data> numeVariabila = new List<Tip_Data>();
    {
        for (i = 0; i < activateOnClick.Count; i++)
        {
            activateOnClick[i].SetActive(true);
        }
        for (i = 0; i < deactivateOnClick.Count; i++)
        {
            deactivateOnClick[i].SetActive(false);
        }
        for (i = 0; i < toggleOnClick.Count; i++)
        {
            if (toggleOnClick[i].activeSelf == false)
                toggleOnClick[i].SetActive(true);
            else
                toggleOnClick[i].SetActive(false);
        }

    }
}
