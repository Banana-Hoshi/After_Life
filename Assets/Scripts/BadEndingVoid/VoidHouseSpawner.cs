using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidHouseSpawner : MonoBehaviour
{
    Transform player;
    DoorControllerRotate[] doors;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        doors = FindObjectsOfType<DoorControllerRotate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("UpTrigger"))
            {
                player.position -= new Vector3(0, 10.5f, 0);
            }
            else
            {
                player.position += new Vector3(0, 10.5f, 0);
            }
        }
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].doorOpen)
            {
                doors[i].finalLevel = true;
                doors[i].doorOpen = false;
                doors[i].transform.localRotation = Quaternion.Euler(doors[i].transform.localEulerAngles.x, doors[i].transform.localEulerAngles.y, 0f);
                doors[i].reproduced = false;
                doors[i].reproduced2 = true;
                doors[i].finalLevel = false;
            }
        }
        
    }
}
