using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowOfDoors : MonoBehaviour
{
  public List<GameObject> doors;
  Transform trans;
  private void Start() {
    trans = this.gameObject.transform;
    for (int i = 0; i < trans.childCount; i++) {
      doors.Add(trans.GetChild(i).gameObject);
    }
  }
}
