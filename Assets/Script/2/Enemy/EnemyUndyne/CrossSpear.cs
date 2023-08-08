using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSpear : MonoBehaviour
{

    private Animator crossSpear;
    private int count;
    public int damage=1;

    
    private void Start()
    {
        crossSpear = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Break")) || (collision.CompareTag("Attack")))
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

        if (collision.CompareTag("Change"))
        {
            crossSpear.SetBool("isChange", true);

            int random = Random.Range(0, 9);
            if (count == 0)
            {
                if (random == 0)
                {
                    ChangeMove(new Vector2(2f, 1f), new Vector2(-2f, -2f),
                        Quaternion.Euler(0f, 0f, 315f));
                    count++;
                }
                else if (random == 1)
                {
                    ChangeMove(new Vector2(0f, 1f), new Vector2(0f, -2f),
                        Quaternion.Euler(0f, 0f, 0f));
                    count++;
                }
                else if (random == 2)
                {
                    ChangeMove(new Vector2(-2f, 1f), new Vector2(2f, -2f),
                        Quaternion.Euler(0f, 0f, 45f));
                    count++;
                }
                else if (random == 3)
                {
                    ChangeMove(new Vector2(-2f, -1f), new Vector2(2f, 0f), Quaternion.Euler(0f, 0f, 90f));
                    count++;
                }
                else if (random == 4)
                {
                    ChangeMove(new Vector2(-2f, -3f), new Vector2(2f, 2f), Quaternion.Euler(0f, 0f, 135f));
                    count++;
                }
                else if (random == 5)
                {
                    ChangeMove(new Vector2(0f, -3f), new Vector2(0f, 2f), Quaternion.Euler(0f, 0f, 180f));
                    count++;
                }
                else if (random == 6)
                {
                    ChangeMove(new Vector2(2f, -3f), new Vector2(-2f, 2f), Quaternion.Euler(0f, 0f, 225f));
                    count++;
                }
                else if (random == 7)
                {
                    ChangeMove(new Vector2(2f,-1f), new Vector2(-2f, 0f), Quaternion.Euler(0f, 0f, 275f));
                    count++;
                }
            }

        }
        void ChangeMove(Vector2 position, Vector2 velocity, Quaternion rotation)
        {

            transform.position = position;
            transform.rotation = rotation;

            Rigidbody2D spear = GetComponent<Rigidbody2D>();
            spear.velocity = velocity;
        }
    }
}
