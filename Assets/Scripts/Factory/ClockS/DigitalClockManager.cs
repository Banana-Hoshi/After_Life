using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using FMOD.Studio;
using Cinemachine;
using System.Diagnostics.Contracts;

public class DigitalClockManager : MonoBehaviour
{
    int hour;
    int minutes;
    public float countdown;
    string sol;
    bool stopped;
    bool gateOpened;
    [SerializeField] EventReference gateSound;
    [SerializeField] ClockSound clockSFX;
    [SerializeField] GameObject clock;
    [SerializeField] Transform hourHand, minuteHand;
    [SerializeField] GameObject power;

    [SerializeField] TMP_Text[] digits;
    [SerializeField] EventReference _solveSound;
    EventInstance solveSound;
    bool soundPlayed;
    [SerializeField] Animator gate;
    [SerializeField] GameObject gateCam, playerCam;
    [SerializeField] CinemachineBrain mainCam;
    private void Awake()
    {
        solveSound = RuntimeManager.CreateInstance(_solveSound);
        solveSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    private void Start()
    {
        hour = Random.Range(1, 13);
        minutes = 5 * Random.Range(0, 12);
        countdown = (hour * 3600f) + (minutes * 60f);

        if (hour < 10)
        {
            sol += "0" + hour.ToString();
        }
        else
        {
            sol += hour.ToString();
        }
        if (minutes < 10 && minutes > 0)
        {
            sol += "0" + minutes.ToString();
        }
        else if (minutes == 0)
        {
            sol += "00";
        }
        else
        {
            sol += minutes.ToString();
        }
        Debug.Log("time solution is:" + sol);
    }

    bool animating = false;
    private void Update()
    {
        string playerSolution = digits[0].text + digits[1].text + digits[2].text + digits[3].text;
        if (playerSolution == sol)
        {
            //PLAYER SOLVED DIGITAL CLOCK
            if (!soundPlayed)
            {
                if (solveSound.isValid())
                {
                    StartCoroutine(PlaySound());
                    soundPlayed = true;
                }
            }
            //INSERT GATE OPEN CODE HERE
            if (!gateOpened)
            {
                StartCoroutine(GateCam());
            }
        }

        //Stop clock after seconds
        if (!animating && stopped == false && power.GetComponent<PowerBoxTracker>().active == true)
        {
            animating = true;
            StartCoroutine(ClockHands());
        }

        if (stopped == false && countdown <= 0.0f)
        {
            animating = false;
            stopped = true;
            clock.GetComponent<Animator>().speed = 0;
            clockSFX.close = true;
        }
    }
    public int GetHours()
    {
        return hour;
    }
    public int GetMinutes()
    {
        return minutes;
    }

    IEnumerator PlaySound()
    {
        for (int i = 0; i < digits.Length; i++)
        {
            RuntimeManager.AttachInstanceToGameObject(solveSound, digits[i].transform);
            solveSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(digits[i].gameObject));
            solveSound.setParameterByName("ClockValue", float.Parse(digits[i].text));
            solveSound.start();
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator GateCam()
    {
        gateOpened = true;
        mainCam.m_DefaultBlend.m_Time = 1.5f;
        yield return new WaitForSeconds(2.5f);
        playerCam.SetActive(false);
        gateCam.GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        yield return new WaitForSeconds(1.25f);
        gateCam.GetComponentInChildren<AudioListener>(true).gameObject.SetActive(true);
        gate.SetBool("open", true);
        FindObjectOfType<TaskList>().SetTask("Exit the Factory.");
        RuntimeManager.PlayOneShotAttached(gateSound, gate.gameObject);
        yield return new WaitForSeconds(2f);
        playerCam.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        gateCam.SetActive(false);
    }

    IEnumerator ClockHands()
    {
        float duration = 0f;
        float speed = countdown / 20f;

        while (countdown >= duration)
        {
            hourHand.rotation = Quaternion.Euler(0, 0, duration / 120f);
            minuteHand.rotation = Quaternion.Euler(0, 0, duration / 10f);


            duration += Time.deltaTime * speed;
            yield return null;
        }

        hourHand.rotation = Quaternion.Euler(0, 0, countdown / 120f);
        minuteHand.rotation = Quaternion.Euler(0, 0, countdown / 10f);
        countdown = -1f;
    }
}
