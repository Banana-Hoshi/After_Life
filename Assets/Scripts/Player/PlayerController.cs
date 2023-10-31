using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody body;
    public float speed = 5f;

    private Transform cameraTransform;

    public Vector2 moveDirection = Vector2.zero;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vCam;
    [SerializeField] GameObject playerSFX;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        body = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        if (PlayerDisable.IsPlayerDisabled())
        {
            PlayerDisable.ToggledDisabled();
        }
    }

    public void MovementUpdate(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (!PlayerDisable.IsPlayerDisabled())
        {
            Vector3 temp = new Vector3(moveDirection.x, 0f, moveDirection.y);
            Vector3 temp2 = cameraTransform.forward;
            temp2.y = 0;
            temp2.Normalize();
            Vector3 temp3 = cameraTransform.right;
            temp3.y = 0;
            temp3.Normalize();
            temp = temp2 * temp.z + temp3 * temp.x;
            body.velocity = new Vector3(temp.x * speed, body.velocity.y, temp.z * speed);
            playerSFX.SetActive(true);
        }
        else
        {
            playerSFX.SetActive(false);
        }
    }
}
