using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapyChangeBone : MonoBehaviour
{
    public float boneSpeed = 3; //�ӵ� : Inspector�� ����
    private Rigidbody2D boneRb2d;
    private float change = 0.1f;
    private float changeTime;
    private int upAndDown=1;
    public int damage = 1;



    void Start()
    {
        boneRb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Update()
    {
        BoneMove();//���� ������
        BoneChange();//���� ũ�Ⱑ ��ȭ��
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Break"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            Status Takedamage = collision.gameObject.GetComponent<Status>();
            if (Takedamage != null)
            {
                Takedamage.TakeDamage(damage);
            }
        }

    }
    private void BoneMove()
    {
        Vector2 movement = new Vector2(-boneSpeed, 0);
        boneRb2d.velocity = movement;
    }

    private void BoneChange()
    {
        
        changeTime += Time.deltaTime;
        if (changeTime > 0.1)
        {
            change += (0.05f * upAndDown);
            if ((change >= 0.35)||(change <= 0.05))
            {
                upAndDown *= -1;
            }

            transform.localScale = new Vector3(0.1f, change, 0);
            changeTime = 0;
        }
    }
}
