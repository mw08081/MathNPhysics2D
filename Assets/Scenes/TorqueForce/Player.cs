using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance = null;
    
    Rigidbody2D rb2D;
    Vector3 stPosition;

    public float v;
    public float power;
    public bool isFlying = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        stPosition = transform.position;
        v = 1.1f;

        power = 0;
    }


    void Update()
    {
        if (rb2D.velocity.y != 0)
            v = rb2D.velocity.magnitude;

        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += dir * 5f * Time.deltaTime;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            power += Time.deltaTime * 10;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rb2D.AddForce(transform.up * power, ForceMode2D.Impulse);
            isFlying = true;
            power = 0;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            rb2D.AddForce(-transform.up * power, ForceMode2D.Impulse);
            isFlying = true;
            power = 0;
        }

        if (isFlying)
            power += Time.deltaTime;
            

            


    }
}
