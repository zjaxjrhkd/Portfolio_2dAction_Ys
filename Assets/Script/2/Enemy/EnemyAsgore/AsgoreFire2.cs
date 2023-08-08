using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgoreFire2 : MonoBehaviour
{

    public int damage = 1;


    private int random;

    public float fallSpeed = 4.0f; // �Ʒ��� �������� �ӵ�
    private float translationY;
    private float time;
    private float move;

    private Vector3 setPosition;
    private float distance;



    // Start is called before the first frame update
    private void Start()
    {
        setPosition = transform.position;
        random = Random.Range(0, 2);
        move = 0.0f;
        distance = 0.005f;
        if (random==1)//������ Ȯ���� ��������� �ݴ�� ������.
        {
            distance *= -1;
        }
    }
    // Update is called once per frame
    void Update()
    {
            Rain();
    }
    void Rain()
    {
        time += Time.deltaTime;

        translationY = setPosition.y-fallSpeed * time;
        if (move > 1.6f)//�ð��� �������� x������ �̵��ϰ� �̵��ѰŸ��� ���� �Ÿ� �̻��̶�� �ݴ�� �̵�
        {
            distance = -0.025f;
        }
        else if (move < -1.6f)
        {
            distance = 0.025f;
        }
        move += distance* Time.deltaTime*100;
        transform.position = new Vector3(setPosition.x + move, translationY, 0.0f);//�������� ���Ͱ����� ���� �����Ͽ� �̵��� ����
    }

    void FireOff()
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
        else if(collision.CompareTag("Break"))
        {
            Destroy(gameObject);
        }
    }

}
