using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class EndingBed : MonoBehaviour
{
    [SerializeField] GameObject playerCam, bedCam, lookLeftCam, lookRightCam;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineBrain MainCam;

    private void Start()
    {
        playerCam.SetActive(false);
        bedCam.SetActive(true);
        PlayerDisable.ToggledDisabled();
        StartCoroutine(WakeUp());
    }

    public IEnumerator WakeUp()
    {
        if (SceneManager.GetActiveScene().name == "BadEnding")
        {
            MainCam.m_DefaultBlend.m_Time = 0.4f;
            bedCam.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            bedCam.SetActive(false);
            lookLeftCam.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            lookLeftCam.SetActive(false);
            lookRightCam.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            lookRightCam.SetActive(false);
            lookLeftCam.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            lookLeftCam.SetActive(false);
            lookRightCam.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            lookRightCam.SetActive(false);
            MainCam.m_DefaultBlend.m_Time = 2f;
            bedCam.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("BadEndCredits");
        }
        else
        {

            MainCam.m_DefaultBlend.m_Time = 2f;
            bedCam.SetActive(true);
            yield return new WaitForSeconds(2f);
            bedCam.SetActive(false);
            player.SetActive(true);
            playerCam.SetActive(true);
            yield return new WaitForSeconds(2f);
            PlayerDisable.ToggledDisabled();
        }
    }
}