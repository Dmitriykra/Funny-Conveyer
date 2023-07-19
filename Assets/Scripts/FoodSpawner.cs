using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] FoodPoolObject foodPoolObject;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        StartCoroutine(StartSpawnFood());
    }

    IEnumerator StartSpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject food = foodPoolObject.GetObjectFromPool();
        food.transform.position = transform.position;
    }
}
