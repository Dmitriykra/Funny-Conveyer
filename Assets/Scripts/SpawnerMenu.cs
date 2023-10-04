using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;

    private int foodSize = 1500;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawndFood());
    }

    IEnumerator SpawndFood()
    {
        while (true) 
        {
            int randomIndex = Random.Range(0, _gameObjects.Length);
            var spawnRange = Random.Range(150, 200);
            var position = new Vector3(spawnRange, transform.position.y, transform.position.z);
            GameObject newFood = Instantiate(_gameObjects[randomIndex], position, transform.rotation);
            newFood.transform.SetParent(this.transform);
            newFood.transform.localScale = new Vector3(foodSize, foodSize, foodSize);
            Rigidbody rb = newFood.GetComponent<Rigidbody>();
            //rb.AddRelativeForce(transform.position.x, - transform.position.y * 2, transform.position.z);
            yield return new WaitForSeconds(1f);
            Destroy(newFood, 7f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
