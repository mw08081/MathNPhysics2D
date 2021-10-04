using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isClock;

    void Start()
    {
        isClock = false;    
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 dir = collision.collider.transform.position - transform.position;
        Vector3 crossVector = Vector3.Cross(dir, transform.right);
        
        float v = Player.Instance.v;
        float dt = Player.Instance.power;
        Player.Instance.power = 0;
        Player.Instance.isFlying = false;
        float a = v / dt;
        Vector3 r = (new Vector3(collision.collider.transform.position.x, transform.position.y, 0)) - transform.position;

        float torque = 1 * a * r.magnitude;

        Debug.Log("F * r = torque  : " + a + " * "  + r.magnitude + " = " + torque);
        if (crossVector.z > 0 && torque > 100)
        {
            isClock = false;
            StartCoroutine("RotateDoor", torque / 4);
        }
        else if (crossVector.z < 0 && torque > 100)
        {
            isClock = true;
            StartCoroutine("RotateDoor", torque / 4);
        }
    }


    IEnumerator RotateDoor(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            yield return new WaitForSeconds(0.012f); 
            
            if(!isClock)
                transform.Rotate(0f, 0f, 2.5f, Space.World);
            else
                transform.Rotate(0f, 0f, -2.5f, Space.World);
        }
            
    }
}

