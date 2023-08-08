using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgoreFire2 : MonoBehaviour
{

    public int damage = 1;


    private int random;

    public float fallSpeed = 4.0f; // 아래로 떨어지는 속도
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
        if (random==1)//랜덤한 확률로 진행방향을 반대로 진행함.
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
        if (move > 1.6f)//시간이 지날수록 x축으로 이동하고 이동한거리가 일정 거리 이상이라면 반대로 이동
        {
            distance = -0.025f;
        }
        else if (move < -1.6f)
        {
            distance = 0.025f;
        }
        move += distance* Time.deltaTime*100;
        transform.position = new Vector3(setPosition.x + move, translationY, 0.0f);//포지션을 벡터값으로 직접 조정하여 이동을 구현
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
