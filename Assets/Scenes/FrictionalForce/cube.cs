using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider2D;
    public PhysicsMaterial2D lowFriction;

    float mass;
    float gravity;
    float friction;
    float angle;

    bool isItem = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        mass = rb2D.mass;
        gravity = 9.87f;
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
            rb2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "item")
        {
            boxCollider2D.sharedMaterial = lowFriction;
        }
        else
        {
            friction = Mathf.Min(collision.collider.GetComponent<BoxCollider2D>().friction, boxCollider2D.friction);
            
            float tmp = collision.collider.transform.rotation.eulerAngles.z;
            if (!(tmp >= 0 && tmp <= 90))
                angle = 360 - tmp;
            else
                angle = tmp;

            float pushForce = mass * gravity * Mathf.Sin(angle * Mathf.Deg2Rad);
            float frictionalForce = friction * mass * gravity * Mathf.Cos(angle * Mathf.Deg2Rad);

            Debug.Log("pushForce : " + pushForce + ", FrictionalForce : " + frictionalForce);
        }


        

    }
}
