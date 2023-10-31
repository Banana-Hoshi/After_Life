using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCutscene : Interactable
{
    int n;
    public bool grabbed;
    public bool fillWater;
    [SerializeField] GameObject[] speechbubble;
    [SerializeField] GameObject player;
    [SerializeField] GameObject sink;


    private void Start()
    {
        n = Random.Range(0, speechbubble.Length);
    }

    private void Update()
    {
        if (grabbed && !fillWater)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 10f);
        }
        if (fillWater)
        {
            transform.position = Vector3.MoveTowards(transform.position, sink.transform.position, Time.deltaTime * 10f);
        }
    }

    public override void Interact()
    {
        if (!grabbed)
        {
            GetComponent<Collider>().enabled = false;
            grabbed = true;
        }
    }
    public override string GetDescription()
    {
        return "Grab empty glass?";
    }

    public override int GetSpeechBubble()
    {
        return n;
    }
}
