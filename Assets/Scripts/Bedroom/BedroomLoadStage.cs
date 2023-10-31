using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomLoadStage : MonoBehaviour
{
  int stage;
  [SerializeField] List<GameObject> baseStage;
  [SerializeField] List<GameObject> postJimStage;
  [SerializeField] List<GameObject> postTimStage;
  [SerializeField] List<GameObject> postKimStage;
  private void Start() {
    stage = BedroomUtility.GetStage();
    switch (stage) {
      case 1:
        for (int i = 0; i <baseStage.Count; i++) {
          baseStage[i].SetActive(true);
        }
        break;
      case 2:
        for (int i = 0; i < postJimStage.Count; i++) {
          postJimStage[i].SetActive(true);
        }
        break;
      case 3:
        for (int i = 0; i < postTimStage.Count; i++) {
          postTimStage[i].SetActive(true);
        }
        break;
      case 4:
        for (int i = 0; i < postKimStage.Count; i++) {
          postKimStage[i].SetActive(true);
        }
        break;
    }
    if (BedroomUtility.GetBadEnding()) {
      StartCoroutine(FindObjectOfType<Bed>().WakeUp(true));
    } else {
      StartCoroutine(FindObjectOfType<Bed>().WakeUp(false));
      
    }
  }
}
