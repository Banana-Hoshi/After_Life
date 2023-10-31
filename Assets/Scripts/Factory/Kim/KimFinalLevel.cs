using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KimFinalLevel : MonoBehaviour
{
    bool active = true;
    Rigidbody rb;
    Transform playerTrans;
    [SerializeField] float speed;
    [SerializeField] float movementLimit;
    [SerializeField] TetherRobotTrigger trigger;
    [SerializeField] GameObject plug;
    [SerializeField] string nextScene;
    RigidbodyConstraints constraint;

    CinemachineVirtualCamera playerCam, kimCam;
    int grabs = 0;
    [SerializeField] int maxGrabs;
    Animator animator;
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
        if (active)
        {
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
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!animator.GetBool("isDying"))
            {
                StartCoroutine(KillPlayer());
            }
        }
    }

    IEnumerator KillPlayer()
    {
        if (grabs > maxGrabs)
        {
            animator.SetBool("isGrabbing", false);
            animator.SetBool("isRunning1", false);
            animator.SetBool("isWireStuck", false);
            animator.SetBool("isKill", true);
            PlayerDisable.ToggledDisabled();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(1f);
            playerCam.enabled = false;
            kimCam.enabled = true;
            kimCam.GetComponent<Rigidbody>().useGravity = true;
            kimCam.GetComponent<Rigidbody>().isKinematic = false;
            kimCam.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 2, ForceMode.Impulse);
            kimCam.GetCinemachineComponent<CinemachineHardLookAt>().enabled = true;
            yield return new WaitForSeconds(2.5f);
            GetComponent<Rigidbody>().constraints = constraint;
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().AddForce((-transform.right) * 5, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.5f);
            kimCam.GetComponent<Collider>().enabled = false;
            kimCam.GetComponent<Rigidbody>().AddForce((-transform.right) * 5, ForceMode.VelocityChange);
            kimCam.LookAt = null;
            yield return new WaitForSeconds(1f);
            BedroomUtility.SetBadEnding(true);
            BedroomUtility.AddAttempt(1);
            BedroomUtility.SetStage(4);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            animator.SetBool("isGrabbing", true);
            PlayerDisable.ToggledDisabled();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(0.5f);
            playerCam.enabled = false;
            kimCam.enabled = true;
            kimCam.GetComponent<Rigidbody>().useGravity = true;
            kimCam.GetComponent<Rigidbody>().isKinematic = false;
            kimCam.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 2, ForceMode.Impulse);
            kimCam.GetCinemachineComponent<CinemachineHardLookAt>().enabled = true;
            yield return new WaitForSeconds(2f);
            kimCam.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 2, ForceMode.Impulse);
            kimCam.GetComponent<Rigidbody>().isKinematic = true;
            kimCam.GetComponent<Rigidbody>().useGravity = false;
            kimCam.enabled = false;
            playerCam.enabled = true;
            kimCam.transform.position = playerCam.transform.position;
            PlayerDisable.ToggledDisabled();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            GetComponent<Rigidbody>().AddForce((-transform.forward + transform.up) * 20, ForceMode.Impulse);
            animator.SetBool("isGrabbing", false);
            grabs++;
            yield return new WaitForSeconds(1f);
            GetComponent<Rigidbody>().constraints = constraint;
        }
    }
}
