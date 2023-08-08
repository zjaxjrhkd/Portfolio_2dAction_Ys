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
        // ���ط��� ����ϴ� ������ �����մϴ�.
        // ���� ���, ������ ���ݷ��̳� �÷��̾��� �ɷ�ġ ���� ����Ͽ� ���ط��� ������ �� �ֽ��ϴ�.
        damage = 1;  // ���÷� ������ ���ط� 10�� ����Ͽ����ϴ�.
        return damage;
    }

}
