using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] Transform handIKItem;
    [SerializeField] Transform handBone;
    [SerializeField] GameObject testObject;
    [SerializeField] Transform basket;
    [SerializeField] Transform middlFinger;
    [SerializeField] FoodPoolObject foodPoolObject;
    List<GameObject> foodInBasket = new List<GameObject>();
    GameObject selectedFood;
    Animator animator;
    bool isHeldingOutToFood;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (foodPoolObject.GrabFoodItem() != null)
        {
            selectedFood = foodPoolObject.GrabFoodItem();
        }

        if (Input.GetKeyDown(KeyCode.Space) &&
            selectedFood != null &&
            selectedFood.GetComponent<FoodItem>().isTakeble &&
            !selectedFood.GetComponent<FoodItem>().isInBasket)
            {
                handIKItem.position = selectedFood.transform.position;
                animator.SetTrigger("GrabFood");
            }

        if(isHeldingOutToFood && selectedFood != null &&
            !selectedFood.GetComponent<FoodItem>().isInBasket)
        {
            selectedFood.transform.position =
                new Vector3(
                    middlFinger.transform.position.x,
                    middlFinger.transform.position.y - 0.1f,
                    middlFinger.transform.position.z);
        }
    }

    public void OnGrab()
    { 
        isHeldingOutToFood = true;
        //выкл колайдер чтобы аккуратно вынести еду за пределы конвеера
        selectedFood.GetComponent<BoxCollider>().enabled = false;
    }

    public void InBasket()
    {
        
        isHeldingOutToFood = false;
        //делаю еду снова физичной
        selectedFood.GetComponent<BoxCollider>().enabled = true;
        selectedFood.GetComponent<Rigidbody>().useGravity = true;
        //укладываю в иерархию корзины
        selectedFood.transform.SetParent(basket, true);

        foodInBasket.Add(selectedFood);

        //предотвращаю повторное взаимодействие
        selectedFood.GetComponent<FoodItem>().FoodItemInBasket();

        //selectedFood.transform.position = selectedFood.transform.position;

        if(selectedFood.tag == "Bomb")
        {
            Debug.Log("Boom!");

            /*foreach(GameObject gameObjects in foodInBasket)
            {
                Debug.Log("gameObjects" + gameObjects);
                //gameObjects.GetComponent<Rigidbody>().AddExplosionForce(1000f, Vector3.up, 5f);
            }*/
            
        }
    }
}
