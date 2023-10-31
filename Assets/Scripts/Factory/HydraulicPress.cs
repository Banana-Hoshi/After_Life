using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraulicPress : MonoBehaviour
{
  [SerializeField] GameObject buttonObject;
  Animator animator;
  HydraulicPressButton button;
  bool raised;
    public bool trailerBool;
  private void Start() {
    animator = GetComponent<Animator>();
    button = buttonObject.GetComponent<HydraulicPressButton>();
  }
  private void Update() {
    if ((button.GetSwitchDown() && !raised) || (trailerBool)) {
      animator.SetBool("raised", true);
      raised = true;
    } else if (!button.GetSwitchDown() && raised) {
      animator.SetBool("raised", false);
      raised = false;
    }
  }
}
