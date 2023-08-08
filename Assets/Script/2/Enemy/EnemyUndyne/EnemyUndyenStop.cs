using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUndyenStop : MonoBehaviour
{
    Animator undyen;
    public GameObject slash;
    private void Start()
    {
        undyen = GetComponent<Animator>();
    }
    void ThrowStop()
    {
        undyen.SetBool("isThrow", false);
    }

    void SlashStop()
    {
        slash.SetActive(false);
        undyen.SetBool("isAttack", false);
    }

    void SlashRange()
    {
        slash.SetActive(true);
    }

}
