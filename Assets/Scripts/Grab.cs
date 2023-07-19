using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] Transform handIKItem;
    [SerializeField] Transform handBone;
    [SerializeField] GameObject foodPoolGameObject;
    [SerializeField] Transform basket;
    [SerializeField] Transform middlFinger;
    [SerializeField] FoodPoolObject foodPoolObject;
    List<GameObject> foodInBasket = new List<GameObject>();
    GameObject selectedFood;
    Animator animator;
    bool isHeldingOutToFood;
    bool isInHand;
    bool isMobile;
    public float rotationDuration = 1f;
    Vector3 currentBoxColliderSize;
    BoxCollider boxCollider;
    Vector3 currentFoodScale;
    Vector3 newFoodScale;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isMobile = Application.isMobilePlatform;
    }

    void Update()
    {

        InitSelectedFood();

        //запуск на пк через пробел
        if (!isMobile)
        {
            if (Input.GetKeyDown(KeyCode.Space) &&
            selectedFood != null &&
            selectedFood.GetComponent<FoodItem>().isTakeble &&
            !selectedFood.GetComponent<FoodItem>().isInBasket &&
            !isInHand)
            {
                Debug.Log("PC");

                StartHandAnimation();
            }            
        }

        //запуск на мобилке
        else
        {
            if (Input.touchCount > 0 &&
            selectedFood != null &&
            selectedFood.GetComponent<FoodItem>().isTakeble &&
            !selectedFood.GetComponent<FoodItem>().isInBasket &&
            !isInHand)
            {
                Debug.Log("Mob");
                StartHandAnimation();
            }
        }


        TakeAction();
    }

    void InitSelectedFood()
    {
        if (foodPoolObject.GrabFoodItem() != null)
        {
            selectedFood = foodPoolObject.GrabFoodItem();
            boxCollider = selectedFood.GetComponent<BoxCollider>();
            currentFoodScale = selectedFood.transform.localScale;
        }
    }

    void TakeAction()
    {
        if (isHeldingOutToFood && selectedFood != null &&
                !selectedFood.GetComponent<FoodItem>().isInBasket)
        {
            selectedFood.transform.position =
                new Vector3(
                    middlFinger.transform.position.x,
                    middlFinger.transform.position.y - 0.1f,
                    middlFinger.transform.position.z);
        }
    }

    void StartHandAnimation()
    {
        isInHand = true;
        handIKItem.position = selectedFood.transform.position;
        animator.SetTrigger("GrabFood");
    }

    public void OnGrab()
    { 
        isHeldingOutToFood = true;
        
        currentBoxColliderSize = boxCollider.size;

        Debug.Log("был размер бк до " + currentBoxColliderSize);

        Vector3 newSize = currentBoxColliderSize * 0.1f;

        boxCollider.size = newSize;

        Debug.Log("Уменьшил размер бк до " + newSize);
    }

    public void InBasket()
    {
        //тянется ли рука к еде
        isHeldingOutToFood = false;

        //возвращаю исходный размер бк
        boxCollider.size = currentBoxColliderSize;

        //уменьшаю размер фруктов чтобы больше поместилось
        newFoodScale = currentFoodScale * 0.7f;
        selectedFood.transform.localScale = newFoodScale;

        selectedFood.GetComponent<Rigidbody>().useGravity = true;

        //укладываю в иерархию корзины
        selectedFood.transform.SetParent(basket, true);

        foodInBasket.Add(selectedFood);

        //предотвращаю повторное взаимодействие
        selectedFood.GetComponent<FoodItem>().FoodItemInBasket();

        if(selectedFood.tag == "Bomb")
        {
            Debug.Log("Boom!, food in basket " + foodInBasket.Count);
            foreach(GameObject gameObjects in foodInBasket)
            {
                //убираю из корзины, на случай если при попадании бомбы будет не GameOver
                gameObjects.GetComponent<FoodItem>().isInBasket = false;
                gameObjects.transform.SetParent(foodPoolGameObject.transform, true);

                //Добавляю взрыв
                gameObjects.GetComponent<Rigidbody>().AddForce(Vector3.up * 300f);
                
            }
            //чистим список в корзине
            foodInBasket.Clear();

            selectedFood.GetComponent<BombItem>().BombBang();
        }

        isInHand = false;
    }

    public void VictoryDance()
    {
        //выключаем RigBuilder
        gameObject.GetComponent<UnityEngine.Animations.Rigging.RigBuilder>().enabled = false;

        //бросаем корзину
        basket.SetParent(null);

        StartCoroutine(RotateNpcToPlayer());

        //запуск победного танца
        animator.SetBool("win", true);
    }

    IEnumerator RotateNpcToPlayer()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, -90f, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

    }
}
