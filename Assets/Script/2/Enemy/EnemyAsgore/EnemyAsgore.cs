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

    private void Update()//������ ���� ����
    {
        if (target != null)
        {
            Vector3 playerPosition = target.position;
        }
        phaseTime += Time.deltaTime;//�ð��� ������ ���� �������� ��ų ���
        if (enemyPhase == 1)//���� ��ȭ�� ���� ���� ���
        {
            EnemySkillHand();//��ų ���
        }
        if (enemyPhase == 2)//���� �μ����� 2������� ����
        {
            if (phaseTime < 10)//10�ʵ��ȸ� ����ϰ� ���̰� ��ų�� ����ϰ� �����
            {
                EnemySkillRain();
            }
            if (!dumyCheack)
            {
                EnemySkillRun();//���̰� ��ų�� ����� ���� �����
            }
            if (phaseTime > nextPhaseTime)//�����ð��� ������ ���� ��ȯ
            {
                enemyPhase++;
                DumyDestroy();
                asgoreAnime.SetBool("isRun", false);//��ü�� �����ڸ��� ���ƿ�

                phaseTime = 0;
            }
        }
        if (enemyPhase == 3)
        {
            EnemySkillFireOrb();//��ų���
            if (phaseTime > nextPhaseTime)
            {
                enemyPhase++;
                phaseTime = 0;
            }
        }
    }

    private void EnemySkillHand()//�÷��̾� ĳ���ͷ� ���� 240��, 40�� �������� �Ÿ��� 3������ ���� ���� ��ġ�ϰ� �ʹ�.
    {
        objTime += Time.deltaTime;
        if (objTime > 6)
        {
            GameObject instantiatedObj = Instantiate(obj); //���ӿ�����Ʈ ������ ������ ������Ʈ���� �Է�
            Vector3 newPosition = CalculatePosition(target.position,
                target.forward, 240f, 3f); //Ÿ��(�÷��̾�), 
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
        //a�� b��  �Ÿ� ���
        float distanceAB = Vector3.Distance(basePosition, forward);//�����ǰ��� ������ ����

        // a���� b�� ���ϴ� ���� ���
        Vector3 directionAB = (forward - basePosition).normalized;//�Ÿ� ���� ����� ���� ���, �Ҽ����� ���� �־ nomalized����
        
        //�־��� ����(angleDegrees)�� ���� ȸ�� ���ʹϿ�(rotation)�� ����մϴ�. ���⼭�� Z���� �������� �־��� ������ŭ ȸ����Ű�� ���� Euler ��(0, 0, -angleDegrees)�� ����մϴ�.
        Quaternion rotation = Quaternion.Euler(0f, 0f, -angleDegrees);

        Vector3 rotatedDirection = rotation * directionAB;//������ ����� ���ؼ� ���Ͱ����� ��ȯ ����*�Ÿ�

        // ȸ���� ������ ũ�⸦ 5�� �����Ͽ� ���� ����
        Vector3 adjustedDirection = rotatedDirection * distance;//���� ���ϴ� �Ÿ��� �߰�

        // a�� ��ġ�� ������ ���͸� ���Ͽ� �������� ��ǥ ���
        Vector3 newPosition = basePosition + adjustedDirection;//Ÿ���� ��ġ+���� ��ġ

        return newPosition;//���� �� ����, ���ݻ����غ��� �Ÿ��� ���� �����ϰ� forward�������� rotation�� +���൵ �� �� ����.
    }

    private void EnemySkillRain()
    {
        obj2Time += Time.deltaTime;//�ش� ��ų�� ������ �󸶳� ����� ������ ����
        int random = Random.Range(0, 5);//�������� 0~4���� �Է�


        if (obj2Time > 0.02)//��ų�� ������� 0.02�ʰ� ������ ��ų ���
        {

            if (random == 0)// 5���� ��ġ�� ����
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

    private void EnemySkillRun()//�� ������ ����ġ�� ���̸� ������ ���ݽ�Ų��.
    {

        asgoreAnime.SetBool("isRun", true);
        GameObject Dumy2 = Instantiate(dumy);
        Dumy2.transform.position = new Vector3(0.0f, 2.83f, 0.0f);
        dumyCheack = true;

    }

    private void EnemySkillFireOrb()//rain�� ���������� �����迭�� ���� �ڵ� ����ȭ �õ���
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



    public void AsgoreWowWow()//���� �μ����� ����Ǵ� �޼ҵ�
    {
        asgoreAnime.SetBool("isDamage", true);
        foreach (Transform child in transform)//�ڽĿ�����Ʈ �߿��� ���� ������Ʈ�� Ȱ��ȭ
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
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Fire");//���� �μ����� �� ������Ʈ�� �� �ı���Ŵ

            foreach (GameObject fire in objectsWithTag)
            {
                Destroy(fire);
            }
        }
    }

    void TakeDamageAnimeEnd()//�ִϸ��̼ǿ��� �ִϸ��̼� ��������� ���� �޼ҵ�
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

    void DumyDestroy()//���� �ı��� �޼ҵ�
    {
        GameObject DumyBye = GameObject.FindWithTag("Dumy");
        if (DumyBye != null)
        {
            Destroy(DumyBye);
        }
    }
}
