using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> UIObjects;
    bool menuActive;

    public void GetEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!PlayerDisable.IsPlayerDisabled() || menuActive)
            {
                ToggleMenu();
            }
        }
    }
    public void SkipLevel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BedroomUtility.SetBadEnding(false);
            BedroomUtility.SetStage((int)(context.ReadValue<float>()));
            SceneManager.LoadScene("Bedroom");
        }
    }
    public void ToggleMenu()
    {
        if (menuActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        menuActive = !menuActive;
        for (int i = 0; i < UIObjects.Count; i++)
        {
            UIObjects[i].SetActive(menuActive);
        }
        PlayerDisable.ToggledDisabled();
        if (menuActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
