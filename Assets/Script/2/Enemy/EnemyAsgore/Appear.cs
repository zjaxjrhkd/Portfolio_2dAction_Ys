using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public Animator appearAnime;
    private EnemySommon end;


    private void Start()
    {
        appearAnime = GetComponent<Animator>();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void AppearMotion()
    {
        appearAnime.SetBool("isAppear", true);
    }

    private void AppearAsgore()
    {
        GameObject end = GameObject.FindWithTag("System");
        if (end != null)
        {
            EnemySommon endComponent = end.GetComponent<EnemySommon>();
            if (endComponent != null)
            {
                endComponent.EndAppear();
            }
        }
    }

}
