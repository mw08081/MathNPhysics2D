# MathNPhysics2D
프로젝트 설명 - 수학과 물리에 대한 2D프로젝트에서 공부를 실습하기 위한 프로젝트입니다  
기본 공부는 2D프로젝트를 통해 복습할 것이며, MathNPhysics에서 3D프로젝트로 심화 복습을 할 예정입니다

### 목차
1. 수학
- 포물선운동
- 미정
2. 물리
- 힘과 운동(Force & motion)
- 마찰력(FrictionalForce)
- 저항력(ResistanceForce)


### 물리
- 힘과 운동
- 마찰력
> 마찰력은 경사면에서 물체가 내려갈 때에 영향을 준다 이때 영향을 미치는 힘은 다음과 같다  
![dd](https://user-images.githubusercontent.com/58582985/135465058-41df773b-7410-448b-8e5d-86e988766142.gif)  
물체가 움직이려면 내려가는 힘이 정지마찰력보다 커야하므로 `mg * sinθ > f = υ*N`이 성립해야한다  
따라서 다음의 공식이 만들어진다 `mg * sinθ > υ * mg * cosθ `  
  
  

> 유니티에서 mg * sinθ과 υ * mg * cosθ는 다음과 같이 나타낼 수 있다  
mass, gravity, angle, friction값이 필요하다 
```
void Start()
{
    rb2D = GetComponent<Rigidbody2D>();
    boxCollider2D = GetComponent<BoxCollider2D>();

    mass = rb2D.mass;                          //질량(m)
    gravity = 9.87f * rb2D.gravityScale;       //중력(g) - 기본값 9.87f에 rb2D.gravityScale을 곱해준다
}



private void OnCollisionEnter2D(Collision2D collision)
{
    friction = Mathf.Min(collision.collider.GetComponent<BoxCollider2D>().friction, boxCollider2D.friction);  
    //2D Friction은 두 오브젝트의 최솟값을 이용한다

    float tmp = collision.collider.transform.rotation.eulerAngles.z;                                                        
    if (!(tmp >= 0 && tmp <= 90))
        angle = 360 - tmp;
    else
        angle = tmp;
    //angle값은 0 < θ < 90이 아닐경우 debug값이 달라진다
}
```

> playerObject의 PhysicsMaterial2D이나 collider의 PhysicsMaterial2D를 변경하려면 다음과 같이 할 수 있다
```
...
public PhysicsMaterial2D lowFriction;

boxCollider2D.sharedMaterial = lowFriction;               
//여기서 boxCollider2D는 boxCollider2D = GetComponent<BoxCollider2D>();를 의미한다
```
  
- 저항력
> 저항력이란, 물체가 운동할 때 주변 유체의 의해 단면적 등에 비례하여 작용하는 힘이다  
![캡처](https://user-images.githubusercontent.com/58582985/135581839-eeb68da4-44a7-4923-81ef-0323044b71bf.PNG)  
따라서 물체가 한 번 정해진 속도로 이동할때, 지속적으로 저항력이 가해진다면 결국 물체는 정지하며  
물체가 일정한 속도로 이동한다면, 지속적으로 저항력에 의해 속도가 감소하여 원하는 속도보다 못미치는 속도로 운동할 것이다  

> 유니티에서는 저항력을 단순하게 다음과 같이 나타낸다  
`ν = ν * (1 - D * Δt)`  
따라서 유니티에서 기본적으로 지원하는 drag속성값을 사용하지 않더라도  
물체의 velocity값에 유니티 유체저항력을 적용시켜주면 Rigidbody2D의 drag속성과 동일하게 작용할 것이다.  
![untiyDrag C#Drag](https://user-images.githubusercontent.com/58582985/135586069-42302e87-1def-4b60-aebd-06c7ba5e6649.gif)  
위의 case가 rigidbody.drag = 0.5가 적용되었고, 아래의 case에는 rigidbody.drag = 0으로 설정하고 다음과 속도를 변화시켜준다  
`rb2D.velocity = rb2D.velocity * (1 - 0.5f * Time.deltaTime);`  
  
> 추가적으로 문득 이런 생각이 들었다  
drag속성에 의해 속도가 줄어드는 것을 외부의 힘에 의해 addForce(운동방향과 반대방향)로도 표현할 수 있지 않을까?  
테스트 조건은 다음과 같다 `rigidbody.drag = 0;`  
`e.GetComponent<Rigidbody2D>().AddForce(-e.transform.right * 0.1f, ForceMode2D.Force);`  
이 때 e는 저항체이다  
![addForceDrag](https://user-images.githubusercontent.com/58582985/135586978-fc276b4a-b72e-4aab-b479-2233bfd6ad20.gif)  
ray의 범위에 들어오면 곧바로 외부의 힘이 가해지는데 따라서 해당 물체의 속도는 다음과 같이 변화한다  
![addForceDrag-velocity](https://user-images.githubusercontent.com/58582985/135587540-3185837d-f1dc-4a20-a777-4fdbb38eb13a.gif)
