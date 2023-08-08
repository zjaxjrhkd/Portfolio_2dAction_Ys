using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Status : MonoBehaviour
{
    public int maxHealth;          // 몬스터 체력
    public int health;          // 몬스터 체력
    public Slider hpBar; // 조정할 슬라이더
    public Text hpText; // 변경할 HP 텍스트
    public GameObject gauge;
    public GameObject system;

    void Start()
    {
        if(gameObject.CompareTag("Player"))//플레이어의 체력 게이지 활성화
        {
            hpText.text = health.ToString() + "/" + maxHealth;
        }
    }

    // Update is called once per frame
    public void TakeDamage(int damage)//데미지 메소드
    {
        if (health > 0)
        {
            health -= damage;
            hpBar.value -= damage;
            hpText.text = health.ToString() + "/" + maxHealth;
            ViewGauge();
        }
        else if (health == 0) //캐릭터를 없애는 기능
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

    public void ViewGauge() //몬스터들의 게이지 활성화
    {
        if (gameObject.CompareTag("Wall") || gameObject.CompareTag("Papyrus")|| gameObject.CompareTag("Asgore")|| gameObject.CompareTag("Undyen")|| gameObject.CompareTag("Sans"))
        gauge.SetActive(true);
    }
    private void Enemywow() //벽이 부서졌을때 적들의 반응 구현
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



    void deadSign()//몬스터들이 죽었을때 기능 구현
    {
        system = GameObject.FindWithTag("System");
        EnemySommon deadCall = system.GetComponent<EnemySommon>();
        deadCall.DeadEnemy();
        Destroy(gameObject);
    }
}
