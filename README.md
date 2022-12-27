# MathNPhysics2D
프로젝트 설명 - 수학과 물리에 대한 2D프로젝트에서 공부를 실습하기 위한 프로젝트입니다  
기본 공부는 2D프로젝트를 통해 복습할 것이며, MathNPhysics에서 3D프로젝트로 심화 복습을 할 예정입니다  
+++ 21.09.부터 패스트 캠퍼스 강의를 토대로 정리

### 목차
1. 수학
    - [삼각함수](https://github.com/mw08081/MathNPhysics2D#1-%EC%82%BC%EA%B0%81%ED%95%A8%EC%88%98)
    - [벡터의 내적](https://github.com/mw08081/MathNPhysics2D#2-%EB%B2%A1%ED%84%B0%EC%9D%98-%EB%82%B4%EC%A0%81)
    - [벡터의 외적](https://github.com/mw08081/MathNPhysics2D#3-%EB%B2%A1%ED%84%B0%EC%9D%98-%EC%99%B8%EC%A0%81)
    - [행렬](https://github.com/mw08081/MathNPhysics2D#4-%ED%96%89%EB%A0%AC)
2. 물리
    - [힘과 운동(Force & motion)](https://github.com/mw08081/MathNPhysics2D#1-%ED%9E%98%EA%B3%BC-%EC%9A%B4%EB%8F%99force--motion)
    - [마찰력(FrictionalForce)](https://github.com/mw08081/MathNPhysics2D#2-%EB%A7%88%EC%B0%B0%EB%A0%A5frictionalforce)
    - [저항력(ResistanceForce)](https://github.com/mw08081/MathNPhysics2D#3-%EC%A0%80%ED%95%AD%EB%A0%A5resistanceforce)
    - [돌림힘(TorqueForce)](https://github.com/mw08081/MathNPhysics2D#4-%EB%8F%8C%EB%A6%BC%ED%9E%98torqueforce)
    - [탄성력(ElasticeForce)](https://github.com/mw08081/MathNPhysics2D#5-%ED%83%84%EC%84%B1%EB%A0%A5elasticforce)

### 수학
#### 1. 삼각함수  
삼각함수는 주기를 그리는 필수적인 요소이다 게임에서의 원형 등과 같은 패턴을 그려낼때도 자주 사용된다  
![Sine_function001 svg](https://user-images.githubusercontent.com/58582985/136132474-6ae0a1f8-d400-42fe-86a5-ef01dbae0ccb.png)  

삼각함수를 계산하는 방법은 다음과 같다  
![M202104-136](https://user-images.githubusercontent.com/58582985/136132710-2aadb01e-ab3d-42c6-9130-770f61e130ce.png)  
이 때, 빗변의 길이를 1로 계산하면 `sinΘ = a`,`cosΘ = b`가 되고  
피타고라스의 정리에 의해 `a^2 + b^2 = sinΘ^2 + cosΘ^2 = 1^2`로도 정리된다

![1404CC334DFE1F6F36](https://user-images.githubusercontent.com/58582985/136132480-cab1ebde-98f4-4ddf-842b-8a563b5ac79f.png)  
이 c = 1인 상황을 이용해서, (x = cosΘ, y = sinΘ) 좌표로 원을 그릴 수 있다.  

삼각함수를 이용해서 패턴을 그려낼 수 도있다.  
![turret](https://user-images.githubusercontent.com/58582985/136133859-473a3ba9-210d-4807-96f6-6112831efe7c.gif)

+++ 삼각함수 패턴 그리기 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Math/TriFunc  
+++ 삼각함수 각도에 따른 항등식  
https://ko.wikipedia.org/wiki/%EC%82%BC%EA%B0%81%ED%95%A8%EC%88%98_%ED%95%AD%EB%93%B1%EC%8B%9D  

#### 2. 벡터의 내적
벡터의 내적은 다음과 같이 계산한다  
`a · b = a1*b1 + a2*b2 = |a||b| * cosΘ (단, a = (a1, a2), b = (b1, b2))`  
이때 벡터의 내적을 단위벡터를 통해 계산하면 |a|, |b|는 1이 된다  

벡터의 내적은 이렇게 활용할 수 있다  
![제목 없음](https://user-images.githubusercontent.com/58582985/136145884-5940d661-c200-44dc-8537-5fa7afa9d747.png)  
위 그림을 보면 Θ = 90이고 c,d벡터가 같은 평면에 있다고 가정했을 때, d벡터가 c벡터를 기준으로 90도 안에 있는지 확인하는데 활용할 수 있다  
`Vector3.Dot(c, d)`는 단위벡터로 계산하면 cosβ가 된다  
이때 cosβ가 cosΘ(= cos90)보다 크다면 d벡터가 c벡터를 기준으로 90도 안에 있게 된다  

원리는 cosβ가 cosΘ보다 크다면 Θ보다 β가 작은 각도가 된다 (단 0 <= Θ <= 90, 0 <= β <= 90) + cos그래프 참고  

벡터의 내적을 통해 다음과 같이 손전등으로 유령을 찾는 코드를 구현할 수 있다  
이렇게 Gost가 있다고 했을 때, Gost는 미리 `setActive(false)`한다  
![캡처](https://user-images.githubusercontent.com/58582985/136146533-43eaaf06-dbf3-40fb-8616-f9a2e74cf494.PNG)  

기본적으로 손전등과 Gost의 거리가 5이하이고, 손전등에서의 60도 사이에 Gost가 있는지 내적을 통해 확인한다
```C#
for (int i = 0; i < gosts.Length; i++)
{
    Vector3 bVector = gosts[i].transform.position - transform.position;
    if (((gosts[i].transform.position - transform.position).magnitude <= 5) && 
        Vector3.Dot(transform.up, bVector.normalized) > Mathf.Cos(lightAngle * Mathf.Deg2Rad))
        gosts[i].SetActive(true);
    else
        gosts[i].SetActive(false);
}
```
![noDenugRay](https://user-images.githubusercontent.com/58582985/136146529-b0668063-1222-49e8-9884-22779c8b8791.gif)  

다음코드를 통해 벡터 내적의 범위를 아래코드를 통해 그려볼 수도 있다  
```
for (float i = 90 - lightAngle; i < (90 + lightAngle); i++)
    Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad), 0) * 5, Color.yellow);
```
![drawDebug](https://user-images.githubusercontent.com/58582985/136148963-113c01aa-3134-4b81-835f-58dcc6439194.gif)  

2 - 1) 벡터의 내적을 통한 두 벡터사잇각  
　  
두 벡터가 이루는 사이각을 구하는 방법은 벡터의 내적을 활용하는 것이다   
두 벡터의 성분값을 알면 사잇각을 계산할 수 있다  
  
두 벡터의 내적은 `a · b = a1*b1 + a2*b2 = |a||b| * cosΘ` 이므로  
a = (1, 0), b = (0.5, -0.5) 의 내적을 계산하면 0.5이다   
  
따라서 a, b벡터가 단위벡터이면, |a|, |b|는 1이고 cosΘ = 0.5, Θ = 𝝿 / 4(45°)  
  
이를 C#에서는 다음과 같이 표현할 수 있다.
```C#
Vector3 a = new Vector3(1, 0, 0);
Vector3 b = new Vector3(0.5f, 0, -0.5f);

float betweenAngle = Mathf.Acos(Vector3.Dot(a, b)) * Mathf.RadToDeg;
```
사잇각(betweenAngle)은 내적결과를 Acos함수를 통해 각도를 얻을 수 있다  
이때 반드시 Acos값에 Mathf.RadToDeg값을 곱해줘야지 라디안 각도를 얻을 수 있다  

다음은 Acos에 대한 C# Docs이다  
<img width="695" alt="스크린샷 2021-12-04 오후 4 17 49" src="https://user-images.githubusercontent.com/58582985/144701396-3b9d3f10-2c72-4916-a8b7-1e7c50ccda5c.png">  
　  
+++ 벡터의 내적 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Math/VectorDotProduct  
+++ 벡터의 내적 구현 난항  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Math/VectorDotProduct  

#### 3. 벡터의 외적
벡터의 외적은 다음과 같이 계산한다  
![캡처](https://user-images.githubusercontent.com/58582985/136176206-6f3a8aa2-6c13-482e-90fe-e811e3c52e5d.PNG)  

벡터의 외적을 통해서 두 벡터에 수직인 벡터를 구할 수 있다  
이때 수직인 벡터는 a x b 일때, 오른손으로 a벡터에서 b벡터쪽으로 감쌀때 엄지손가락이 향하는 방향이라고 한다  

2d에서 벡터의 외적을 통해 z값을 통해 두 오브젝트간의 위치관계를 알 수 있다  
![제목 없음](https://user-images.githubusercontent.com/58582985/136177873-82e83c52-7433-4f65-a5ee-8c758b605d09.png)  
반대로 Player가 위에 있게된다면 z 값이 음수값이 되는 것이다  

![CrossProduct](https://user-images.githubusercontent.com/58582985/136171333-5f3f8ce7-4ef1-49cf-8373-b1f283d0e7a8.gif)  

+++ 벡터의 외적 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Math/VectorCrossProduct  

#### 4. 행렬
행렬은 유니티에서의 Transform 정보를 행렬로 나타낼 수 있고 해당 하는 행렬의 이동, 회전, 크기에 대해서 쉽게 계산할 수 있다  

행렬의 변환은 다음 사진을 참고해서 설명한다  
![변환행렬의 곱](https://user-images.githubusercontent.com/58582985/136191729-c1f8a3b3-7f1b-406d-b7f6-96f9f824e16f.png)  
이동할때에는 T행렬을 기존 동차좌표계에 곱해주고, 신축할때에는 S행렬을 기존 동차좌표계에 곱해준다  
마지막으로 회전을 할때에는 각각의 축에 맞게 Θ값을 대입해서 곱해주면 된다  
다음은 3차원 좌표에 점 a(1, 0, 0)가 있을때 이 점을 z축을 기준으로 90도 회전한다면 해당 점의 위치가 어떻게 바뀌는지에 대한 예시이다  
![z회전](https://user-images.githubusercontent.com/58582985/136193747-482deb8e-1979-4ff8-916e-a003f68595f7.png)

행렬 변환을 하기위해 행렬을 곱하는 순서가 있다`TR(y -> x -> z)S`순서이다  
따라서 RST순으로 곱하게 된다면 잘못된 값이 나오니 주의하자  

그렇다면 유니티에서도 실제로 동일한 값이 나올지 연산을 통해 transform정보를 추출해보고, 직접 이동/회전/신축을 통한 object의 transform 정보도 보도록하자  

다음의 tranform 정보를 가지고 있는 Object를  
x축으로 2, y축으로 1만큼 이동하고  
x축을 기준으로 45도, z축을 기준으로 45도 회전하고  
x,y,z에 대해서 3배 신축해주려고 한다  
![origin](https://user-images.githubusercontent.com/58582985/136195254-e3576946-af85-46a2-84f5-0b236b54d460.PNG)  
```C#
Matrix4x4 directConversionMatrix = transform.localToWorldMatrix;

directConversionMatrix = Matrix4x4.Translate(tran) * Matrix4x4.Rotate(rota) * Matrix4x4.Scale(scale);
// 또는 다음과 같이 연산해줄 수 있다
directConversionMatrix = Matrix4x4.TRS(tran, rota, scale);
```

Conversion을 연산한 결과와 에디터에서 직접 이동/회전/신축한 object의 행렬값은 다음과 같았다  
![afterConversion](https://user-images.githubusercontent.com/58582985/136195112-11ea38f7-5df5-41e0-aa4c-d43c59b9fa3a.PNG)
![캡처2](https://user-images.githubusercontent.com/58582985/136195114-7baef641-222f-43c0-abd1-06b72919296b.PNG)
![캡처](https://user-images.githubusercontent.com/58582985/136195243-0b0908a7-2f38-4af0-a62e-a76ba021f241.PNG)  

+++ 행렬 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Math/Matrix  
+++ 계산된 행렬을 추출하여 scale, rotation, posion으로 assing 하는 방법(★★★★★)    
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Math/Matrix#readme  

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
`C.y : Vy = v * sinΘ - 2gt = -v * sinΘ, Sy = v * sinΘ * t - g(t^2) / 2 = 0`  
C지점에서의 Vy, Sy의 식을 정리하는데는 다음의 정의가 들어간다. B지점에서 `Vy = v * sinΘ - gt = 0` 이므로 `t = v * sinΘ / g`을 대입하면 식이 정리된다  
![포물선운동](https://user-images.githubusercontent.com/58582985/135850506-d8ec3189-e7b8-443a-9aba-85e9f422fb7e.jpg)

이동거리와 최대높이에 대한 식을 `t = v * sinΘ / g`통해 정리하면 다음과 같다  
`B.Sy = (v * sinΘ)^2 / 2g`  
`C.Sx = v^2 * sin2Θ / g`   (배각공식 : 2sinΘcosΘ = sin2Θ)  
  
![parabola](https://user-images.githubusercontent.com/58582985/135963584-1a75307c-44e7-499d-9743-cca827fc2568.gif)
![캡처](https://user-images.githubusercontent.com/58582985/135963582-d281b70b-48d0-4809-8da4-3e33e3cd3737.PNG)

+++ 힘과 운동에서의 포물선 운동 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Physics/ForceNMotion  

  
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
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Physics/FrictionalForce  
  
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
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Physics/ResistanceForce  


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
 https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Physics/TorqueForce  
 +++ torqueForce 구현간의 난항  
 https://github.com/mw08081/MathNPhysics2D/blob/main/Assets/Scenes/Physics/TorqueForce/README.md  
#### 5. 탄성력(ElasticForce)
탄성력이란 고체의 변형에 의하여 생기는 힘으로 처음의 상태로 되돌아가려는 성질 때문이다  
충돌 탄성에서는 3가지 경우에 따라 두 물체사이에서의 운동량의 차이가 생긴다  
다음은 위에서 부터 완전탄성/비탄성충돌/완전비탄성충돌에 대한 예시이다  
![collisionSimul](https://user-images.githubusercontent.com/58582985/135846516-bd6a46b6-9b30-43e7-a8c2-f3d1361e5995.gif)

운동량은 다음과 같다 `p = m * v`  
따라서 운동량은 m1 * v1 + m2 * v2 = m'1 * v'1 + m'2 * v'2  

+++ 탄성력 구현 코드  
https://github.com/mw08081/MathNPhysics2D/tree/main/Assets/Scenes/Physics/ElasticForce
