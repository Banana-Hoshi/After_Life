using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEntrance : MonoBehaviour
{
  [SerializeField] PowerBox powerSwitch;
    [SerializeField] EventReference gateSound;
  bool opened;
  Animator animator;
  private void Start() {
    powerSwitch = powerSwitch.GetComponent<PowerBox>();
    animator = GetComponent<Animator>();
  }
  private void Update() {
    if (!opened && powerSwitch.GetSwitchDown()) {
      RuntimeManager.PlayOneShotAttached(gateSound, gameObject);
      animator.SetBool("open", true);
      opened = true;
    }
  }
}
