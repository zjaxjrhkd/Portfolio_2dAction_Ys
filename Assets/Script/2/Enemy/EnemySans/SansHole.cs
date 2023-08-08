using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansHole : MonoBehaviour
{
    private GameObject bObject; //플레이어의 위치값을 받기 위한 변수 지금생각해보면 그냥Transform으로 해도 됬을것 같다.
    private float time;
    private float pullSpeed; // 끌어당기는 속력

    private void Start()
    {
        bObject = GameObject.FindWithTag("Player");
        pullSpeed = 3;
    }
    private void Update()
    {
        if(time>2)
        {
            Hole();
        }
        time += Time.deltaTime;
        if(time>10)
        {
            
            Destroy(gameObject);
        }
        else if(time>5)
        {
            pullSpeed = 7f;
        }

    }
    void Hole()//
    {
        // target 오브젝트의 위치를 목표 위치로 설정
        Vector3 targetPosition = transform.position;
        // 끌어당기는 방향 계산
        Vector3 direction = targetPosition - bObject.transform.position;
        // 프레임에 따른 이동량 계산
        float step = pullSpeed * Time.deltaTime;
        // hole 오브젝트를 끌어당기는 이동
        bObject.transform.position += direction.normalized * step;
    }
}
