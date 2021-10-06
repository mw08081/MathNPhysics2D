using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    Rigidbody2D rb2D;

    public bool isJump = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"),0, 0);
        transform.position += dir * 10f * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            rb2D.AddForce(transform.up * 15f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
            isJump = false;
    }
}
