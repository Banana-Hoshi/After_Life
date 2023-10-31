using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
  Light l;
  float timer;
  float nextFlashTime;
  public float flashTimeLower;
  public float flashTimeUpper;
  public float doubleFlashChance;
  public bool defaultEnabled;
  private void Start() {
    l = GetComponent<Light>();
    l.enabled = defaultEnabled;
  }
  private void Update() {
    timer += Time.deltaTime;
    if (timer >= nextFlashTime) {
      StartCoroutine(FlashLight((Random.Range(flashTimeLower, flashTimeUpper))));
      timer = 0;
      nextFlashTime = Random.Range(2, 5);
    }
  }
  private IEnumerator FlashLight (float flashTime) {
    l.enabled = !l.enabled;
    yield return new WaitForSeconds(flashTime);
    l.enabled = !l.enabled;
    if (Random.Range(0, 100) < doubleFlashChance) {
      yield return new WaitForSeconds(0.1f);
      l.enabled = !l.enabled;
      yield return new WaitForSeconds(0.1f);
      l.enabled = !l.enabled;
    }
  }
}
