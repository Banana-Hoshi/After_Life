using Cinemachine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class FreeRoamRobot : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    float timer;
    [SerializeField] public float duration;
    bool robotBroken, lookingAtPlayer;
    int grabs = 0;
    [SerializeField] int maxGrabs;
    [SerializeField] GameObject spotLight;
    [SerializeField] string nextScene;
    [SerializeField] EventReference robotDrop;
    [SerializeField] GameObject soundPlayer;
    CinemachineVirtualCamera playerCam, kimCam;
    [SerializeField] GameObject face, spine;
    int stage;
    FallingRobotSpawns spawner;
    bool grabbing;
    Animator animator;
    RigidbodyConstraints constraint;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Bedroom")
        {
            stage = 4;
            nextScene = "FinalLevelRespawn";
        }
        else
        {
            stage = 3;
            nextScene = "Bedroom";
        }
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        kimCam = GameObject.FindGameObjectWithTag("kimGrabCam").GetComponent<CinemachineVirtualCamera>();
        playerCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
        animator = GetComponent<Animator>();
        animator.SetBool("isDying", true);
        grabs = 0;
        constraint = GetComponent<Rigidbody>().constraints;
        spawner = GameObject.Find("Spawnpoints for falling robots").GetComponent<FallingRobotSpawns>();
    }
    private void Update()
    {
        if (agent.enabled)
        {
            agent.destination = player.transform.position;
            timer += Time.deltaTime;
            if (timer > duration && !grabbing)
            {
                RobotBreak();
            }
        }
        if (lookingAtPlayer) spine.transform.LookAt(kimCam.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "kimFloor" && !robotBroken)
        {
            spotLight.SetActive(false);
            animator.SetBool("isDying", false);
            animator.SetBool("isCrawling", true);
            RuntimeManager.PlayOneShotAttached(robotDrop, soundPlayer);
            agent.enabled = true;
            agent.destination = player.transform.position;
        }
        if (collision.gameObject.tag == "Player")
        {
            if (!animator.GetBool("isDying"))
            {
                StartCoroutine(GrabPlayer());
            }
        }
    }
    private void RobotBreak()
    {
        animator.SetBool("isCrawling", false);
        robotBroken = true;
        agent.speed = 0;
        StartCoroutine(RobotDespwan());
    }
    private IEnumerator RobotDespwan()
    {
        animator.SetBool("isDying", true);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
    IEnumerator GrabPlayer()
    {
        grabbing = true;
        if (grabs > maxGrabs)
        {
            spawner.canSpawn = false;
            animator.SetBool("isGrabbing", false);
            animator.SetBool("isCrawling", false);
            animator.SetBool("isDying", false);
            animator.SetBool("isKill", true);
            PlayerDisable.ToggledDisabled();
            agent.enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(1f);
            playerCam.enabled = false;
            kimCam.enabled = true;
            kimCam.GetComponent<Rigidbody>().useGravity = true;
            kimCam.GetComponent<Rigidbody>().isKinematic = false;
            kimCam.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 2, ForceMode.Impulse);
            kimCam.LookAt = face.transform;
            kimCam.GetCinemachineComponent<CinemachineHardLookAt>().enabled = true;
            yield return new WaitForSeconds(2.5f);
            GetComponent<Rigidbody>().constraints = constraint;
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().AddForce((-transform.right) * 5, ForceMode.VelocityChange);
            lookingAtPlayer = true;
            yield return new WaitForSeconds(0.5f);
            kimCam.GetComponent<Collider>().enabled = false;
            kimCam.GetComponent<Rigidbody>().AddForce((-transform.right) * 5, ForceMode.VelocityChange);
            kimCam.LookAt = null;
            yield return new WaitForSeconds(1f);
            BedroomUtility.SetBadEnding(true);
            BedroomUtility.AddAttempt(1);
            BedroomUtility.SetStage(stage);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            spawner.canSpawn = false;
            animator.SetBool("isGrabbing", true);
            PlayerDisable.ToggledDisabled();
            agent.enabled = false;
            GetComponent<BoxCollider>().size = new Vector3(0.01f, 1f, 0.01f);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(0.5f);
            playerCam.enabled = false;
            kimCam.enabled = true;
            kimCam.GetComponent<Rigidbody>().useGravity = true;
            kimCam.GetComponent<Rigidbody>().isKinematic = false;
            kimCam.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 2, ForceMode.Impulse);
            kimCam.LookAt = face.transform;
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
            GetComponent<Rigidbody>().AddForce(((-transform.forward * 50) + (transform.up * 10)), ForceMode.Impulse);
            animator.SetBool("isGrabbing", false);
            grabs++;
            yield return new WaitForSeconds(5f);
            GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
            agent.enabled = true;
            GetComponent<Rigidbody>().constraints = constraint;
            spawner.canSpawn = true;
        }
        grabbing = false;
    }
}
