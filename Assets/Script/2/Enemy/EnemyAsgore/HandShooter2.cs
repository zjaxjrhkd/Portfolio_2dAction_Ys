using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShooter2 : MonoBehaviour
{
    public float angle = 15f; // 각도 (도 단위)
    public float distance = 8f; // 거리

    private float objTime = 0;

    private float spin = 0;

    public GameObject fire;


    private Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        MoveToNewPosition();
        FireFire();
    }

    private Vector3 CalculatePosition(Vector3 basePosition, Vector3 forward, float angleDegrees, float distance)
    {
        float distanceAB = Vector3.Distance(basePosition, forward);
        // a에서 b로 향하는 벡터 계산
        Vector3 directionAB = (forward - basePosition).normalized;
        // 벡터를 15도 회전
        Quaternion rotation = Quaternion.Euler(0f, 0f, -angleDegrees);
        Vector3 rotatedDirection = rotation * directionAB;

        // 회전한 벡터의 크기를 5로 설정하여 길이 조정
        Vector3 adjustedDirection = rotatedDirection * distance;

        // a의 위치에 조정된 벡터를 더하여 최종적인 좌표 계산
        Vector3 newPosition = basePosition + adjustedDirection;

        return newPosition;
    }

    private void MoveToNewPosition()
    {
        Vector3 newPosition = CalculatePosition(target.position, target.forward, 40f + spin, distance);
        transform.position = newPosition;
        spin += Time.deltaTime * 40;
        if (spin > 200)
        {
            Destroy(gameObject);
        }

    }

    private void FireFire()
    {
        objTime += Time.deltaTime;

        if (objTime > 0.2)
        {
            GameObject asgorefire = Instantiate(fire);
            asgorefire.transform.position = transform.position;
            objTime = 0;
        }
    }
}

