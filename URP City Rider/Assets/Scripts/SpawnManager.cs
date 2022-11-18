using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] RoadPrefabsSpawner;
    public GameObject[] EnvironmentPrefabsSpawner;
    public int RoadOnScreen = 10;

    private Transform playerTransform;
    private float RoadZ = 0.0f; // start pos of road
    private float OffsetRoad = 24.0f; // offset road length for next spawn
    private float SafeZone = 30.0f;
    private int lastEnvIndex = 0;
    private int randomEnvIndex;
    private int randomEnvEvent;
    private int randomRoadIndex;
    private int randomRoadEvent;
    private int Event;
    private int startRoad = 0;

    private int nowRoad;
    private int nowEnvRight;
    private int nowEnvLeft;


    private List<GameObject> ActiveRoad;
    private List<GameObject> ActiveEnvironmentRight;
    private List<GameObject> ActiveEnvironmentLeft;


    // Start is called before the first frame update
    void Start()
    {
        randomEnvEvent = 0;
        Event = 0;
        ActiveRoad = new List<GameObject>();
        ActiveEnvironmentRight = new List<GameObject>();
        ActiveEnvironmentLeft = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < RoadOnScreen; i++)
        {
            SpawnRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (playerTransform.position.z - SafeZone > (RoadZ - RoadOnScreen * OffsetRoad)) // 366
        {
            SpawnRoad();
            DeleteRoad();
        }
    }

    void SpawnRoad(int prefabIndex = -1)
    {
        GameObject road;
        GameObject environmetLeft;
        GameObject environmetRight;
        nowRoad = RandomRoad();
        nowEnvRight = RandomEnvironment();
        nowEnvLeft= RandomEnvironment();
        
        if (nowRoad > 32 && nowRoad < 39)
        {
            nowEnvRight = Random.Range(37, 45);
            nowEnvLeft = Random.Range(37, 45);
        }
        if (nowRoad > 38 && nowRoad < 45)
                nowEnvLeft = Random.Range(37, 45);

        if (nowRoad > 44 && nowRoad < 51)
                nowEnvRight = Random.Range(37, 45);
       

        road = Instantiate(RoadPrefabsSpawner[nowRoad]) as GameObject;
        environmetLeft = Instantiate(EnvironmentPrefabsSpawner[nowEnvLeft]) as GameObject;
        environmetRight = Instantiate(EnvironmentPrefabsSpawner[nowEnvRight]) as GameObject;

        road.transform.SetParent(transform);
        environmetLeft.transform.SetParent(transform);
        environmetRight.transform.SetParent(transform);

        road.transform.position = Vector3.forward * RoadZ;
        environmetLeft.transform.position = Vector3.forward * RoadZ;
        environmetRight.transform.position = Vector3.forward * RoadZ;

        environmetRight.transform.localScale = new Vector3(-1f, 1f, 1f);

        RoadZ += OffsetRoad;
        ActiveRoad.Add(road);
        ActiveEnvironmentLeft.Add(environmetLeft);
        ActiveEnvironmentRight.Add(environmetRight);
    }
    void DeleteRoad(int prefabIndex = -2)
    {
        Destroy(GameObject.Find("Scenee"));
        Destroy(ActiveRoad[0]);
        Destroy(ActiveEnvironmentLeft[0]);
        Destroy(ActiveEnvironmentRight[0]);

        ActiveRoad.RemoveAt(0);
        ActiveEnvironmentLeft.RemoveAt(0);
        ActiveEnvironmentRight.RemoveAt(0);

    }
    private int RandomEnvironment()
    {

        if (EnvironmentPrefabsSpawner.Length <= 1)
            return 0;

        Event++;
        if (Event > 20)
        {
            randomEnvIndex = lastEnvIndex;
            randomEnvEvent = Random.Range(0, 3);            
            Event = 0;
        }

        if (randomEnvEvent == 0) // Suburbs
        {
            randomEnvIndex = Random.Range(0, 10);
        }

        else if (randomEnvEvent == 1) // Shop
        {
            randomEnvIndex = Random.Range(10, 15);
        }
        else if (randomEnvEvent == 2) // Downtown
        {
            randomEnvIndex = Random.Range(15, 27);
        }
        else if (randomEnvEvent == 3) // Park
        {
            randomEnvIndex = Random.Range(27, 37);
        }
        lastEnvIndex = randomEnvEvent;
        return randomEnvIndex;
    }
    private int RandomRoad()
    {

        if (RoadPrefabsSpawner.Length <= 1)
            return 0;
        if (startRoad > 3) { 
            randomRoadEvent = Random.Range(0, 3);

            if (randomRoadEvent == 0) // ClearRoad
            {
                randomRoadIndex = Random.Range(0, 5);
            }

            if (randomRoadEvent == 1) // ObstRoad
            {
                randomRoadIndex = Random.Range(5, 33);

            }
            if (randomRoadEvent == 2) // XRoad
            {
                randomRoadIndex = Random.Range(33, 51);
            }
        }
        else
            randomRoadIndex = Random.Range(0, 5);
        startRoad++;

        return randomRoadIndex;
    }
}
