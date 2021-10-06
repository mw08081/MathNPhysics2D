using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintMatrix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Matrix4x4 myTransform = transform.localToWorldMatrix;

        Debug.Log("--ConversionInEditor--");
        for (int i = 0; i < 4; i++)
            Debug.Log(myTransform.GetRow(i));
            

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
