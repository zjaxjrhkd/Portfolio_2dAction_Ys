using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Status : MonoBehaviour
{
    public int maxHealth;          // ���� ü��
    public int health;          // ���� ü��
    public Slider hpBar; // ������ �����̴�
    public Text hpText; // ������ HP �ؽ�Ʈ
    public GameObject gauge;
    public GameObject system;

    void Start()
    {
        if(gameObject.CompareTag("Player"))//�÷��̾��� ü�� ������ Ȱ��ȭ
        {
            hpText.text = health.ToString() + "/" + maxHealth;
        }
    }

    // Update is called once per frame
    public void TakeDamage(int damage)//������ �޼ҵ�
    {
        if (health > 0)
        {
            health -= damage;
            hpBar.value -= damage;
            hpText.text = health.ToString() + "/" + maxHealth;
            ViewGauge();
        }
        else if (health == 0) //ĳ���͸� ���ִ� ���
        {
            if (gameObject.CompareTag("Papyrus"))
            {
                PlayerPrefs.SetInt("EasyClear", 1);
                PlayerPrefs.Save();

                deadSign();
            }
            else if(gameObject.CompareTag("Undyen"))
            {
                PlayerPrefs.SetInt("NomalClear", 1);
                PlayerPrefs.Save();
                deadSign();
            }
            else if(gameObject.CompareTag("Asgore"))
            {
                PlayerPrefs.SetInt("HardClear", 1);
                PlayerPrefs.Save();
                deadSign();
            }
            else if (gameObject.CompareTag("Sans"))
            {
                PlayerPrefs.SetInt("SansClear", 1);
                PlayerPrefs.Save();
                deadSign();
            }
            else if(gameObject.CompareTag("Wall"))
            {
                Enemywow();
            }
            else if (gameObject.CompareTag("Player"))
            {
                GameObject textPlayer = GameObject.FindWithTag("System");
                if (textPlayer != null)
                {
                    TextManager playerDeadText = textPlayer.GetComponent<TextManager>();
                    if (playerDeadText != null)
                    {
                        playerDeadText.PlayerEnd();
                        deadSign();
                        Destroy(gameObject);
                    }
                }
            }

                Destroy(gameObject);
        }
    }

    public void ViewGauge() //���͵��� ������ Ȱ��ȭ
    {
        if (gameObject.CompareTag("Wall") || gameObject.CompareTag("Papyrus")|| gameObject.CompareTag("Asgore")|| gameObject.CompareTag("Undyen")|| gameObject.CompareTag("Sans"))
        gauge.SetActive(true);
    }
    private void Enemywow() //���� �μ������� ������ ���� ����
    {

        GameObject enemyObject = GameObject.FindWithTag("Papyrus");
        if (enemyObject != null)
        {
            EnemyPapyrus enemyPapyrusComponent = enemyObject.GetComponent<EnemyPapyrus>();
            if (enemyPapyrusComponent != null)
            {
                enemyPapyrusComponent.PapyWowWow();
            }
        }
        enemyObject = GameObject.FindWithTag("Asgore");
        if (enemyObject != null)
        {
            EnemyAsgore enemyPapyrusComponent = enemyObject.GetComponent<EnemyAsgore>();
            if (enemyPapyrusComponent != null)
            {
                enemyPapyrusComponent.AsgoreWowWow();
            }
        }

        enemyObject = GameObject.FindWithTag("Undyen");
        if (enemyObject != null)
        {
            EnemyUndyne enemyPapyrusComponent = enemyObject.GetComponent<EnemyUndyne>();
            if (enemyPapyrusComponent != null)
            {
                enemyPapyrusComponent.UndyenWowWow();
            }
        }
        enemyObject = GameObject.FindWithTag("Sans");
        if (enemyObject != null)
        {
            EnemySans enemyPapyrusComponent = enemyObject.GetComponent<EnemySans>();
            if (enemyPapyrusComponent != null)
            {
                enemyPapyrusComponent.SansWowWow();
            }
        }
    }



    void deadSign()//���͵��� �׾����� ��� ����
    {
        system = GameObject.FindWithTag("System");
        EnemySommon deadCall = system.GetComponent<EnemySommon>();
        deadCall.DeadEnemy();
        Destroy(gameObject);
    }
}
