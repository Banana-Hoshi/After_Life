using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] Transform[] positions;
    int i;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Length;
    }
    private void Update()
    {

        if (i < positions.Length)
        {
            lineRenderer.SetPosition(i, positions[i].position);
            i++;
        }
        else
        {
            i = 0;
        }

    }
}
