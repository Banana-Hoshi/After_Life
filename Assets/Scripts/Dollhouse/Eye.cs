using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] Transform player;
    private void Start()
    {
        
    }
    void Update()
    {
        transform.forward = new Vector3(transform.position.x + 90f, transform.position.y, transform.position.z) - player.position;
        transform.LookAt(player);
    }
}
