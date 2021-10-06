using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForceDrag : MonoBehaviour
{
    GameObject[] simulators;

    void Start()
    {
        simulators = GameObject.FindGameObjectsWithTag("simulator");

    }

    
    void Update()
    {
        foreach(GameObject e in simulators)
        {
            Vector3 distanceVec = e.transform.position - transform.position;
            Debug.DrawRay(transform.position, -transform.right * 70.0f, Color.red);

            if(distanceVec.magnitude < 70.0f)
            {
                e.GetComponent<Rigidbody2D>().AddForce(-e.transform.right * 0.1f, ForceMode2D.Force);
            }
        }
    }
}
