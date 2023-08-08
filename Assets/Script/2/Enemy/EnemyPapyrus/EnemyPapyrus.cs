using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPapyrus : MonoBehaviour
{

    private int enemyPhase = 1;

    private float mountainFloat;
    private bool upScale = true;
    public bool live;

    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;

    public Animator anim;


    private float objTime = 0;
    private float obj2Time = 0;


    private float phaseTime = 0;

    private float nextPhaseTime = 15;
    private int direction;

    private PapyChangeBone changeBoneSpeed;
    private PapyBoneSkill bone;


    private void Start()
    {
        changeBoneSpeed = obj.GetComponent<PapyChangeBone>();  // BoneScript는 bone 오브젝트의 스크립트 이름입니다. 필요에 따라 스크립트를 수정하세요.
        bone = obj2.GetComponent<PapyBoneSkill>();  // BoneScript는 bone 오브젝트의 스크립트 이름입니다. 필요에 따라 스크립트를 수정하세요.

        bone.boneSpeed = 3;
        changeBoneSpeed.boneSpeed = 3;

        direction = 1;
        mountainFloat = 0;
        anim = GetComponent<Animator>();

        live = true;
        
    }

    private void FixedUpdate()
    {
        objTime += Time.deltaTime;
        phaseTime += Time.deltaTime;
        if (phaseTime > 5 && enemyPhase == 1)
        {
            anim.SetBool("isAttack", true);
            EnemySkillBone(direction);
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                changeBoneSpeed.boneSpeed*= -1;
                phaseTime = 0;
            }
        }
        else if (enemyPhase == 2)
        {
            EnemySkillTopBone(direction);
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase--;
                phaseTime = 0;
                direction *= -1;
                bone.boneSpeed *= -1;
            }
        }
        else if (enemyPhase == 3)
        {
            anim.SetBool("isWow", true);
        }
    }

    private void EnemySkillBone(int direction)
    {
        objTime += Time.deltaTime;

        if (objTime > 2)
        {
            Instantiate(obj);
            obj.transform.position = new Vector3((direction * 6.0f), -2.75f, 0);
            objTime = 0;
        }
    }
    private void EnemySkillTopBone(int direction)
    {
        obj2Time += Time.deltaTime;
        if (obj2Time > 0.1)
        {
                Instantiate(obj2);
                obj2.transform.localScale = new Vector3(0.1f, mountainFloat, 0);
                obj2.transform.position = new Vector3((direction * 6.0f), -2.75f, 0);

                Instantiate(obj3);
                obj3.transform.localScale = new Vector3(0.1f, -(0.16f - mountainFloat), 0);
                obj3.transform.position = new Vector3(6 * direction, 0.00f, 0);
            if (upScale == true && mountainFloat >= 0.14f)
            {
                upScale = false;
            }
            else if (upScale == false && mountainFloat <= 0.02f)
            {
                upScale = true;
            }

            if (upScale == true)
            {
                mountainFloat += 0.02f;
            }
            else if (upScale == false)
            {
                mountainFloat -= 0.02f;
            }
            obj2Time = 0;
        }
    }
    public void PapyWowWow()
    {
        enemyPhase = 3;
    }
  
}
