using Cinemachine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Interactable
{
    bool interacted;
    [SerializeField] GameObject[] speechbubble;
    [SerializeField] WaterCutscene waterCutscene;
    [SerializeField] EventReference sinkSFX;
    [SerializeField] FinalLevelController finalLevelCutscene;
    [SerializeField] GameObject player;
    Bed _bed;
    int n;

    private void Start()
    {
        n = Random.Range(0, speechbubble.Length);
        _bed = FindObjectOfType<Bed>();
    }

    public override void Interact()
    {
        if (!interacted)
        {
            if (waterCutscene.grabbed)
            {
                StartCoroutine(FillGlass());
                interacted = true;
                _bed.canSleep = true;
                FindObjectOfType<TaskList>().SetTask("Go to bed");
            }
        }
    }
    public override string GetDescription()
    {
        if (waterCutscene.grabbed && !interacted)
        {
            return "Fill glass with water?";

        }
        else
        {
            return "";
        }
    }
    public override int GetSpeechBubble()
    {
        return n;
    }

    IEnumerator FillGlass()
    {
        float timer = 0;
        waterCutscene.fillWater = true;
        PlayerDisable.ToggledDisabled();
        CinemachineVirtualCamera playerCam = player.GetComponentInChildren<CinemachineVirtualCamera>();
        playerCam.enabled = false;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            Camera.main.gameObject.transform.Rotate(Vector3.right, Mathf.MoveTowards(-Camera.main.gameObject.transform.rotation.x, 30f, Time.deltaTime * 10f));
            yield return null;
        }
        if (finalLevelCutscene != null)
        {
            finalLevelCutscene.enableMindMan = true;
        }
        RuntimeManager.PlayOneShotAttached(sinkSFX, gameObject);
        yield return new WaitForSeconds(4f);
        waterCutscene.fillWater = false;
        playerCam.ForceCameraPosition(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.rotation);
        yield return new WaitForSeconds(2f);
        playerCam.enabled = true;
        waterCutscene.grabbed = false;
        PlayerDisable.ToggledDisabled();
        GetComponent<Collider>().enabled = false;
        waterCutscene.gameObject.SetActive(false);
    }
}
