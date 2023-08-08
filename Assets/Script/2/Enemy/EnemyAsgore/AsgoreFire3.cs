using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgoreFire3 : MonoBehaviour
{

    public int damage = 1;


    private float speed; // �Ʒ��� �������� �ӵ�

    public GameObject obj1;
    private Transform target;


    private float objTime;
    private Rigidbody2D rb;

    public float rotationSpeed = 180f; // ȸ�� �ӵ��� �����ϴ� �����Դϴ�.

    private SpriteRenderer spriteRenderer;
    private bool change;

    // Start is called before the first frame update
    private void Start()
    {
        EndFire();
        speed = 1f;
        objTime = 0;
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindWithTag("Player") != null)//�÷��̾��� ��ġ�� ã�� �ڵ�
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        change=true;//���� �ٲ������ �Ǵ��ϴ� ����
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.localScale.x == 3)    
        {
            Chaser();
        }
        else if(transform.localScale.x == 6)
        {
            FireOrb();
        }

    }
    void Chaser()
    {
        objTime += Time.deltaTime;
        if (objTime > 2)
        { 
            if (target)
            {
                Vector2 direction = target.position - transform.position;
                float targetAngle = Mathf.Atan2(direction.y,
                    direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f,
                    targetAngle + 90.0f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    targetRotation, rotationSpeed * Time.deltaTime);
                speed = 6.0f;
                if (change)
                {
                    ColorChanged();
                }
            }
        }
    }

    private void FireOrb()
    {
        objTime += Time.deltaTime;

        if (objTime > 1)
        {
            GameObject[] asd = new GameObject[24];
            for (int i = 0; i < 24; i++)
            {
                asd[i] = Instantiate(obj1, transform.position,
                    Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + i*15.0f));
                asd[i].transform.localScale = new Vector3(transform.localScale.x - 3,
                    transform.localScale.y - 3, 1);
            }

            Destroy(gameObject);
            objTime = 0;
        }
    }
    private void Move()
    {
        rb.velocity = -transform.up * speed;
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
        if (transform.localScale.x == 3)
        {
            if (collision.CompareTag("Asgore"))
            {
                Status Takedamage = collision.gameObject.GetComponent<Status>();
                if (Takedamage != null)
                {
                    Takedamage.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }

        }
    }
    void EndFire()
    {
        if(transform.localScale.x==0)
        {
            Destroy(gameObject);
        }
    }

    void ColorChanged()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �÷� ���� ������ �� ����� ���ο� �÷� �� ����
        Color newColor = new Color(255f, 00f, 0f); // ���÷� �����(��Ȳ���� �ʷϻ��� ����)�� ���

        // ��������Ʈ �������� �÷� ���� ����
        spriteRenderer.color = newColor;
        change = false;
    }


}
