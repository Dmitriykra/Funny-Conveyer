using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] FoodPoolObject foodPoolObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawnFood());
    }

    IEnumerator StartSpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            GameObject food = foodPoolObject.GetObjectFromPool();
            food.transform.position = transform.position;
        }
    }
}
