using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherRobotPlug : Interactable {
  [SerializeField] TetherRobots robot;
    [SerializeField] Rigidbody wireEnd;
  string unplug = "Pull the Plug?";
  [SerializeField] EventReference switchSound;
  public bool active;
  [SerializeField] HydraulicPressButton button;
  private void Update() {
    active = !button.GetSwitchDown();
  }
  public override string GetDescription() {
    return unplug;
  }
  public override int GetSpeechBubble() {
    return 0;
  }
  public override void Interact() {
    if (active) {
      robot.Unplug();
      wireEnd.isKinematic = false;
      wireEnd.useGravity = true;
      wireEnd.AddForce((Vector3.up + Vector3.forward) * 1f, ForceMode.Impulse);
      unplug = "";
    }
    
  }

}
