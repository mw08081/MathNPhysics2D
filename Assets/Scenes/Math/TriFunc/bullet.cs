using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
