using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Animator spear;
    public int damage=1;


    void Start()
    {
        spear = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Break"))||(collision.CompareTag("Attack")))
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
            spear.SetBool("isNear", true);
        }
    }
}