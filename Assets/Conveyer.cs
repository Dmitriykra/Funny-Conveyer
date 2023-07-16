using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    Rigidbody rigidbody;
    float conveyrSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = rigidbody.position;
        rigidbody.position += Vector3.forward * conveyrSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(newPosition);
    }
}
