using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] public GameObject canvas;
    [SerializeField] GameObject coinPrefab;
    float timeToSpawn = 0.9f;
    [SerializeField] float minTrans;
    [SerializeField] float maxTrans;

    private void Start()
    {
        StartCoroutine(Spawner()); 
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawner());
    }
   
    IEnumerator Spawner()
    {
        while(true)
        {
            var wanted = UnityEngine.Random.Range(minTrans, maxTrans);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(coinPrefab, position, Quaternion.identity);
            gameObject.transform.SetParent(canvas.transform);
            /*Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(transform.position.x, -transform.position.y * 2);
            rb.AddRelativeForce(force, ForceMode2D.Force);*/
            
            yield return new WaitForSeconds(timeToSpawn);
            Destroy(gameObject, 15f);
        }        
    }
}
