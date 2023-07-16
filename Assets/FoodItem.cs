using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] public bool isTakeble = false;
    [SerializeField] public bool isInBasket = false;
    [SerializeField] FoodPoolObject foodPoolObject; 
    public bool isInGarbage;
    [SerializeField] GameState gameState;
        

    private void Awake()
    {
        foodPoolObject = FindObjectOfType<FoodPoolObject>();
        gameState = FindObjectOfType<GameState>();
    }

    private void OnTriggerStay(Collider other)
    {
       if(other.tag == "TakeZone")
       {
            isTakeble = true;
       }

       if(other.tag == "Garbage" || other.tag == "Ground" )
       {
            isInGarbage = true;
            foodPoolObject.DesableFoodItem();

            if(gameObject.tag != "Bomb")
            {
                gameState.IncreaseScore(-1);
            }
            
        }

        if (other.tag == "Conveyer")
        {
            isInBasket = false;
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
        isInBasket = true;
        isTakeble = false;
        gameState.IncreaseScore(1);
    } 
}
