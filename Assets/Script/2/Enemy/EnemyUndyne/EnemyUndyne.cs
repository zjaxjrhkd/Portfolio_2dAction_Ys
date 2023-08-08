using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUndyne : MonoBehaviour
{
    public GameObject attackObject;
    public GameObject boneWall;

    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    
    public Animator attack;
    public GameObject spark;

    private float objTime = 0;
    private float objCoolTime = 0.5f;
    private float obj2Time = 0;
    private float obj2CoolTime = 0.8f;
    private float obj3Time = 0;
    private float obj3CoolTime = 0.6f;

    private float attackTime = 0;
    private float attackCoolTime = 0.5f;



    private int enemyPhase = 1;

    private float phaseTime = 0;

    private float nextPhaseTime = 10;
    private void Start()
    {
        SummonBoneWall();
    }
    void SummonBoneWall()
    {
        boneWall = Instantiate(boneWall);
        boneWall.SetActive(true);
        boneWall.transform.position = new Vector3(-10.82f, 1.24f, 0f);
    }
    void Update()
    {
        phaseTime += Time.deltaTime;
        if (enemyPhase == 2)
        {
            ThrowSpear();
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                phaseTime = 0;
            }
        }
        else if (enemyPhase == 3)
        {
            ThrowSpear();
            ThrowCrossSpear();
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                phaseTime = 8;
                boneWall.SetActive(false);
            }
        }
        else if (enemyPhase == 1)
        {
            boneWall.SetActive(false);
            Teleportation();
            EnemyAttack();

            if (phaseTime > nextPhaseTime)
            {
                boneWall.SetActive(true);
                summon();
                enemyPhase++;
                phaseTime = 0;
            }
        }
        else if (enemyPhase == 4)
        {

            if (phaseTime > nextPhaseTime)
            {
                phaseTime = 0;
                enemyPhase = 1;
            }

        }
        else if (enemyPhase == 10)
        {
            if (phaseTime > nextPhaseTime)
            {
                phaseTime = 0;
                enemyPhase = 1;
            }
        }
    }
    void EnemyAttack()
    {
        attackTime += Time.deltaTime;
        if (attackTime > attackCoolTime)
        {
            int randomPosition = Random.Range(0, 7);
            attack.SetBool("isAttack", true);
            if (randomPosition == 0)
            {
                transform.position = new Vector3(-6f, 0f, 0f);

            }
            else if (randomPosition == 1)
            {
                transform.position = new Vector3(-4f, 0f, 0f);


            }
            else if (randomPosition == 2)
            {
                transform.position = new Vector3(-2f, 0f, 0f);


            }
            else if (randomPosition == 3)
            {
                transform.position = new Vector3(0f, 0f, 0f);


            }
            else if (randomPosition == 4)
            {
                transform.position = new Vector3(2f, 0f, 0f);

            }
            else if (randomPosition == 5)
            {
                transform.position = new Vector3(4f, 0f, 0f);

            }
            else if (randomPosition == 6)
            {
                transform.position = new Vector3(6f, 0f, 0f);
            }
            attackTime = 0;
        }
    }

    void Teleportation()
    {
        obj3Time += Time.deltaTime;
        if (obj3Time> obj3CoolTime)
        {
            int randomPosition = Random.Range(0, 7);
            attack.SetBool("isThrow", true);
            if (randomPosition == 0)
            {
                transform.position = new Vector3(-6f, 2.5f, 0f);

            }
            else if (randomPosition == 1)
            {
                transform.position = new Vector3(-4f, 2.5f,0f);


            }
            else if (randomPosition == 2)
            {
                transform.position = new Vector3(-2f, 2.5f, 0f);


            }
            else if (randomPosition == 3)
            {
                transform.position = new Vector3(0f, 2.5f, 0f);


            }
            else if (randomPosition == 4)
            {
                transform.position = new Vector3(2f, 2.5f, 0f);

            }
            else if (randomPosition == 5)
            {
                transform.position = new Vector3(4f, 2.5f,0f);

            }
            else if (randomPosition == 6)
            {
                transform.position = new Vector3(6f, 2.5f, 0f);

            }
            CreateObj3AndSetVelocity(new Vector2(transform.position.x-1.5f, 2f), new Vector2(0f, -8f), Quaternion.Euler(0f, 0f, 270f));

            obj3Time = 0;
        }
    }
    void CreateObj3AndSetVelocity(Vector2 position, Vector2 velocity, Quaternion rotation)
    {
        GameObject spear3 = Instantiate(obj3);
        spear3.transform.position = position;
        spear3.transform.rotation = rotation;

        Rigidbody2D spearRigidbody3 = spear3.GetComponent<Rigidbody2D>();
        spearRigidbody3.velocity = velocity;
    }


    void CreateObjectAndSetVelocity(Vector2 position, Vector2 velocity, Quaternion rotation)
    {
        GameObject spear = Instantiate(obj);
        spear.transform.position = position;
        spear.transform.rotation = rotation;

        Rigidbody2D spearRigidbody = spear.GetComponent<Rigidbody2D>();
        spearRigidbody.velocity = velocity;
    }

    void ThrowSpear()
    {
        objTime += Time.deltaTime;
        if (objCoolTime < objTime)
        {

            int location = Random.Range(0, 4);
            if (location == 0)
            {
                CreateObjectAndSetVelocity(new Vector2(0f, 4f)
                    , new Vector2(0f, -4f), Quaternion.Euler(0f, 0f,0f));

            }
            else if (location == 1)
            {
                CreateObjectAndSetVelocity(new Vector2(0f, -4f)
                    , new Vector2(0f, 4f), Quaternion.Euler(0f, 0f, 180f));
            }
            else if (location == 2)
            {
                CreateObjectAndSetVelocity(new Vector2(8f, -1.3f)
                    , new Vector2(-4f, 0f), Quaternion.Euler(0f, 0f, 270f));
            }
            else if (location == 3)
            {
                CreateObjectAndSetVelocity(new Vector2(-8f, -1.3f)
                    , new Vector2(4f, 0f), Quaternion.Euler(0f, 0f, 90f));
            }
            objTime = 0;
        }
    }

    void ThrowCrossSpear()
    {
        obj2Time += Time.deltaTime;
        if (obj2CoolTime < obj2Time)
        {
            int location = Random.Range(0, 4);
            if (location == 0)
            {
                CreateObj2AndSetVelocity(new Vector2(0f, 4f), new Vector2(0f, -4f), Quaternion.Euler(0f, 0f, 0f));
            }
            else if (location == 1)
            {
                CreateObj2AndSetVelocity(new Vector2(0f, -4f), new Vector2(0f, 4f), Quaternion.Euler(0f, 0f, 180f));
            }
            else if (location == 2)
            {
                CreateObj2AndSetVelocity(new Vector2(8f, -1.3f), new Vector2(-4f, 0f), Quaternion.Euler(0f, 0f, 270f));
            }
            else if (location == 3)
            {
                CreateObj2AndSetVelocity(new Vector2(-8f, -1.3f), new Vector2(4f, 0f), Quaternion.Euler(0f, 0f, 90f));
            }
            obj2Time = 0;
        }
    }

    void CreateObj2AndSetVelocity(Vector2 position, Vector2 velocity, Quaternion rotation)
    {
        GameObject spear2 = Instantiate(obj2);

        spear2.transform.position = position;
        spear2.transform.rotation = rotation;

        Rigidbody2D spearRigidbody2 = spear2.GetComponent<Rigidbody2D>();
        spearRigidbody2.velocity = velocity;
    }
    public void UndyenWowWow()
    {
        boneWall.SetActive(false);
        phaseTime = 0;

        attack.SetBool("isHate", true);
        foreach (Transform child in transform)
        {
            if (child.name == "Spark")
            {
                child.gameObject.SetActive(true);
            }
        }
        enemyPhase = 10;
    }
    void summon()
    {
        GameObject system = GameObject.FindWithTag("System");
        EnemySommon call = system.GetComponent<EnemySommon>();
        call.SummonPlayer();
    }
}
