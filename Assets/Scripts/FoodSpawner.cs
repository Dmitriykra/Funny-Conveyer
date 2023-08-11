using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class FoodSpawner : MonoBehaviour, IAccelerable
{
    [SerializeField] FoodPoolObject foodPoolObject;

    private float timeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = 1.5f;
        Spawn();
        StartCoroutine(StartSpawnFood());
    }

    IEnumerator StartSpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject food = foodPoolObject.GetObjectFromPool();
        food.transform.position = transform.position;
    }

    public void IncreaseSpeed()
    {
        timeToSpawn -= 0.01f;
    }
}
