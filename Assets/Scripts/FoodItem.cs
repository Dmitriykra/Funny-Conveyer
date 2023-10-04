using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] public bool isTakeble = false;
    [SerializeField] public bool isInBasket = false;
    FoodPoolObject foodPoolObject = FoodPoolObject.instance; 
    Conveyer conveyer;
    TaskForLevel taskForLevel = TaskForLevel.instance;
    public bool isInGarbage;
    bool correctFood;
    private IAccelerable accelerable, decreaseTime;
    private FoodSpawner _foodSpawner;
    
    Conveyer _conveyer;

    private void Start()
    {
        if (_foodSpawner != null)
        {
            _foodSpawner = FindObjectOfType<FoodSpawner>();
            decreaseTime = _foodSpawner.GetComponent<FoodSpawner>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
       if(other.CompareTag("TakeZone"))
       {
            isTakeble = true;
       }

       //если взятый фрукт является фруктом по заданию
       if(gameObject.CompareTag(taskForLevel.targetFoodName))
       {
            correctFood = true;
       }    
       if(other.CompareTag("Garbage") || other.CompareTag("Ground") )
       {
            isInGarbage = true;
            foodPoolObject.ReturnInPool();

            //если упущен нужный фрукт, отнимаем очки
            if (correctFood)
            {
                taskForLevel.UpdateScore(-1);
            }
       }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TakeZone"))
        {
            isTakeble = false;
        }
    }

    public void CandyInBasket()
    {
        //если не бомба, продолжаем игру
        if (!gameObject.CompareTag("Bomb"))
        {
            isInBasket = true;
            isTakeble = false;

            //нужный фрукт увеличивает скор
            if(correctFood)
            {
                taskForLevel.UpdateScore(1);
            }
            //если нет, ускоряем конвеер
            else
            {
                if (Conveyer.instance != null)
                {
                    Conveyer.instance.IncreaseSpeed();
                }
            
                if (accelerable != null)
                {
                    accelerable.IncreaseSpeed();
                }

                if (decreaseTime != null)
                {
                    decreaseTime.IncreaseSpeed();
                }
            }
        } 
    }    
}
