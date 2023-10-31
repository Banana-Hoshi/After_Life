using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherRobotTrigger : MonoBehaviour
{
  bool chasing;
  private void OnTriggerStay(Collider other) {
    if (other.gameObject.tag == "Player") {
      chasing = true;
    }
  }
  public bool GetChasing() {
    return chasing;
  }
}
