## Torque Force 실습 중에...
#### 1. a를 계산하는 방법
`τ = F * r`에서, `F = m * a`에 해당하는 a를 구할 방법을 몰랐다  
처음에 2as = v^2 * v0^2를 통해 계산하려고 했으나 해당 공식은 등가속도 운동에서의 공식임을 떠올리고 다른 방식으로 접근하기로 했다  
따라서 `a = Δv / Δt`를 사용하여 a를 계산했다.
#### 2. collision.collider의 velocity가 0가 나오는 것을 해결하는 방법
v0 = 0 이니 v값만 구하면 되어 v를 다음과 같이 계산하였다  
```C#
private void OnCollisionEnter2D(Collision2D collision)
{
    ...
    float v = collision.collider.GetComponent<Rigidbody2D>().velocity.magnitude;
    ...
}
```  
하지만 이는 이미 collider가 발생한 후의 velocity이므로 v = 0이였다  
해서 다음과 같이 수정하였다  
```C#
//Player.cs
public class Player : MonoBehaviour
{
    public static Player Instance = null;
    
    Rigidbody2D rb2D;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    ...
}
...
//Player() - Update()
void Update()
{
    ...
    if (rb2D.velocity.y != 0)
         v = rb2D.velocity.magnitude;
    ...
}
```
```C#
//Door.cs OnCollisionEnter2D(Collision2D collision)
private void OnCollisionEnter2D(Collision2D collision)
{
    ...
    float v = Player.Instance.v;        //Player객채의 싱글턴 구현
    ...
}
```
Player 측에서 rb2D.velocity.y가 0이 아닐 때, 자신의 속도를 `public float v`에 넣어두고, door는 해당 v를 싱글턴으로 접근한 것이다  

  #### 3. 문을 돌릴 방향을 결정하는 방법
  벡터의 외적을 통해 문과 Player의 위치관계를 파악하여 `transform.rotation.eulerAngles.z` 값을 변경해준다  
  
  ```C#
  private void OnCollisionEnter2D(Collision2D collision)
{
    Vector3 dir = collision.collider.transform.position - transform.position;
    Vector3 crossVector = Vector3.Cross(dir, transform.right);

    if (crossVector.z > 0 && torque > 100)        //반시계방향 회전
    {
        isClock = false;
        StartCoroutine("RotateDoor", torque / 4);           //2.5f씩 torque / 4번(torqueForce가 100일때, 50도 정도 회전한다는 계산)
    }
    else if (crossVector.z < 0 && torque > 100)   //시계 방향 회전
    {
        isClock = true;
        StartCoroutine("RotateDoor", torque / 4);  
    }
}
  ```
  #### 4. 문을 돌리는 효과 연출하는 방법
  단순하게 `transform.Rotate(0f, 0f, 2.5f * torque, Space.Self);`를 통해 돌린다면  
  문이 돌아가는 회전 연출을 할 수 없어 밋밋한 연출이 나온다  
  따라서 첫번째로 아래와 같은 방법을 떠올렸다  
  ```C#
  if (crossVector.z > 0 && torque > 100)
{
    for (int i = 0; i < torque / 4; i++)
    {
        transform.Rotate(0f, 0f, 2.5f, Space.World);          //2.5f씩 torque / 4번(torqueForce가 100일때, 50도 정도 회전한다는 계산)
    }
}
  ```
  하지만 해당 연출도 너무 빠른 진행속도로 회전 연출이 보이지 않았고, 2.5f 회전간의 delay가 필요하다는 생각이 들었다  
  그래서 코루틴 함수를 생각해냈다  
  
  ```C#
  IEnumerator RotateDoor(int cnt)
{
    for (int i = 0; i < cnt; i++)
    {
        yield return new WaitForSeconds(0.012f);

        if (!isClock)
            transform.Rotate(0f, 0f, 2.5f, Space.World);
        else
            transform.Rotate(0f, 0f, -2.5f, Space.World);
    }

}
  ```
  결과적으로 2.5f회전간 0.012f의 공백이 매끄러운 회전을 연출했다
  
![torque(true)](https://user-images.githubusercontent.com/58582985/135825179-dcdd4c2d-c7e7-4142-bd82-271f37f4fa7e.gif)
