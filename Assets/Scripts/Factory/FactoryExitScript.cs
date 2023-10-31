using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FactoryExitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") StartCoroutine(End());
    }

    IEnumerator End()
    {
        BedroomUtility.SetBadEnding(false);
        BedroomUtility.SetStage(3);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Bedroom");
    }
}
