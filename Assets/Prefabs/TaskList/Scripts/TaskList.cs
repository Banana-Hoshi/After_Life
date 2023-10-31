using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Users;

public class TaskList : MonoBehaviour
{
    Scene scene;

    Animator anim;
    Animator indicatorAnim;

    [SerializeField] GameObject toggleIndicator;
    [SerializeField] TextMeshProUGUI inputText;

    public TextMeshProUGUI task;

    string taskMessage;
    string newTaskMessage;

    bool toggled;
    bool canToggle;
    bool inputCalled;
    [HideInInspector] public bool startTutorial;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        anim = GetComponent<Animator>();
        indicatorAnim = toggleIndicator.GetComponent<Animator>();
        task.SetText("");
        taskMessage = "Current Task: ";
        newTaskMessage = "New Task: ";
    }

    // Update is called once per frame
    void Update()
    {
        if (inputCalled && canToggle)
        {
            StartCoroutine(ToggleTaskList());
        }
    }

    IEnumerator ToggleTaskList()
    {
        canToggle = false;
        toggled = !toggled;
        anim.SetBool("Toggled", toggled);
        indicatorAnim.SetBool("Toggled", toggled);
        yield return new WaitForSeconds(0.5f);
        canToggle = true;
    }

    public void SetTask(string newTask)
    {
        canToggle = false;

        if (anim.GetBool("NewTask") == false)
        {
            anim.SetBool("Toggled", false);
            indicatorAnim.SetBool("Toggled", false);

            if (toggled)
            {
                StartCoroutine(StartNewTask(newTask));
            }
            else
            {
                task.SetText(newTaskMessage + newTask);
                anim.SetBool("NewTask", true);
            }
            StartCoroutine(ResetNewTask(newTask));
        }
    }

    IEnumerator ResetNewTask(string currentTask)
    {
        yield return new WaitForSeconds(3.75f);
        anim.SetBool("NewTask", false);
        task.SetText(taskMessage + currentTask);
        toggled = false;
        canToggle = true;
    }

    IEnumerator StartNewTask(string currentTask)
    {
        yield return new WaitForSeconds(0.75f);
        task.SetText(newTaskMessage + currentTask);
        anim.SetBool("NewTask", true);
    }

    public void GetInput(InputAction.CallbackContext context)
    {
        inputCalled = context.performed;
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
                inputText.text = "[X]";
            }
            else
            {
                inputText.text = "[Tab]";
            }
        }
    }
}
