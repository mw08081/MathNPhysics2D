using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justGoRight : MonoBehaviour
{
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.velocity = transform.right * 200f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
