using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class TetherRobots : MonoBehaviour
{
    bool active;
    Rigidbody rb;
    Transform playerTrans;
    [SerializeField] float speed;
    [SerializeField] float movementLimit;
    [SerializeField] PowerBox box;
    [SerializeField] GameObject particles;
    [SerializeField] HydraulicPressButton button;
    [SerializeField] TetherRobotTrigger trigger;
    [SerializeField] GameObject plug, face, hand;
    [SerializeField] string nextScene;
    RigidbodyConstraints constraint;

    CinemachineVirtualCamera playerCam, kimCam;
    int grabs = 0;
    [SerializeField] int maxGrabs;
    Animator animator;
    bool unplugged;
    Vector3 originalPos;
    private void Start()
    {
        originalPos = plug.transform.position;
        rb = GetComponent<Rigidbody>();
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        constraint = GetComponent<Rigidbody>().constraints;
        kimCam = GameObject.FindGameObjectWithTag("kimGrabCam").GetComponent<CinemachineVirtualCamera>();
        playerCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if (box != null)
        {
            active = box.GetSwitchDown();
        }
        if (button != null)
        {
            active = button.GetSwitchDown();
        }
        if (active && !unplugged)
        {
            particles.SetActive(true);
            //ACTIVATE THE ROBOT
            if (trigger.GetChasing())
            {
                transform.LookAt(playerTrans.position);
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y - 90, 0));
                animator.SetBool("isWireStuck", false);
                animator.SetBool("isRunning1", true);
                Vector3 temp = (playerTrans.position - transform.position).normalized;
                temp.y = -0.5f;
                rb.velocity = speed * temp;
                if (Vector3.Distance(originalPos, transform.position) > movementLimit)
                {
                    animator.SetBool("isRunning1", false);
                    animator.SetBool("isWireStuck", true);
                    //BIND THE ROBOT TO YANK ON THE CORD
                    temp = (transform.position - originalPos).normalized;
                    //temp.y = -0.5f;
                    transform.position = originalPos + temp * movementLimit;
                }
            }
        }
        else
        {
            animator.SetBool("isRunning1", false);
            animator.SetBool("isWireStuck", false);
            particles.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (active && !unplugged)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(KillPlayer());
            }
        }
    }
    public void Unplug()
    {
        unplugged = true;
        particles.SetActive(false);
    }

    IEnumerator KillPlayer()
    {
        if (grabs >= maxGrabs - 1)
        {
            active = false;
            animator.SetBool("isRunning1", false);
            animator.SetBool("isWireStuck", false);
            animator.SetBool("isGrab", true);
            PlayerDisable.ToggledDisabled();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(1f);
            playerCam.enabled = false;
            kimCam.enabled = true;
            kimCam.Follow = hand.transform;
            kimCam.GetCinemachineComponent<CinemachineHardLookAt>().enabled = true;
            kimCam.LookAt = face.transform;
            yield return new WaitForSeconds(2.5f);
            GetComponent<Rigidbody>().constraints = constraint;
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().AddForce((-transform.right) * 5, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.5f);
            kimCam.GetComponent<Collider>().enabled = false;
            kimCam.GetComponent<Rigidbody>().useGravity = true;
            kimCam.GetComponent<Rigidbody>().AddForce((-transform.right) * 5, ForceMode.VelocityChange);
            kimCam.LookAt = null;
            kimCam.Follow = null;
            yield return new WaitForSeconds(1f);
            BedroomUtility.SetBadEnding(true);
            BedroomUtility.AddAttempt(1);
            BedroomUtility.SetStage(3);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            active = false;
            animator.SetBool("isRunning1", false);
            animator.SetBool("isWireStuck", false);
            animator.SetBool("isGrab", true);
            PlayerDisable.ToggledDisabled();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(0.5f);
            playerCam.enabled = false;
            kimCam.enabled = true;
            kimCam.Follow = hand.transform;
            kimCam.GetCinemachineComponent<CinemachineHardLookAt>().enabled = true;
            kimCam.LookAt = face.transform;
            yield return new WaitForSeconds(2f);
            kimCam.enabled = false;
            playerCam.enabled = true;
            kimCam.transform.position = playerCam.transform.position;
            PlayerDisable.ToggledDisabled();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            GetComponent<Rigidbody>().AddForce((-transform.forward + transform.up) * 20, ForceMode.Impulse);
            active = true;
            animator.SetBool("isGrab", false);
            grabs++;
            yield return new WaitForSeconds(1f);
            GetComponent<Rigidbody>().constraints = constraint;
        }
    }
}
