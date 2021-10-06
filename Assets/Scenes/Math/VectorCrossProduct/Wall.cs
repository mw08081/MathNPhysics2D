using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 bVector = collision.collider.transform.position - transform.position;
        Vector3 CrossProduct = Vector3.Cross(bVector, transform.right);

        if (CrossProduct.z > 0)
            Debug.Log("Down");
        else
            Debug.Log("Up");
    }
}
