using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum pattern { uno, dos, tres, cuatro, cinco, seis}
public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject spawnPosition;

    float angle = 0;
    public pattern pat;
    bool isClock = false;

    public float intervalAngle;

    public float startAngle;
    public float endAngle;

    void Start()
    {
        StartCoroutine("attack");
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator attack()
    {
        if(pat == pattern.uno)
        {
            while (true)
            {
                Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                GameObject bullet = Instantiate(bulletPrefab);

                bullet.transform.position = spawnPosition.transform.position;
                bullet.transform.up = dir;

                transform.up = dir;

                angle += intervalAngle;
                if (angle > 360)
                    angle -= 360;

                yield return new WaitForSeconds(0.1f);
            }

        }
        else if(pat == pattern.dos)
        {
            while(true)
            {
                Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                GameObject bullet = Instantiate(bulletPrefab);

                bullet.transform.position = spawnPosition.transform.position;
                bullet.transform.up = dir;


                if (isClock)
                    angle -= 10;
                else
                    angle += 10;

                if (angle > 120)
                    isClock = true;
                else if (angle < 60)
                    isClock = false;

                yield return new WaitForSeconds(0.05f);
            }
            
        }
        else if(pat == pattern.tres)
        {
            while (true)
            {
                for(float angle = startAngle; angle < endAngle; angle += 5)
                {
                    Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                    GameObject bullet = Instantiate(bulletPrefab);

                    bullet.transform.position = spawnPosition.transform.position;
                    bullet.transform.up = dir;

                   
                }
                yield return new WaitForSeconds(2f);
            }

        }
        else if(pat == pattern.cuatro)
        {
            while(true)
            {
                for (float angle = 0; angle < 360; angle += 5)
                {
                    if (angle % 60 > 30)
                        continue;
                    Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                    GameObject bullet = Instantiate(bulletPrefab);

                    bullet.transform.position = spawnPosition.transform.position;
                    bullet.transform.up = dir;


                }
                yield return new WaitForSeconds(1f);
            }
        }
        else if(pat== pattern.cinco)
        {
            while(true)
            {
                for (int i = 0; i < 7; i++)
                {
                    float stAngle = 0;
                    for (angle = startAngle; angle < 360; angle += 45)
                    {
                        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                        GameObject bullet = Instantiate(bulletPrefab);

                        bullet.transform.position = spawnPosition.transform.position;
                        bullet.transform.up = dir;


                    }
                    yield return new WaitForSeconds(0.1f);
                    stAngle += 5;
                    if (stAngle > 360)
                        stAngle -= 360;

                }
                yield return new WaitForSeconds(2f);
                
            }
        }
        else if(pat == pattern.seis)
        {
            while (true)
            {
                float stAngle = 0;
                for (int i = 0; i < 7; i++)
                {
                    for (angle = stAngle; angle < 360; angle += 45)
                    {
                        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                        GameObject bullet = Instantiate(bulletPrefab);

                        bullet.transform.position = spawnPosition.transform.position;
                        bullet.transform.up = dir;


                    }
                    yield return new WaitForSeconds(0.1f);
                    stAngle += 5;
                    if (stAngle > 360)
                        stAngle -= 360;

                }
                yield return new WaitForSeconds(2f);

            }
        }
    }
}
