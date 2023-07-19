using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    [SerializeField] GameObject boomEffect;
    [SerializeField] GameObject sparksEffect;
 
    public void BombBang()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

        sparksEffect.SetActive(false);

        Instantiate(boomEffect, gameObject.transform.position, Quaternion.identity);       
    }
}
