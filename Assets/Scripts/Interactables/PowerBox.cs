using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : Interactable
{
  bool active = false;
  Animator animator;
  [SerializeField] GameObject switchObject;
  [SerializeField] EventReference switchSound;
  private void Start() {
    animator = switchObject.GetComponent<Animator>();
  }
  public override string GetDescription() {
        if (!active)
        {
            return "Pull the Switch?";
        }
        else
        {
            return "";
        }
    }
  public override int GetSpeechBubble() {
    return 0;
  }
  public override void Interact() {
    if (!active) {
      active = true;
      animator.SetBool("enabled", true);
      GetComponent<Collider>().enabled = false;
      RuntimeManager.PlayOneShotAttached(switchSound, switchObject);
    }
    //active = !active;

    //also lighting insert code here

  }
  public bool GetSwitchDown() {
    return active;
  }
}
