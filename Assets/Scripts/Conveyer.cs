using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Conveyer : MonoBehaviour, IAccelerable
{
    Rigidbody rigidbody;
    public float conveyrSpeed;

    public static Conveyer instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        conveyrSpeed = 2f;
        rigidbody = GetComponent<Rigidbody>();
    }

    public void IncreaseSpeed()
    {
        conveyrSpeed += 0.01f;
    }

    void FixedUpdate()
    {
        //Move conveyer
        Vector3 newPosition = rigidbody.position;
        rigidbody.position += Vector3.forward * conveyrSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(newPosition);
    }
}
