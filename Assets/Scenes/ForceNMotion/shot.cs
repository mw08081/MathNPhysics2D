using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    Rigidbody2D rb2D;

    bool isFlyinng;
    bool readyLending = false;

    float g = 9.87f;

    public float shotPower;
    float angle;

    float sx;
    float sy;
    float t;

    float bestHight;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        isFlyinng = false;
        t = 0;
        bestHight = 0f;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.right * 3f, Color.red);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            transform.Rotate(0f, 0f, Input.GetAxisRaw("Vertical") * 5f, Space.Self);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(transform.right * shotPower, ForceMode2D.Impulse);
            angle = transform.rotation.eulerAngles.z;
            isFlyinng = true;
            readyLending = true;

            float estimatedTime = 2 * shotPower * Mathf.Sin(angle * Mathf.Deg2Rad) / g;
            float estimatedDistance = Mathf.Pow(shotPower, 2) * Mathf.Sin(2 * angle * Mathf.Deg2Rad) / g;
            float estimatedBestHight = Mathf.Pow(shotPower * Mathf.Sin(angle * Mathf.Deg2Rad), 2) / (2 * g);
            Debug.Log("Estimated time : " + estimatedTime);
            Debug.Log("Estimated Distance : " + estimatedDistance);
            Debug.Log("Estimated BestHight : " + estimatedBestHight);
        }
            
        if(isFlyinng)
            t += Time.deltaTime;

        

        sy = shotPower * Mathf.Sin(angle * Mathf.Deg2Rad) * t - g * Mathf.Pow(t, 2) / 2;

        if (sy > bestHight)
            bestHight = sy;
            

        if (rb2D.velocity.y == 0 && readyLending)
        {
            isFlyinng = false;
            rb2D.velocity = Vector2.zero;

            Debug.Log("time : " + t);
            Debug.Log("distance : " + transform.position.x);
            Debug.Log("Best hight : " + bestHight);

        }
            
        
    }
}
