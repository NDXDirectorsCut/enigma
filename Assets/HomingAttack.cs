using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAttack : MonoBehaviour
{
    public CharacterController charControl;
    Player player;
    public float range;
    Transform homingTrigger;
    // Start is called before the first frame update
    void Start()
    {
        player = charControl.player;
        homingTrigger = player.physBody.transform.Find("HomingTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        homingTrigger.localScale = new Vector3(range,range,range);
        if(player.physBody.velocity.magnitude >0.1f)
            homingTrigger.forward = Quaternion.FromToRotation(Vector3.Cross(player.physBody.velocity,homingTrigger.right),Vector3.up) * player.physBody.velocity;
    }
}
