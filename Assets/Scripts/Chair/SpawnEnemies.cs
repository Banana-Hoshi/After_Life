using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    Transform trans;

    public GameObject enemy;
    public List<Walls> walls;

    float spawnTimer;
    float rampTimer;
    float rampDelayTimer;
    float totalTime;
    public float spawnInterval;
    public float doubleSpawnPercentage;
    public bool active;
    bool leftClick;

    [Header("SFX")]
    [SerializeField] EventReference doorOpen;

    [Header("Ramp")]
    public float rampInterval;
    public float rampDelay;
    public float rampSpawnRate;
    public float rampDoubleSpawnPercentage;

    [Header("Random Acceleration")]
    public float randomAccelerationChance;
    public float randomAccelerationIncrease;
    public float randomAccelerationChanceRamp;
    public float randomAccelerationIncreaseRamp;

    [Header("Scene Manager")]
    public string nextScene;


    Vector3 lastSpawnLocation = new Vector3(0, 0, 0);
    int randWall;
    int randRow;
    int randDoor;

    public void OnLMB(InputAction.CallbackContext context)
    {
        leftClick = context.started || leftClick;
    }

    private void Start()
    {
        trans = this.gameObject.transform;
        for (int i = 0; i < trans.childCount; i++)
        {
            if (trans.GetChild(i).GetComponent<Walls>() != null)
            {
                walls.Add(trans.GetChild(i).gameObject.GetComponent<Walls>());
            }
        }
    }
    private void Update()
    {
        if (leftClick)
        {
            //active = true;
        }
        if (active)
        {
            totalTime += Time.deltaTime;
            spawnTimer += Time.deltaTime;
            if (rampDelay > rampDelayTimer)
            {
                rampTimer += Time.deltaTime;
            }
            else
            {
                rampDelayTimer += Time.deltaTime;
            }
            if (rampTimer > rampInterval)
            {
                rampTimer -= rampInterval;
                Ramp();
            }
            if (spawnTimer > spawnInterval)
            {
                if (Random.Range(0, 100) > doubleSpawnPercentage)
                {
                    SpawnEnemy();
                }
                else
                {
                    SpawnEnemy();
                    SpawnEnemy();
                }

                spawnTimer -= spawnInterval;
            }
            if (totalTime > 40f)
            {
                BedroomUtility.SetBadEnding(false);
                BedroomUtility.SetStage(2);
                SceneManager.LoadScene(nextScene);
            }
        }
    }
    private void FixedUpdate()
    {
        if (active)
        {
            if (Random.Range(0, 100) < randomAccelerationChance)
            {
                spawnTimer += randomAccelerationIncrease;
            }
        }
    }
    void SpawnEnemy()
    {
        while (randWall == lastSpawnLocation.x)
        {
            randWall = (int)Random.Range(-0.51f, walls.Count - 0.51f);
        }
        while (randRow == lastSpawnLocation.y)
        {
            randRow = (int)Random.Range(-0.51f, walls[randWall].row.Count - 0.51f);
        }
        while (randDoor == lastSpawnLocation.z)
        {
            randDoor = (int)Random.Range(-0.51f, walls[randWall].row[randRow].doors.Count - 0.51f);
        }
        lastSpawnLocation = new Vector3(randWall, randRow, randDoor);
        Transform spawnTrans = walls[randWall].row[randRow].doors[randDoor].transform;
        RuntimeManager.PlayOneShotAttached(doorOpen, spawnTrans.gameObject);
        GameObject newEnemy = Instantiate(enemy, spawnTrans.position, spawnTrans.rotation);
        randDoor = (int)Random.Range(-0.51f, walls[randWall].row[randRow].doors.Count - 0.51f);
        newEnemy.GetComponent<SpiderController>().runawayPosition = walls[randWall].row[randRow].doors[randDoor].transform;
    }
    void Ramp()
    {
        spawnInterval -= rampSpawnRate;
        rampInterval -= rampTimer;
        doubleSpawnPercentage += rampDoubleSpawnPercentage;
        randomAccelerationChance += randomAccelerationChanceRamp;
        randomAccelerationIncrease += randomAccelerationIncreaseRamp;
    }
}
