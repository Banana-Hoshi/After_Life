using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollsAnimationController : MonoBehaviour
{
    Animator anim;

    public bool sitting;
    public bool gossiping;
    public bool standing;
    public bool standingCrooked;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (sitting)
        {
            anim.SetBool("Sitting", true);
        } else if (gossiping)
        {
            anim.SetBool("Gossiping", true);
        }
        else if (standing)
        {
            anim.SetBool("Standing", true);
        }
        else if (standingCrooked)
        {
            anim.SetBool("StandingCrooked", true);
        }
    }
}
