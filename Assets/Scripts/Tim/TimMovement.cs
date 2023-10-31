using Cinemachine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class TimMovement : MonoBehaviour
{
    [SerializeField] List<Transform> firstArea;
    [SerializeField] List<Transform> secondArea;
    [SerializeField] float waitTime;
    [SerializeField] float killDistance;
    bool secondAreaEntered;
    Vector3 destination;
    NavMeshAgent agent;

    float waitTimer, chasingTimer;
    bool waiting;
    [SerializeField] float speed;

    public bool chasing;
    [SerializeField] GameObject player;

    [SerializeField] Animator anim;
    [SerializeField] GameObject timCam;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject timHead;
    [SerializeField] Animator doll;
    [SerializeField] GameObject breath;

    [SerializeField] string nextScene;

    float freezeTime;
    float freezeTimer;
    bool frozen;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (firstArea != null && firstArea.Count > 0)
        {
            NewDestination();
        }
        speed = agent.speed;
        playerCam = player.GetComponentInChildren<CinemachineVirtualCamera>().gameObject;
    }

    void Update()
    {
        if (firstArea != null && firstArea.Count > 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < killDistance)
            {
                StartCoroutine(Kill());
            }
            else
            {
                CheckPlayerSight();
                freezeTimer += Time.deltaTime;
                if (freezeTimer >= freezeTime && frozen)
                {
                    Unfreeze();
                }
                // Update destination if the target moves one unit
                if (Vector3.Distance(transform.position, agent.destination) < 1.0f)
                {
                    waiting = true;
                    chasing = false;
                }
                if (waiting)
                {
                    agent.speed = 0f;
                    waitTimer += Time.deltaTime;
                    if (waitTimer > waitTime)
                    {
                        waitTimer = 0;
                        waiting = false;
                        agent.speed = speed;
                        NewDestination();
                    }
                }
                if (chasing)
                {
                    chasingTimer += Time.deltaTime;
                    if (chasingTimer > 10)
                    {
                        chasingTimer = 0;
                        chasing = false;
                        NewDestination();
                    }
                }
                WalkCheck();
                timHead.transform.LookAt(playerCam.transform, playerCam.transform.up);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < killDistance)
            {
                StartCoroutine(Kill());
            }
            else
            {
                freezeTimer += Time.deltaTime;
                if (freezeTimer >= freezeTime && frozen)
                {
                    Unfreeze();
                }
                agent.destination = player.transform.position;
                WalkCheck();
                timHead.transform.LookAt(playerCam.transform, playerCam.transform.up);
            }
        }
    }
    void NewDestination()
    {
        if (!secondAreaEntered)
        {
            destination = firstArea[Random.Range(0, firstArea.Count - 1)].position;
        }
        else
        {
            destination = secondArea[Random.Range(0, secondArea.Count - 1)].position;
        }
        agent.destination = destination;
    }
    void NewDestination(Vector3 des)
    {
        agent.destination = des;
    }
    public void EnteredSecondArea()
    {
        secondAreaEntered = true;
        NewDestination();
        //insert tp here if needed
    }
    void CheckPlayerSight()
    {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                NewDestination(hit.collider.transform.position);
                chasing = true;
            }
        }
    }
    public void Freeze(float freeze)
    {
        freezeTimer = 0;
        frozen = true;
        if (freezeTime - freezeTimer < freeze)
        {
            freezeTime = freeze;
        }
        agent.speed = 0;
        //INSERT TIM FREEZE CODE HERE (SOUND)
    }
    public void Unfreeze()
    {
        agent.speed = speed;
        frozen = false;
    }

    public void WalkCheck()
    {
        if (agent.speed >= 1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
    public IEnumerator Kill()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isKill", true);
        doll.gameObject.SetActive(true);
        doll.SetBool("isKill", true);
        player.SetActive(false);
        playerCam = timCam;
        timCam.SetActive(true);
        breath.SetActive(false);
        yield return new WaitForSeconds(5f);
        if (SceneManager.GetActiveScene().name == "Bedroom")
        {
            BedroomUtility.SetBadEnding(true);
            BedroomUtility.SetStage(4);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            BedroomUtility.SetBadEnding(true);
            BedroomUtility.AddAttempt(1);
            BedroomUtility.SetStage(2);
            SceneManager.LoadScene(nextScene);
        }
        
    }
}
