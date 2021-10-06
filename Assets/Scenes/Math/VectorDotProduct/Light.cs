using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    GameObject[] gosts;

    public float lightAngle;

    private void Awake()
    {
        gosts = GameObject.FindGameObjectsWithTag("gost");   
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //for (float i = 90 - lightAngle; i < (90 + lightAngle); i++)
        //    Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad), 0) * 5, Color.yellow);


        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += dir * 10f * Time.deltaTime;

        for(int i = 0; i < gosts.Length; i++)
        {

            Vector3 bVector = gosts[i].transform.position - transform.position;
            if (((gosts[i].transform.position - transform.position).magnitude <= 5) && Vector3.Dot(transform.up, bVector.normalized) > Mathf.Cos(lightAngle * Mathf.Deg2Rad))
                gosts[i].SetActive(true);
            else
                gosts[i].SetActive(false);
        }
    }
}
