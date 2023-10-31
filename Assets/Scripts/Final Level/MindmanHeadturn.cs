using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindmanHeadturn : MonoBehaviour
{

    Transform playerCam;


    private void Start()
    {
        //transform.forward = new Vector3(0, 1, 0);
        playerCam = GameObject.Find("Main Camera").transform;
    }

    private void Update()
    {
        //bone.rotation.SetLookRotation(playerCam.position.normalized, transform.up);
    }
}
