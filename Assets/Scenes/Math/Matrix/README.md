## MatrixConversion 구현 중에..
#### 1. C#에서 행렬을 연산해서 다시 tranform정보 대입하는 방법
기존의 해당 tranform정보(transform.localToWorldMatrix)를 대입하여 연산만 하여 출력했다  

그렇다면 이렇게 연산된 matrix 정보를 원래 object의 tranform정보로 대입할 수 없을까?  

방법은 다음과 같다(참고 https://forum.unity.com/threads/how-to-assign-matrix4x4-to-transform.121966/)  
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatrixExtensions                  //확장 메소드(matrix 4*4 class를 확장하다)
{
    public static Quaternion ExtractRotation(this Matrix4x4 matrix)
    {
        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;
        forward.z = matrix.m22;

        Vector3 upwards;
        upwards.x = matrix.m01;
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;

        return Quaternion.LookRotation(forward, upwards);
    }

    public static Vector3 ExtractPosition(this Matrix4x4 matrix)
    {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }

    public static Vector3 ExtractScale(this Matrix4x4 matrix)
    {
        Vector3 scale;
        scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
        scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
        scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
        return scale;
    }
}

public class MatrixConversion : MonoBehaviour
{
    ...

    void Update()
    {
        if(Input.anyKeyDown)
        {
            transform.localScale = directConversionMatrix.ExtractScale();
            transform.rotation = directConversionMatrix.ExtractRotation();
            transform.position = directConversionMatrix.ExtractPosition();
            
        }
    }

   
}
```
해서 anyKeyDown 한다면 originObject가 변환되면서 afterConversionObject와 동일한 위치로 가서 겹쳐지게 된다
![holy](https://user-images.githubusercontent.com/58582985/136199102-42517dc7-591d-426f-8460-796408ca1a6f.gif)

