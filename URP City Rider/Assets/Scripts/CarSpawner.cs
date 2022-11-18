using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] Cars;
    private List<GameObject> ActiveCars;
    public int CarsOnScreen = 1;
    private float CarHere;
    private float CarZ = 24.0f; // start pos of road
    private float OffsetRoad = 24.0f; // offset road length for next spawn
    private Transform playerTransform;
    private int randomCar;
    private int randomPosCar;
    private int randomPosCarZ;
    GameObject car;

    void Start()
    {
        CarHere = 48f;
        ActiveCars = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform.position.z - 30f > CarHere) 
        {
            SpawnCar();
            CarHere += 48f;
        }
    }

    private void SpawnCar(int prefabIndex = -1)
    {
            randomCar = Random.Range(0, 5);
            randomPosCar = Random.Range(0, 4);
            randomPosCarZ = Random.Range(12, 24);

            car = Instantiate(Cars[randomCar]);

            car.transform.SetParent(transform);
            if (randomPosCar == 0)
            {
                car.transform.position = new Vector3(-3.15f, 0.1f, CarZ + randomPosCarZ);
                car.transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            else if (randomPosCar == 1)
            {
                car.transform.position = new Vector3(-1.15f, 0.1f, CarZ + randomPosCarZ);
                car.transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            else if (randomPosCar == 2)
            {
                car.transform.position = new Vector3(1.15f, 0.1f, CarZ + randomPosCarZ);
                car.transform.eulerAngles = new Vector3(0, 0f, 0);
            }
            else if (randomPosCar == 3)
            {
                car.transform.position = new Vector3(3.15f, 0.1f, CarZ + randomPosCarZ);
                car.transform.eulerAngles = new Vector3(0, 0f, 0);
            }
        
        CarZ = playerTransform.position.z + 100f;
        ActiveCars.Add(car);
    }
}
