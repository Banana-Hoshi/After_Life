using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class TutorialBook : Interactable
{

    [SerializeField] GameObject[] speechBubbles;
    [SerializeField] GameObject interactableUI;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] pages;
    [SerializeField] TextMeshProUGUI controlType, space;
    [SerializeField] float readTime;
    TaskList taskList;
    bool interact;
    Bed bed;
    string description = "Press [E] to read book";
    public string tutorialText = "Press TAB to see your current task.";
    int n;

    private void Awake()
    {
        taskList = FindObjectOfType<TaskList>();
        PlayerInput input = FindObjectOfType<PlayerInput>();
    }

    private void OnEnable()
    {
        InputUser.onChange += OnInputDeviceChange;
    }

    private void OnDisable()
    {
        InputUser.onChange -= OnInputDeviceChange;
    }

    private void Start()
    {
        bed = FindObjectOfType<Bed>(false);
        n = Random.Range(0, speechBubbles.Length);
    }
    public void PressSpace(InputAction.CallbackContext context)
    {
        interact = context.performed;
        Debug.Log("Space");
    }

    public override void Interact()
    {
        StartCoroutine(ReadBook());
    }
    public override string GetDescription()
    {
        return description;
    }

    public override int GetSpeechBubble()
    {
        return n;
    }

    public IEnumerator ReadBook()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        interactableUI.SetActive(false);
        PlayerDisable.ToggledDisabled();
        canvas.SetActive(true);
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(true);
            yield return new WaitForSeconds(readTime);
            interactableUI.SetActive(true);

            yield return new WaitUntil(() => interact);
            pages[i].SetActive(false);
            interactableUI.SetActive(false);
        }
        PlayerDisable.ToggledDisabled();
        canvas.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        interactableUI.SetActive(true);
        bed.canSleep = true;
        taskList.SetTask(tutorialText);
        yield return new WaitForSeconds(6f);
        taskList.SetTask("Go to bed");
    }

    void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            if (user.controlScheme.Value.name.Equals("Gamepad"))
            {
                controlType.text = "Right Trigger";
                space.text = "[A Button]";
                description = "Press [A] to read book";
                tutorialText = "Press X to see your current task.";
            }
            else
            {
                controlType.text = "Left Mouse Button";
                space.text = "[Space]";
                description = "Press [E] to read book";
                tutorialText = "Press TAB to see your current task.";
            }

        }
    }
}
