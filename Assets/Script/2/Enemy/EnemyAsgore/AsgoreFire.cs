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
            Vector3 direction = target.position - transform.position;//타겟의 위치와 내 위치를 빼서 두 점 사이의 점위치 확인
            //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);//두 점 사이의 위치와 불의 오브젝트의 위방향을 기준으로 방향 판단
            direction.z = 0f; // Z 축 이동 방지
            transform.Translate(direction.normalized * speed * Time.deltaTime);//
        }
    }
    public void FireOff()//시간이 지나면 자동으로 사라짐
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
