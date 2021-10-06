using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixConversion : MonoBehaviour
{
    

    void Start()
    {
        Vector3 tran = new Vector3(2, 1, 0);
        Quaternion rota = Quaternion.Euler(45, 0, 45);
        Vector3 scale = new Vector3(3, 3, 3);

        Matrix4x4 directConversionMatrix = transform.localToWorldMatrix;
        Matrix4x4 inDirectConversionMatrix = transform.localToWorldMatrix;

        directConversionMatrix = Matrix4x4.Translate(tran) * Matrix4x4.Rotate(rota) * Matrix4x4.Scale(scale);
        inDirectConversionMatrix = Matrix4x4.TRS(tran, rota, scale);

        Debug.Log("--directConversionMatrix--");
        for (int i = 0; i < 4; i++)
            Debug.Log(directConversionMatrix.GetRow(i));

        Debug.Log("--inDirectConversionMatrix--");
        for (int i = 0; i < 4; i++)
            Debug.Log(inDirectConversionMatrix.GetRow(i));

    }


    void Update()
    {
        
    }
}
