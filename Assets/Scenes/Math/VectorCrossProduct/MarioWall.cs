using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioWall : MonoBehaviour
{
    int hp;

    void Start()
    {
        hp = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 bVector = collision.collider.transform.position - transform.position;
        Vector3 CrossProduct = Vector3.Cross(bVector, transform.right);

        if (CrossProduct.z > 0)
        {
            hp--;

            if (hp == 0)
                gameObject.SetActive(false);
        }

    }
}
