using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject monsterHolder, previousFloor, nextFloor, door;
    int n = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monsterHolder.SetActive(true);
            previousFloor.SetActive(false);
            nextFloor.SetActive(true);
            door.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
            door.GetComponent<DoorControllerRotate>().canOpen = false;
            door.GetComponent<DoorControllerRotate>().enabled = false;
            if (n == 0)
            {
                FindObjectOfType<TaskList>().SetTask("Go to sleep.");
                n++;
            }
            else if (n == 1)
            {
                FindObjectOfType<TaskList>().SetTask("Sleep.");
                n++;
            }
            else if (n == 2)
            {
                FindObjectOfType<TaskList>().SetTask("Goodnight.");
                n++;
            }

        }
    }
}
