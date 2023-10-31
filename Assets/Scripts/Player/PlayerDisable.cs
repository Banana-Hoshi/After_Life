using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDisable
{
  static bool disabled = false;
  public static bool IsPlayerDisabled() {
    return disabled;
  }
  public static void ToggledDisabled() {
    disabled = !disabled;
  }
}
