using Cinemachine;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bed : Interactable
{
    [SerializeField] GameObject playerCam, bedCam, alarmCam, lookLeftCam, lookRightCam;
    [SerializeField] GameObject player, playerLight;
    [SerializeField] CinemachineBrain MainCam;
    [SerializeField] GameObject interaction;
    [SerializeField] RectTransform topEye, bottomEye;
    [SerializeField] string nextScene = "";
    [SerializeField] bool isScared;
    [SerializeField] public string sleepDialogue = "Press [E] to sleep";
    [HideInInspector] public bool canSleep, isSleeping;
    string sleepText;

    Material[] mats;

    private void Start()
    {
        bedCam.SetActive(true);
        alarmCam.SetActive(false);
        playerCam.SetActive(false);
        lookLeftCam.SetActive(false);
        lookRightCam.SetActive(false);
        interaction.SetActive(false);
        PlayerDisable.ToggledDisabled();

        mats = gameObject.GetComponent<Renderer>().sharedMaterials;
    }

    public override string GetDescription()
    {
        if (canSleep)
        {
            if (isSleeping)
            {
                return "";
            }
            return "Press " + sleepText + " to sleep";
        }
        return sleepDialogue;
    }
    public override int GetSpeechBubble()
    {
        return 0;
    }

    public override void Interact()
    {
        if (canSleep)
        {
            mats[1].color = Color.black;
            PlayerDisable.ToggledDisabled();
            player.SetActive(false);
            StartCoroutine(Sleep(3));
        }
        else
        {
            mats[1].color = Color.white;
            sleepDialogue = "I'm not tired";
        }
    }

    IEnumerator Sleep(int s)
    {
        topEye.sizeDelta = new Vector2(topEye.rect.width, 0f);
        bottomEye.sizeDelta = new Vector2(topEye.rect.width, 0f);
        isSleeping = true;
        bedCam.SetActive(true);
        yield return new WaitForSeconds(s);
        alarmCam.SetActive(true);
        bedCam.SetActive(false);
        yield return new WaitForSeconds(s);
        while (topEye.rect.height < 549.5f)
        {
            topEye.sizeDelta = new Vector2(topEye.rect.width, Mathf.Lerp(topEye.rect.height, 550f, Time.deltaTime * 10f));
            bottomEye.sizeDelta = new Vector2(topEye.rect.width, Mathf.Lerp(bottomEye.rect.height, 550f, Time.deltaTime * 10f));
            yield return null;
        }
        SceneManager.LoadScene(nextScene);
        //NextLevel();
    }

    public IEnumerator WakeUp(bool scared)
    {
        topEye.sizeDelta = new Vector2(topEye.rect.width, 550f);
        bottomEye.sizeDelta = new Vector2(topEye.rect.width, 550f);
        while (topEye.rect.height > 0.5f)
        {
            topEye.sizeDelta = new Vector2(topEye.rect.width, Mathf.Lerp(topEye.rect.height, 0, Time.deltaTime * 10f));
            bottomEye.sizeDelta = new Vector2(topEye.rect.width, Mathf.Lerp(topEye.rect.height, 0, Time.deltaTime * 10f));
            yield return null;
        }
        if (scared)
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
            bedCam.SetActive(false);
            player.SetActive(true);
            playerCam.SetActive(true);
            yield return new WaitForSeconds(2f);
            PlayerDisable.ToggledDisabled();
            playerLight.SetActive(true);
            interaction.SetActive(true);
        }
        if (!scared)
        {
            MainCam.m_DefaultBlend.m_Time = 2f;
            bedCam.SetActive(true);
            yield return new WaitForSeconds(2f);
            bedCam.SetActive(false);
            player.SetActive(true);
            playerCam.SetActive(true);
            yield return new WaitForSeconds(2f);
            PlayerDisable.ToggledDisabled();
            playerLight.SetActive(true);
            interaction.SetActive(true);
        }
        if (BedroomUtility.GetAttempts() == 0)
        {
            canSleep = false;
        }
        else canSleep = true;

    }

    private void OnEnable()
    {
        InputUser.onChange += OnInputDeviceChange;
    }

    private void OnDisable()
    {
        InputUser.onChange -= OnInputDeviceChange;
    }

    void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            if (user.controlScheme.Value.name.Equals("Gamepad"))
            {
                sleepText = "[A]";
                sleepDialogue = sleepDialogue.Replace("[E]", "[A]");
            }
            else
            {
                sleepText = "[E]";
                sleepDialogue = sleepDialogue.Replace("[A]", "[E]");
            }

        }
    }
}