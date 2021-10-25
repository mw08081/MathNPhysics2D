## VectorDotProduct 실습 중에...
#### 내적 안에 Gost가 제대로 감지되지 않을 때  
처음에는 내적계산이 안되서 Gost가 감지되지 않는줄 알았다  
그러나 Gost들이 처음부터 gosts 배열안에 들어오지도 못한 것이였다  

이유는 다음과 같다  
```C#
//Light.cs 
void Start()
{
    gosts = GameObject.FindGameObjectsWithTag("gost");
}

//Gost.cs
void Start()
{
    gameObject.SetActive(false);
}
```
처음에는 운좋게 gost가 생성되고 -> Light.cs에서 gost를 gosts배열에 대입 -> 모든 gost가 setActive(false)가 된것이다    
그러나 이후 gost가 잘 감지되지 않아 `Debug.log(gosts.Length);`를 출력하니 42개가 아닌 10개밖에 안되는 것을 확인했다  

그래서 코드를 다음과 같이 수정했다  
```C#
//Light.cs 
void Awake()
{
    gosts = GameObject.FindGameObjectsWithTag("gost");
}

//Gost.cs
void Start()
{
    gameObject.SetActive(false);
}
```
함수 라이프 사이클에 의거해서 Gost.start()되어 setActive(fasle)하기전에 Light.cs에서 Awake()를 통해 gost를 모두 gosts로 대입하는 것이다
