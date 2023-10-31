using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    Animator animator;
    RuntimeAnimatorController controller;
    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = animator.runtimeAnimatorController;
    }
    void Update()
    {
        if (controller.animationClips.GetValue(8).ToString() == "1")
        {
            print("one");
        }
        print(controller.animationClips.GetValue(8).ToString());
    }
}
