using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
