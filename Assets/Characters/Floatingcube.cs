using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatingcube : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private float zSpeed;

    [SerializeField] private float floatSpeed;

    [SerializeField] private float maxRange;

    [SerializeField] private float minRange;

    Vector3 distance;
    Vector3 origin;

    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y >= maxRange)
        {
            Debug.Log("bigger then");
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        }
        else if (transform.position.y <= minRange)
        {
            transform.Translate(Vector3.down * floatSpeed * Time.deltaTime);
        }

        transform.Rotate(transform.rotation.x + xSpeed, transform.rotation.y + ySpeed, transform.rotation.z + zSpeed);

    }
}
