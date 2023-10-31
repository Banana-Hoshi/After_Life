using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDespawner : MonoBehaviour
{
    [SerializeField] GameObject monsterHolder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monsterHolder.SetActive(false);
        }
    }
}
