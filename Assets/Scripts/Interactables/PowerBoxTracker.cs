using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoxTracker : MonoBehaviour
{
    [SerializeField] List<PowerBox> powerBoxes;
    [SerializeField] FallingRobotSpawns robotSpawner;
    [SerializeField] PowerBox masterControlBox;
    [SerializeField] GameObject clockCam, playerCam;
    CinemachineBrain mainCam;
    [SerializeField] GameObject clock;
    [SerializeField] GameObject lightObj;
    [SerializeField] public GameObject soundObj;
    public bool active;
    bool activated, a;
    int totalPower;

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<CinemachineBrain>();
    }
    private void Update()
    {
        if (!active)
        {
            totalPower = 0;
            if (totalPower <= 0)
            {
                //robotSpawner.canSpawn = false;
            }
            for (int i = 0; i < powerBoxes.Count; i++)
            {
                if (powerBoxes[i].GetSwitchDown())
                {
                    totalPower++;
                }
            }
            if (totalPower >= 3 && activated == false)
            {
                activated = true;
                StartCoroutine(ClockCutscene());
            }
            if (totalPower >= 1 && a == false)
            {
                lightObj.SetActive(true);
                robotSpawner.canSpawn = true;
                FindObjectOfType<TaskList>().SetTask("Find levers to turn the clock off.");
                a = true;
            }
        }
    }

    IEnumerator ClockCutscene()
    {
        PlayerDisable.ToggledDisabled();
        mainCam.m_DefaultBlend.m_Time = 2f;
        playerCam.SetActive(false);
        clockCam.SetActive(true);
        yield return new WaitForSeconds(mainCam.m_DefaultBlend.m_Time);
        FindObjectOfType<TaskList>().SetTask("Turn on the Factory power in the Control Room.");
        clock.GetComponent<Animator>().SetBool("active", true);
        soundObj.SetActive(true);
        active = true;
        yield return new WaitForSeconds(2);
        PlayerDisable.ToggledDisabled();
        clockCam.SetActive(false);
        playerCam.SetActive(true);
        yield return new WaitForSeconds(mainCam.m_DefaultBlend.m_Time);
        mainCam.m_DefaultBlend.m_Time = 0.5f;
    }
}
