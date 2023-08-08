using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public int damage;
    /*private Status Enemy;
    private float coolTime;*/
    public float coolTimeOver;
    private CapsuleCollider2D attackRange;
    private float attackCheakTime=0;

    //public Transform playerRotation;

    void Start()
    {
        animator = GetComponent<Animator>();
        /*coolTime = 1.0f;*/
        attackRange = GetComponent<CapsuleCollider2D>();
        //playerRotation = GetComponent<Transform>();
    }
    private void Update()
    {
        animator.SetBool("isAttack", true);
        //transform.position = playerRotation.position;
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        attackCheakTime += Time.deltaTime;
                
        if (attackCheakTime > coolTimeOver && other.gameObject.CompareTag("Asgore")|| other.gameObject.CompareTag("Wall")||other.gameObject.CompareTag("Papyrus")||other.gameObject.CompareTag("Undyen") || other.gameObject.CompareTag("Sans"))
        {
            Status enemy = other.gameObject.GetComponent<Status>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
            attackCheakTime = 0;
        
        }
    }

    void AttackOff()
    {
        gameObject.SetActive(false);
    }
    void AttackRangeOn()
    {
        attackRange.enabled = true;
    }
    void AttackRangeOff()
    {
        attackRange.enabled=false;
    }
    private int CalculateDamage()
    {
        // 피해량을 계산하는 로직을 구현합니다.
        // 예를 들어, 무기의 공격력이나 플레이어의 능력치 등을 고려하여 피해량을 결정할 수 있습니다.
        damage = 1;  // 예시로 고정된 피해량 10을 사용하였습니다.
        return damage;
    }

}
