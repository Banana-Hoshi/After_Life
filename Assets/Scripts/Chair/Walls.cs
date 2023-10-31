using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
  public List<RowOfDoors> row;
  Transform trans;
  private void Start() {
    trans = this.gameObject.transform;
    for (int i = 0; i < trans.childCount; i++) {
      if (trans.GetChild(i).GetComponent<RowOfDoors>() != null) {
        row.Add(trans.GetChild(i).gameObject.GetComponent<RowOfDoors>());
      }
    }
  }
}
