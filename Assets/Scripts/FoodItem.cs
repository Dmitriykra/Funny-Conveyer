using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] public bool isTakeble = false;
    [SerializeField] public bool isInBasket = false;
    [SerializeField] GameObject foodPoolObjectGo;
    FoodPoolObject foodPoolObject = FoodPoolObject.instance;
    TaskForLevel taskForLevel = TaskForLevel.instance;
    public bool isInGarbage;
    bool correctFood;

    private void OnTriggerStay(Collider other)
    {
       if(other.tag == "TakeZone")
       {
            isTakeble = true;
       }

       //если взятый фрукт является фруктом по заданию
       if(gameObject.tag ==
            taskForLevel.targetFoodName)
       {
            correctFood = true;
       }    
       if(other.tag == "Garbage" || other.tag == "Ground" )
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
        if (other.tag == "TakeZone")
        {
            isTakeble = false;
        }
    }

    public void FoodItemInBasket()
    {
        //если не бомба, продолжаем игру
        if (gameObject.tag != "Bomb")
        {
            isInBasket = true;
            isTakeble = false;

            //нужный фрукт увеличивает скор
            if(correctFood)
            {
                taskForLevel.UpdateScore(1);
            }
        } 
    }    
}
