using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSimulation : MonoBehaviour
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
        rb2D.velocity = rb2D.velocity * (1 - 0.5f * Time.deltaTime);
    }
}
