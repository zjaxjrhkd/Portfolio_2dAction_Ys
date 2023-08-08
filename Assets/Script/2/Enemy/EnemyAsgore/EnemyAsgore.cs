using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsgore : MonoBehaviour
{
    private int enemyPhase = 1;

    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject dumy;

    public GameObject obj4;

    private bool dumyCheack;

    private float objTime = 0;
    private float obj2Time = 0;
    private float obj4Time = 0;

    private float phaseTime = 0;
    private float nextPhaseTime = 20;

    private Transform target;
    public Animator asgoreAnime;



    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        objTime = 5;
        obj4Time = 4;
        asgoreAnime = GetComponent<Animator>();
    }

    private void Update()//몬스터의 패턴 진행
    {
        if (target != null)
        {
            Vector3 playerPosition = target.position;
        }
        phaseTime += Time.deltaTime;//시간이 지남에 따라 여러가지 스킬 사용
        if (enemyPhase == 1)//패턴 변화를 위한 변수 사용
        {
            EnemySkillHand();//스킬 사용
        }
        if (enemyPhase == 2)//벽이 부서지면 2페이즈로 돌입
        {
            if (phaseTime < 10)//10초동안만 사용하고 더미가 스킬을 사용하고 사라짐
            {
                EnemySkillRain();
            }
            if (!dumyCheack)
            {
                EnemySkillRun();//더미가 스킬을 사용할 동안 사라짐
            }
            if (phaseTime > nextPhaseTime)//일정시간이 지나면 패턴 변환
            {
                enemyPhase++;
                DumyDestroy();
                asgoreAnime.SetBool("isRun", false);//본체가 원래자리로 돌아옴

                phaseTime = 0;
            }
        }
        if (enemyPhase == 3)
        {
            EnemySkillFireOrb();//스킬사용
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                phaseTime = 0;
            }
        }
    }

    private void EnemySkillHand()//플레이어 캐릭터로 부터 240도, 40도 기울어지고 거리고 3떨어진 곳에 손을 위치하고 싶다.
    {
        objTime += Time.deltaTime;
        if (objTime > 6)
        {
            GameObject instantiatedObj = Instantiate(obj); //게임오브젝트 변수에 생성된 오브젝트값을 입력
            Vector3 newPosition = CalculatePosition(target.position,
                target.forward, 240f, 3f); //타겟(플레이어), 
            instantiatedObj.transform.position = newPosition;

            GameObject instantiatedObj2 = Instantiate(obj2);
            Vector3 newPosition2 = CalculatePosition(target.position,
                target.forward, 40f, 3f);
            instantiatedObj2.transform.position = newPosition2;

            objTime = 0;
        }
    }

    private Vector3 CalculatePosition(Vector3 basePosition, Vector3 forward, float angleDegrees, float distance) 
    {
        //a와 b의  거리 계산
        float distanceAB = Vector3.Distance(basePosition, forward);//포지션값과 방향을 지정

        // a에서 b로 향하는 벡터 계산
        Vector3 directionAB = (forward - basePosition).normalized;//거리 값을 남기기 위해 계산, 소수점일 수도 있어서 nomalized실행
        
        //주어진 각도(angleDegrees)에 대한 회전 퀴터니온(rotation)을 계산합니다. 여기서는 Z축을 기준으로 주어진 각도만큼 회전시키기 위해 Euler 각(0, 0, -angleDegrees)을 사용합니다.
        Quaternion rotation = Quaternion.Euler(0f, 0f, -angleDegrees);

        Vector3 rotatedDirection = rotation * directionAB;//벡터의 계산을 위해서 벡터값으로 변환 방향*거리

        // 회전한 벡터의 크기를 5로 설정하여 길이 조정
        Vector3 adjustedDirection = rotatedDirection * distance;//내가 원하는 거리를 추가

        // a의 위치에 조정된 벡터를 더하여 최종적인 좌표 계산
        Vector3 newPosition = basePosition + adjustedDirection;//타겟의 위치+계산된 위치

        return newPosition;//최종 값 리턴, 지금생각해보면 거리는 내가 지정하고 forward기준으로 rotation만 +해줘도 될 것 같다.
    }

    private void EnemySkillRain()
    {
        obj2Time += Time.deltaTime;//해당 스킬이 사용된지 얼마나 됬는지 변수로 저장
        int random = Random.Range(0, 5);//랜덤으로 0~4까지 입력


        if (obj2Time > 0.02)//스킬을 사용한지 0.02초가 됬으면 스킬 사용
        {

            if (random == 0)// 5개의 위치에 생성
            {
                GameObject instantiatedObj2 = Instantiate(obj3);
                instantiatedObj2.transform.position = new Vector3(-7.0f, 4.5f, 0.0f);
            }
            else if (random == 1)
            {
                GameObject instantiatedObj2 = Instantiate(obj3);
                instantiatedObj2.transform.position = new Vector3(-3.0f, 4.5f, 0.0f);
            }
            else if (random == 2)
            {
                GameObject instantiatedObj2 = Instantiate(obj3);
                instantiatedObj2.transform.position = new Vector3(0.0f, 4.5f, 0.0f);
            }
            else if (random == 3)
            {
                GameObject instantiatedObj2 = Instantiate(obj3);
                instantiatedObj2.transform.position = new Vector3(3.0f, 4.5f, 0.0f);
            }
            else if (random == 4)
            {
                GameObject instantiatedObj2 = Instantiate(obj3);
                instantiatedObj2.transform.position = new Vector3(7.0f, 4.5f, 0.0f);
            }

            obj2Time = 0;
        }
    }

    private void EnemySkillRun()//벽 밖으로 도망치고 더미를 생성해 공격시킨다.
    {

        asgoreAnime.SetBool("isRun", true);
        GameObject Dumy2 = Instantiate(dumy);
        Dumy2.transform.position = new Vector3(0.0f, 2.83f, 0.0f);
        dumyCheack = true;

    }

    private void EnemySkillFireOrb()//rain과 마찬가지로 생성배열을 통해 코드 간결화 시도함
    {
        int random = Random.Range(0, 5);

        obj4Time += Time.deltaTime;
        if (obj4Time > 1)
        {
            GameObject[] fireOrb = new GameObject[4];
            for(int i = 0;i<4;i++)
            {
                    fireOrb[i] = Instantiate(obj4);
                if (random == 0)
                {
                    fireOrb[i].transform.position = new Vector3(-5f, 3f, 0.0f);
                }
                else if (random == 1)
                {
                    fireOrb[i].transform.position = new Vector3(-5f, -1f, 0.0f);
                }
                else if (random == 2)
                {
                    fireOrb[i].transform.position = new Vector3(5f, 3f, 0.0f);
                }
                else if (random == 3)
                {
                    fireOrb[i].transform.position = new Vector3(5f, -3f, 0.0f);
                }
            }
            obj4Time = 0;
        }

    }



    public void AsgoreWowWow()//벽이 부서지면 실행되는 메소드
    {
        asgoreAnime.SetBool("isDamage", true);
        foreach (Transform child in transform)//자식오브젝트 중에서 놀라는 오브젝트를 활성화
        {
            if (child.name == "AsgoreWow")
            {
                child.gameObject.SetActive(true);
            }
        }
        if (enemyPhase == 1)
        {
            phaseTime = 0;
            enemyPhase++;
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Fire");//벽이 부서지면 불 오브젝트를 다 파괴시킴

            foreach (GameObject fire in objectsWithTag)
            {
                Destroy(fire);
            }
        }
    }

    void TakeDamageAnimeEnd()//애니메이션에서 애니메이션 종료용으로 만든 메소드
    {
        foreach (Transform child in transform)
        {
            if (child.name == "AsgoreWow")
            {
                child.gameObject.SetActive(false);
            }
        }
        asgoreAnime.SetBool("isDamage", false);

    }

    void DumyDestroy()//더미 파괴용 메소드
    {
        GameObject DumyBye = GameObject.FindWithTag("Dumy");
        if (DumyBye != null)
        {
            Destroy(DumyBye);
        }
    }
}
