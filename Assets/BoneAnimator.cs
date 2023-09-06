using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class animBone 
{
    public Transform bone;
    public Transform rotationReference;
    public Vector3 multiplier;
}


public class BoneAnimator : MonoBehaviour
{
    public EnigmaPhysics enigmaPhysics;
    [Range(0,1)]
    public float lerp;
    float turnRate;
    public List<animBone> animBones = new List<animBone>();
    Quaternion rot;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        turnRate = Mathf.Lerp(turnRate,enigmaPhysics.activeTurnRate,lerp);
        
        for(int i = 0;i<animBones.Count;i++)
        {
            if(animBones[i].rotationReference == null)
            {
                rot = Quaternion.Euler(turnRate * animBones[i].multiplier.x,turnRate * animBones[i].multiplier.y,turnRate * animBones[i].multiplier.z);
                Quaternion modifiedRot = rot * animBones[i].bone.localRotation;
                animBones[i].bone.localRotation = modifiedRot;
            }
            else
            {
                Transform rotationReference = animBones[i].rotationReference;
                Vector3 multiplier = animBones[i].multiplier;
                Quaternion modifiedRot = Quaternion.AngleAxis(turnRate*multiplier.x,rotationReference.right) * Quaternion.AngleAxis(turnRate*multiplier.y,rotationReference.up) * Quaternion.AngleAxis(turnRate*multiplier.z,rotationReference.forward) *  animBones[i].bone.localRotation;
                animBones[i].bone.localRotation = modifiedRot;
            }
            
        }
    }
}
