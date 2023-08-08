using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgoreFire : MonoBehaviour
{

    private Transform target;
    public float speed;
    public int damage = 1;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
            Chaser();
    }
    void Chaser() 
    {
        if (target)
        {
            Vector3 direction = target.position - transform.position;//Ÿ���� ��ġ�� �� ��ġ�� ���� �� �� ������ ����ġ Ȯ��
            //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);//�� �� ������ ��ġ�� ���� ������Ʈ�� �������� �������� ���� �Ǵ�
            direction.z = 0f; // Z �� �̵� ����
            transform.Translate(direction.normalized * speed * Time.deltaTime);//
        }
    }
    public void FireOff()//�ð��� ������ �ڵ����� �����
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Status Takedamage = collision.gameObject.GetComponent<Status>();
            if (Takedamage != null)
            {
                Takedamage.TakeDamage(damage);
                Destroy(gameObject);

            }
        }
        else if(collision.CompareTag("Attack"))
        {
            Destroy(gameObject);
        }
    }

}
