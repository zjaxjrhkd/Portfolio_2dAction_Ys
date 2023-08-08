using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansHole : MonoBehaviour
{
    private GameObject bObject; //�÷��̾��� ��ġ���� �ޱ� ���� ���� ���ݻ����غ��� �׳�Transform���� �ص� ������ ����.
    private float time;
    private float pullSpeed; // ������� �ӷ�

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
        // target ������Ʈ�� ��ġ�� ��ǥ ��ġ�� ����
        Vector3 targetPosition = transform.position;
        // ������� ���� ���
        Vector3 direction = targetPosition - bObject.transform.position;
        // �����ӿ� ���� �̵��� ���
        float step = pullSpeed * Time.deltaTime;
        // hole ������Ʈ�� ������� �̵�
        bObject.transform.position += direction.normalized * step;
    }
}
