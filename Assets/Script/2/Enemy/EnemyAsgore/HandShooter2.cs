using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShooter2 : MonoBehaviour
{
    public float angle = 15f; // ���� (�� ����)
    public float distance = 8f; // �Ÿ�

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
        // a���� b�� ���ϴ� ���� ���
        Vector3 directionAB = (forward - basePosition).normalized;
        // ���͸� 15�� ȸ��
        Quaternion rotation = Quaternion.Euler(0f, 0f, -angleDegrees);
        Vector3 rotatedDirection = rotation * directionAB;

        // ȸ���� ������ ũ�⸦ 5�� �����Ͽ� ���� ����
        Vector3 adjustedDirection = rotatedDirection * distance;

        // a�� ��ġ�� ������ ���͸� ���Ͽ� �������� ��ǥ ���
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

