using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySans : MonoBehaviour
{

    private int enemyPhase = 1;

    private float mountainFloat;
    private bool upScale = true;

    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;


    public Animator sansAni;

    private float objTime = 0;
    private float obj2Time = 0;
    private float obj4Time = 0;

    private float phaseTime = 0;

    private float nextPhaseTime = 20;

    private int laserCounter;

    private Transform target;
    private Transform flip;
    private Bone topBoneSpeed;
    private Bone bottomBoneSpeed;
    private int pCount;

    private int direction;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        flip = GetComponent<Transform>();
        topBoneSpeed = obj2.GetComponent<Bone>();  // BoneScript는 bone 오브젝트의 스크립트 이름입니다. 필요에 따라 스크립트를 수정하세요.
        bottomBoneSpeed = obj3.GetComponent<Bone>();  // BoneScript는 bone 오브젝트의 스크립트 이름입니다. 필요에 따라 스크립트를 수정하세요.
        direction = 1;
        mountainFloat = 0;
        laserCounter = 0;
        sansAni = GetComponent<Animator>();

        obj5.transform.localScale = new Vector3(0.5f, -(5.0f - mountainFloat), 0);
        obj4.transform.localScale = new Vector3(0.5f, (mountainFloat), 0);
        obj2.transform.localScale = new Vector3(0.5f, -(0.16f - mountainFloat), 0);
        obj3.transform.localScale = new Vector3(0.5f, (mountainFloat), 0);
    }




    private void FixedUpdate()
    {
        phaseTime += Time.deltaTime;


        if (target != null)
        {
            Vector3 playerPosition = target.position;
        }
        if (enemyPhase == 1)
        {

            if (laserCounter < 10)
            {
                EnemySkillLaser();
            }
            else if (laserCounter == 10)
            {
                StartCoroutine(_CircleLaser());
                laserCounter = 12;
            }
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                phaseTime = 0;
                direction = 1;

                flip.transform.localScale = new Vector3(-4.0f, 4.0f, 1.0f);
                topBoneSpeed.boneSpeed = 3.0f;
                bottomBoneSpeed.boneSpeed = 3.0f;

            }
        }
        if (enemyPhase == 2)
        {
            EnemySkillTopBone(direction);
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                phaseTime = 0;
                flip.transform.localScale = new Vector3(4.0f, 4.0f, 1.0f);
                topBoneSpeed.boneSpeed = -3.0f;
                bottomBoneSpeed.boneSpeed = -3.0f;
                direction = -1;
                mountainFloat = 0;
            }
        }

        if (enemyPhase == 3)
        {
            EnemySkillTopBone(direction);
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase--;
                phaseTime = 0;
                topBoneSpeed.boneSpeed = 3.0f;
                bottomBoneSpeed.boneSpeed = 3.0f;
                direction = 1;
                mountainFloat = 0;
            }
        }

        if (enemyPhase == 4)
        {
            if (pCount == 0)
            {
                NextPhase();
                Alldelete();
                pCount++;
                phaseTime = 18;
            }

            if (phaseTime > nextPhaseTime)
            {
                HandDown();
                enemyPhase++;
                phaseTime = 0;
            }
        }

        if (enemyPhase == 5)
        {
            EnemySkillSkyLeftBone(direction);
            EnemySkillLaser();
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                phaseTime = 0;
                HandDown();
            }
        }

        if (enemyPhase == 6)
        {

            if (phaseTime > 3)
            {
                HandDown();
                EnemySkillWhiteHoll();
                StartCoroutine(_HoleLaser());
            }

            if (phaseTime > nextPhaseTime)
            {
                enemyPhase = 7;
                phaseTime = 0;
            }
        }
        if (enemyPhase == 7)
        {

            SansWowWow();
        }
        if (enemyPhase == 8)
        {
            if(phaseTime > nextPhaseTime)
            {
                enemyPhase = 7;
                phaseTime = 0;
            }
        }
        if (enemyPhase == 10)
        {
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase = 4;
                phaseTime = 15;
            }
        }

    }

    private void EnemySkillLaser()//레이져는 애니메이션이 끝나면 사라짐
    {
        int angle = Random.Range(0, 9);
        objTime += Time.deltaTime;
        if (objTime > 0.5)
        {
            Instantiate(obj);

            if (angle == 0)
            {

                obj.transform.position = target.position + new Vector3(4, 4, 0); //플레이어의 위치+위치에 포지션 값 설정
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y,
                    target.position.x - obj.transform.position.x); //플레이어의 위치를 지정한 위치에서 바라보는 각도 설정
                float angleDeg = angleRad * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));
            }
            else if (angle == 1)
            {
                obj.transform.position = target.position + new Vector3(4, 0, 0);
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y,
                    target.position.x - obj.transform.position.x);
                //1번째는 y좌표 차이 두번째는 x좌표 차이  Mathf.Atan2() 함수를 사용합니다. 이 함수는 두 좌표 사이의 각도를 라디안 값으로 계산해줍니다.

                float angleDeg = angleRad * Mathf.Rad2Deg;
                //Euler값으로 변환
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));
                //로테이션에 값을 직접 입력 90-는 오브젝트이미지의 방향을 맞추기위해 설정된 임의의 변수
            }
            else if (angle == 2)
            {
                obj.transform.position = target.position + new Vector3(4, -4, 0);
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y, target.position.x - obj.transform.position.x);
                float angleDeg = angleRad * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));
            }
            else if (angle == 3)
            {
                obj.transform.position = target.position + new Vector3(-4, 0, 0);
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y, target.position.x - obj.transform.position.x);
                float angleDeg = angleRad * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));
            }
            else if (angle == 4)
            {
                obj.transform.position = target.position + new Vector3(-4, -4, 0);
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y, target.position.x - obj.transform.position.x);
                float angleDeg = angleRad * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));

            }
            else if (angle == 5)
            {
                obj.transform.position = target.position + new Vector3(0, -4, 0);
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y, target.position.x - obj.transform.position.x);
                float angleDeg = angleRad * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));
            }
            else if (angle == 6)
            {
                obj.transform.position = target.position + new Vector3(4, -4, 0);
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y, target.position.x - obj.transform.position.x);
                float angleDeg = angleRad * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));
            }
            else if (angle == 7)
            {
                obj.transform.position = target.position + new Vector3(0, 4, 0);
                float angleRad = Mathf.Atan2(obj.transform.position.y - target.position.y, target.position.x - obj.transform.position.x);
                float angleDeg = angleRad * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, (90 - angleDeg));
            }
            laserCounter++;

            objTime = 0;
        }
    }

    IEnumerator _CircleLaser()
    {
        Vector2 targetPosition = target.transform.position; // 레이져를 쏠타겟의 위치
        GameObject[] circle = new GameObject[36];

        for (int i = 0; i < 36; i++)
        {
            targetPosition = target.transform.position;
            circle[i] = Instantiate(obj);
            Vector2 direction = Quaternion.Euler(0f, 0f, -20 * i) * Vector2.up; // 각도에 따른 방향 벡터 계산
            Vector2 newPosition = targetPosition - direction * 5f; // 타겟 위치에서 방향과 거리를 고려한 새로운
                                                                   // 위치 계산 targetPosition과 direction값을 빼면 포지션의 위치가 나옴
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, newPosition - targetPosition);

            circle[i].transform.rotation = rotation;
            circle[i].transform.position = newPosition;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(_CircleLaser());
        laserCounter = 0;
    }

    IEnumerator _HoleLaser()
    {
        Vector2 targetPosition = obj6.transform.position; // 생성될 레이져의 위치
        GameObject[] circle = new GameObject[72];

        for (int i = 0; i < 72; i++)
        {
            targetPosition = obj6.transform.position;
            circle[i] = Instantiate(obj);
            Vector2 direction = Quaternion.Euler(0f, 0f, -20 * i) * Vector2.up; // 각도에 따른 방향 벡터 계산
            Vector2 newPosition = targetPosition - direction * 5f; // 타겟 위치에서
                                                                   // 방향과 거리를 고려한 새로운 위치 계산
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward,
                newPosition - targetPosition);

            circle[i].transform.rotation = rotation;
            circle[i].transform.position = newPosition;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(_HoleLaser());
        laserCounter = 0;
    }



    private void EnemySkillTopBone(int direction)
    {
        obj2Time += Time.deltaTime;

        if (obj2Time > 0.05)
        {
            Instantiate(obj2);
            obj2.transform.localScale = new Vector3(0.1f, mountainFloat, 0);
            obj2.transform.position = new Vector3((direction * 6.0f), -2.75f, 0);

            Instantiate(obj3);
            obj3.transform.localScale = new Vector3(0.1f, -(0.16f - mountainFloat), 0);
            obj3.transform.position = new Vector3(6 * direction, 0.00f, 0);

            if (upScale == true && mountainFloat >= 0.16f)
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

    private void EnemySkillSkyLeftBone(int direction)
    {
        obj4Time += Time.deltaTime;

        if (obj4Time > 0.5)
        {
            Instantiate(obj4);
            Instantiate(obj5);

            obj4.transform.localScale = new Vector3(0.5f, (mountainFloat), 0);
            obj4.transform.position = new Vector3(-9.0f * direction, 5.0f, 0);

            obj5.transform.localScale = new Vector3(0.5f, -(4.0f - mountainFloat), 0);
            obj5.transform.position = new Vector3(9.0f * direction, 5.0f, 0);

            if (upScale == true && mountainFloat >= 3.5f)//스케일 값을 늘릴지 내릴지 판단하는 if문
            {
                upScale = false;
            }
            else if (upScale == false && mountainFloat <= 0.5f)
            {
                upScale = true;
            }

            if (upScale == true)//스케일 값을 늘리거나 줄인다
            {
                mountainFloat += 0.5f;
            }
            else if (upScale == false)
            {
                mountainFloat -= 0.5f;
            }

            obj4Time = 0;
        }
    }


    public void SansWowWow()
    {
        sansAni.SetBool("isHate", true);
        foreach (Transform child in transform)
        {
            if (child.name == "Spark")
            {
                child.gameObject.SetActive(true);
            }
        }
        enemyPhase = 10;

    }
    public void SansWowEnd()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Spark")
            {
                child.gameObject.SetActive(false);
            }
        }
        sansAni.SetBool("isHate", false);

        phaseTime = 18;
    }
    public void NextPhase()//Sans의 두번째 대화 출력 메소드
    {
        GameObject text = GameObject.FindWithTag("System");
        GameManager2 textDialog = text.GetComponent<GameManager2>();
        textDialog.Talk(6);
        textDialog.Type();

        phaseTime = 15;
    }

    public void Alldelete()//패턴 진행시 모든 UI를 없애는 메소드
    {
        GameObject[] wallBroken = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject obj1 in wallBroken)
        {
            obj1.SetActive(false);
        }

        GameObject[] endPhaseObjects = GameObject.FindGameObjectsWithTag("EndPhase");

        foreach (GameObject obj in endPhaseObjects)
        {
            obj.SetActive(false);
        }
    }

    public void EnemySkillWhiteHoll()
    {
        summon();
        Instantiate(obj6);
        enemyPhase = 8;
        phaseTime = 10;

    }
    void summon()//캐릭터를 초기 위치로 소환
    {
        GameObject system = GameObject.FindWithTag("System");
        EnemySommon call = system.GetComponent<EnemySommon>();
        call.SummonPlayer();
    }

    void HandDown()//손을 내리는 애니메이션 실행
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Body")
            {
                EffectDes bodyComponent = child.GetComponent<EffectDes>();
                if (bodyComponent != null)
                {
                    bodyComponent.HandDown();
                }
            }
        }
    }
}

