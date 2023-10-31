using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRobotSpawns : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnpoints;
    [SerializeField] GameObject robot;
    GameObject player;
    float timer;
    [HideInInspector] public bool canSpawn = true;
    [SerializeField] float spawnTime;
    Vector3 spawnOffset = new Vector3(0, 10, 0);
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnpoints.Add(transform.GetChild(i).gameObject);
        }
        player = GameObject.Find("Player");
        canSpawn = false;
    }
    private void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                timer -= spawnTime;
                GameObject spawnpoint = spawnpoints[0];
                for (int i = 1; i < spawnpoints.Count; i++)
                {
                    if (Vector3.Distance(player.transform.position, spawnpoint.transform.position) > Vector3.Distance(player.transform.position, spawnpoints[i].transform.position))
                    {
                        if (Vector3.Distance(player.transform.position, spawnpoints[i].transform.position) > 4)
                        {
                            spawnpoint = spawnpoints[i];

                        }
                    }
                }
                Instantiate(robot, spawnpoint.transform.position + spawnOffset, spawnpoint.transform.rotation);
            }
        }
    }
}
