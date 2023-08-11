using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPoolObject : MonoBehaviour
{
    [SerializeField] GameObject[] foodPrefabs;
    public int poolSize = 50;
    List<GameObject> objectPool ;
    public static FoodPoolObject instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        CreatObjectPooll();
    }

    private void CreatObjectPooll()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            var randomFood = Random.Range(0, foodPrefabs.Length);
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
        // Перемешиваю порядок объектов в пуле случайным образом
        ShuffleObjectPool();

        // Проходимся по объектам в пуле и выбираем первый неактивный объект
        if (objectPool != null)
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    objectPool[i].SetActive(true);
                    return objectPool[i];
                }
            }
        }
        

        // Если все объекты в пуле заняты, создаем новый экземпляр объекта и добавляем его в пул
        var randomFood = Random.Range(0, foodPrefabs.Length);
        GameObject newFood = Instantiate(foodPrefabs[randomFood]);
        newFood.name = "new " + foodPrefabs[randomFood].name;
        newFood.transform.SetParent(this.transform);
        objectPool.Add(newFood);
        newFood.SetActive(true);

        return newFood;
    }

    private void ShuffleObjectPool()
    {
        // Алгоритм перемешивания "тасования Фишера–Йетса"
        if (objectPool != null)
        {
            int n = objectPool.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                GameObject value = objectPool[k];
                objectPool[k] = objectPool[n];
                objectPool[n] = value;
            }
        }
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

    //вернуть обект в пул
    public void ReturnInPool()
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