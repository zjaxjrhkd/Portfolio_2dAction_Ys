using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgoreSlash : MonoBehaviour
{
    public Animator appearAnime;
    private Collider2D attackRange;
    public int damage=1;

    private void Start()
    {
        appearAnime = GetComponent<Animator>();
        attackRange = GetComponent<Collider2D>();

    }
    public void SlashOn()
    {
        appearAnime.SetBool("isSlash", true);
    }

    private void SlashOff()
    {
        appearAnime.SetBool("isSlash", false);
        attackRange.enabled = false;

    }
    private void SlashRangeOn()
    {
        attackRange.enabled = true;

    }

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
