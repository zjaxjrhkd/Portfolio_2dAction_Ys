using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapyBoneSkill : MonoBehaviour
{
    public float boneSpeed = 3; //속도 : Inspector에 지정
    private Rigidbody2D boneRb2d;
    public int damage = 1;

    void Start()
    {
        boneRb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Update()
    {
        Vector2 movement = new Vector2(-boneSpeed, 0);
        boneRb2d.velocity = movement;
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
}
