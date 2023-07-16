using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPoolObject : MonoBehaviour
{
    [SerializeField] GameObject[] foodPrefabs;
    public int poolSize = 6;
    List<GameObject> objectPool;

    // Start is called before the first frame update
    void Start()
    {
        CreatObjectPooll();
    }

  
    private void CreatObjectPooll()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            var randomFood = Random.Range(0, 5);
            // Создаем новый экземпляр объекта из префаба
            GameObject obj = Instantiate(foodPrefabs[randomFood]);
            //присваиваю уникальное имя
            obj.name = foodPrefabs[randomFood].name + i;
            //устанавливаю в иерархию родителя
            obj.transform.SetParent(this.transform);
            //выкл
            obj.SetActive(false);
            // добавляем объект в список пула 
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for(int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);

                return objectPool[i];
            }
        }

        Debug.Log("foodPrefabs.Length" + foodPrefabs.Length);

        var randomFood = Random.Range(0, 5);
        // Если все объекты в пуле заняты, создаем новый экземпляр объекта и добавляем его в пул
        GameObject newFood = Instantiate(foodPrefabs[randomFood]);
        newFood.name = "new " + foodPrefabs[randomFood].name;
        newFood.transform.SetParent(this.transform);
        newFood.SetActive(false);
        objectPool.Add(newFood);

        return newFood;
    }

    public GameObject GrabFoodItem()
    {
        GameObject grabGo = null;

        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].GetComponent<FoodItem>().isTakeble)
            {
                grabGo = objectPool[i];

                return grabGo;
            }
        }

        return grabGo;
    }

    public void DesableFoodItem()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].GetComponent<FoodItem>().isInGarbage)
            {
                objectPool[i].SetActive(false);

                objectPool[i].GetComponent<BoxCollider>().enabled = true;

                //убираю из мусорки и из корзині
                objectPool[i].GetComponent<FoodItem>().isInGarbage = false;
                objectPool[i].GetComponent<FoodItem>().isInBasket = false;
             
                //меняю родителя
                objectPool[i].transform.SetParent(this.transform);
            }
        }
    }
}


