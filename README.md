# MathNPhysics2D
프로젝트 설명 - 수학과 물리에 대한 2D프로젝트에서 공부를 실습하기 위한 프로젝트입니다  
기본 공부는 2D프로젝트를 통해 복습할 것이며, MathNPhysics에서 3D프로젝트로 심화 복습을 할 예정입니다

### 목차
1. 수학
- 삼각함수
- 벡터의 내적
- 벡터의 외적
- 행렬
2. 물리
- 힘과 운동(Force & motion)
- 마찰력(FrictionalForce)
- 저항력(ResistanceForce)
- 돌림힘(TorqueForce)
- 탄성력(ElasticeForce)

### 수학
#### 1. 삼각함수

#### 2. 벡터의 내적

#### 3. 벡터의 외적

#### 4. 행렬

### 물리
#### 1. 힘과 운동(Force & Motion)
AddForce 함수가 있다 addForce함수는 다음과 같다  
`Rigidbody2D.AddForce(힘 작용 방향, ForceMode2D.Impulse / ForceMode2D.Force);`  
forceMode란 힘을 가하는 방식에 대해 이야기 하려고한다  
`ForceMode.Force`는 짧은 시간에 발생하는 운동량 변화의 크기를 나타내며  
주로 바람이나 자기력처럼 연속적으로 주어지는 힘을 나타내는 데 이용 된다.  
`ForceMode.Impulse`는 충격량을 리지드바디에 주는 모드로 힘의 크기와 주는 시간을 곱한 수치다.  
주로 타격이나 폭팔처럼 순간적으로 힘을 나타내는 데 이용된다.  

> f = ma 라는 공식을 가지고 있으며 등가속도운동에서 다음과 같은 공식이 성립한다  
`v = v0 + at`, `v = v0 * t + at^2 / 2`, `2as = v^2 - v0^2`  
`at = Δv`라는 식을 통해 다음과 같이도 나타낼 수 있다. `v = v0 * t + Δv * t`  

> I 는 충격량을 나타내며, Impulse라고 한다  
`F * Δt = mv - mv0 = Δp = I`, `F = (mv - mv0) / Δt = Δp / Δt = I / Δt`

포물선 운동은 대표적인 등가속도 운동이다  
시작점을 A, 최고높이를 B, 다시 땅에 떨어진 지점을 C라고 하면 각 위치에서의 속도와 위치 좌표는 다음과 같다  

`A.x : Vx = v * cosΘ, Sx = 0`  
`Ay : Vy = v * sinΘ, Sy = 0`  
이때 곱해주는 v는 v0의 크기와 같다  
따라서 `rb2D.AddForce(tranform.right * 10, ForceMode.Inpuluse);`로 쏘아올릴 경우 단위 백터 * 10 한 크기이므로 v = 10 이된다

`B.x : Vx = v * cosΘ, Sx = cosΘ * t`  
`B.y : Vy = v * sinΘ - gt, Sy = v * sinΘ * t - gt^2 / 2` 

B지점에서 Vx의 속도가 동일한 이유는 x방향으로의 가해지는 힘이 없으므로 속도는 변하지 않는다(`v = v0 + at`)  
B지점에서 Vy, Sy를 구하는데 다음의 공식이 사용된다 ` v = v0 + at, s = v0 * t + at^2 / 2`  

`C.x : Vx = v * cosΘ, Sx = v * cosΘ * 2t`  
`C.y : Vy = v * sinΘ - 2gt = -v * sinΘ Sy = v * sinΘ * t - g2(t^2) / 2 = 0`  
C지점에서의 Vy, Sy의 식을 정리하는데는 다음의 정의가 들어간다. B지점에서 `Vy = v * sinΘ - gt = 0` 이므로 `t = v * sinΘ / g`을 대입하면 식이 정리된다
![포물선운동](https://user-images.githubusercontent.com/58582985/135850506-d8ec3189-e7b8-443a-9aba-85e9f422fb7e.jpg)

이동거리와 최대높이에 대한 식을 `t = v * sinΘ / g`통해 정리하면 다음과 같다  
`Sy = (v * sinΘ)^2 / 2g`  
`Sx = v^2 * sin2Θ / g`  
  
![parabola](https://user-images.githubusercontent.com/58582985/135963584-1a75307c-44e7-499d-9743-cca827fc2568.gif)
![캡처](https://user-images.githubusercontent.com/58582985/135963582-d281b70b-48d0-4809-8da4-3e33e3cd3737.PNG)

+++ 힘과 운동에서의 포물선 운동 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/ForceNMotion

  
#### 2. 마찰력(FrictionalForce)
마찰력은 경사면에서 물체가 내려갈 때에 영향을 준다 이때 영향을 미치는 힘은 다음과 같다  
![dd](https://user-images.githubusercontent.com/58582985/135465058-41df773b-7410-448b-8e5d-86e988766142.gif)  
물체가 움직이려면 내려가는 힘이 정지마찰력보다 커야하므로 `mg * sinθ > f = υ*N`이 성립해야한다  
따라서 다음의 공식이 만들어진다 `mg * sinθ > υ * mg * cosθ `  
  
유니티에서 mg * sinθ과 υ * mg * cosθ는 다음과 같이 나타낼 수 있다  
mass, gravity, angle, friction값이 필요하다 
```
void Start()
{
    rb2D = GetComponent<Rigidbody2D>();
    boxCollider2D = GetComponent<BoxCollider2D>();

    mass = rb2D.mass;                                       //질량(m)
    gravity = 9.87f * rb2D.gravityScale;                    //중력(g) - 기본값 9.87f에 rb2D.gravityScale을 곱해준다
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

playerObject의 PhysicsMaterial2D이나 collider의 PhysicsMaterial2D를 변경하려면 다음과 같이 할 수 있다
```
...
public PhysicsMaterial2D lowFriction;

boxCollider2D.sharedMaterial = lowFriction;               
//여기서 boxCollider2D는 boxCollider2D = GetComponent<BoxCollider2D>();를 의미한다
```  
+++ FrictionalForce 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/FrictionalForce  
  
#### 3. 저항력(ResistanceForce)
저항력이란, 물체가 운동할 때 주변 유체의 의해 단면적 등에 비례하여 작용하는 힘이다  
![캡처](https://user-images.githubusercontent.com/58582985/135581839-eeb68da4-44a7-4923-81ef-0323044b71bf.PNG)  
따라서 물체가 한 번 정해진 속도로 이동할때, 지속적으로 저항력이 가해진다면 결국 물체는 정지하며  
물체가 일정한 속도로 이동한다면, 지속적으로 저항력에 의해 속도가 감소하여 원하는 속도보다 못미치는 속도로 운동할 것이다  

유니티에서는 저항력을 단순하게 다음과 같이 나타낸다  
`ν = ν * (1 - D * Δt)`  
따라서 유니티에서 기본적으로 지원하는 drag속성값을 사용하지 않더라도  
물체의 velocity값에 유니티 유체저항력을 적용시켜주면 Rigidbody2D의 drag속성과 동일하게 작용할 것이다.  
![untiyDrag C#Drag](https://user-images.githubusercontent.com/58582985/135586069-42302e87-1def-4b60-aebd-06c7ba5e6649.gif)  
위의 case가 rigidbody.drag = 0.5가 적용되었고, 아래의 case에는 rigidbody.drag = 0으로 설정하고 다음과 속도를 변화시켜준다  
`rb2D.velocity = rb2D.velocity * (1 - 0.5f * Time.deltaTime);`  
  
추가적으로 문득 이런 생각이 들었다  
drag속성에 의해 속도가 줄어드는 것을 외부의 힘에 의해 addForce(운동방향과 반대방향)로도 표현할 수 있지 않을까?  
테스트 조건은 다음과 같다 `rigidbody.drag = 0;`  
`e.GetComponent<Rigidbody2D>().AddForce(-e.transform.right * 0.1f, ForceMode2D.Force);`  
이 때 e는 저항체이다  
![addForceDrag](https://user-images.githubusercontent.com/58582985/135586978-fc276b4a-b72e-4aab-b479-2233bfd6ad20.gif)  
ray의 범위에 들어오면 곧바로 외부의 힘이 가해지는데 따라서 해당 물체의 속도는 다음과 같이 변화한다  
![addForceDrag-velocity](https://user-images.githubusercontent.com/58582985/135587540-3185837d-f1dc-4a20-a777-4fdbb38eb13a.gif)  


+++ ResistanceForce 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/ResistanceForce


#### 4. 돌림힘(torqueForce)
돌림힘이란, 물체를 돌리는데 필요한 힘이며 관성모멘트 값, 질량 값에 따라 변화한다 다만 유니티에서는 해당 부분을 제외한다  
해당 힘은 다음과 같은 공식으로 구한다 `τ = r * F`  

유니티에서 해당 물체에 일정한 힘으로 회전운동을 가하는 방법은 다음과 같다
`rb2D.AddTorque(movePower, ForceMode2D.Impluse);`


게임에서 특정 물체를 돌리려면 일정 이상의 돌림힘일 때, 돌아가도록 구현을 할 수 있을 듯하다.  
하여 해당 물체는 돌림힘이 100 이상일 때 돌아갈 수 있도록 코드를 준비했다   
```  
if (crossVector.z > 0 && torque > 100)          //벡터의 외적을 통해 회전방향을 설정한다 시계/반시계
{
    isClock = false;                            //반시계 방향으로 회전한다
    StartCoroutine("RotateDoor", torque / 4);
}
else if (crossVector.z < 0 && torque > 100)
{
    isClock = true;                             //시계방향으로 회전한다 
    StartCoroutine("RotateDoor", torque / 4);
}
```  
이때 torque는 `float torque = 1 * a * r.magnitude;`로 계산하며 아래는 r에 따른 돌림힘 debug와 그 결과이다  
![torque(ture)](https://user-images.githubusercontent.com/58582985/135825184-0e87ec85-ecba-43ef-b0b8-04c0324f4141.PNG)  
![torque(true)](https://user-images.githubusercontent.com/58582985/135825179-dcdd4c2d-c7e7-4142-bd82-271f37f4fa7e.gif)  
![torque(false)](https://user-images.githubusercontent.com/58582985/135825175-8d246cc7-a85e-416e-8166-af91ce335a89.PNG)  
![torque(false)](https://user-images.githubusercontent.com/58582985/135825178-8408657a-d65e-4f5f-b16d-c5e89f1adf64.gif)  

 +++ torqueForce 구현 코드  
 https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/TorqueForce  
 +++ torqueForce 구현간의 난항  
 https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/TorqueForce#readme  

#### 5. 탄성력(ElasticForce)
탄성력이란 고체의 변형에 의하여 생기는 힘으로 처음의 상태로 되돌아가려는 성질 때문이다  
충돌 탄성에서는 3가지 경우에 따라 두 물체사이에서의 운동량의 차이가 생긴다  
다음은 위에서 부터 완전탄성/비탄성충돌/완전비탄성충돌에 대한 예시이다  
![collisionSimul](https://user-images.githubusercontent.com/58582985/135846516-bd6a46b6-9b30-43e7-a8c2-f3d1361e5995.gif)

운동량은 다음과 같다 `p = m * v`  
따라서 운동량은 m1 * v1 + m2 * v2 = m'1 * v'1 + m'2 * v'2  

+++ 탄성력 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/ElasticForce
