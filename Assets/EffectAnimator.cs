using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimator : MonoBehaviour
{
    //public EnigmaAnimator enigmaAnimator;
    public Color auraColor;
    public EnigmaPhysics enigmaPhysics;
    public Animator anim;
    public ParticleSystem jumpEffect;
    // Start is called before the first frame update
    void Start()
    {
        //anim = enigmaAnimator.anim;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("Jumped") == true)
            jumpEffect.Play();//.SetActive(true);
        if(anim.GetBool("Jumped") == false)
            jumpEffect.Stop();//.SetActive(false);
        if(enigmaPhysics.characterState != 2)
        {
            jumpEffect.Stop();//.SetActive(false);
        }
    }
}