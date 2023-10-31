using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ChairTutorial : MonoBehaviour
{
    public GameObject enemySpawn;
    public GameObject ui1, ui2, ui3, parentUI;
    [SerializeField] TextMeshProUGUI text1;
    bool t = false;
    bool t1 = false;
    bool interacted, notInteracted;
    [SerializeField] SpiderController controller;
    [SerializeField] CinemachineVirtualCamera cam;


    private void Start()
    {
        cam.enabled = false;
        controller.enabled = false;
        ui1.SetActive(true);
    }

    private void Update()
    {
        if (interacted && t && !t1)
        {
            ui1.SetActive(false);
            ui2.SetActive(true);
            t = false;
        }
        if (notInteracted && !t)
        {
            t1 = true;
            cam.enabled = true;
            controller.enabled = true;
            ui2.SetActive(false);
            ui3.SetActive(true);
            StartCoroutine(WaitForS());
        }
    }

    IEnumerator WaitForS()
    {
        yield return new WaitForSeconds(1f);
        controller.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        ui3.SetActive(false);
        parentUI.SetActive(false);
        enemySpawn.GetComponent<SpawnEnemies>().active = true;
        FindObjectOfType<TaskList>().SetTask("Flash the monsters away.");
        this.enabled = false;
    }

    public void GetMouse(InputAction.CallbackContext context)
    {
        interacted = context.performed || interacted;
        t = context.performed || t;
        if (interacted)
        {
            notInteracted = context.canceled;
        }
        Debug.Log(context.phase);
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
                text1.text = "Hold [Right Trigger] to charge flash";
            }
            else
            {
                text1.text = "Hold [Left Mouse Button] to charge flash";
            }

        }
    }

}
