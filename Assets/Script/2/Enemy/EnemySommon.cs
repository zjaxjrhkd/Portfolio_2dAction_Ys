using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySommon : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3Dumy;
    public GameObject enemy3;
    public GameObject enemy4;
    public Animator endAni;


    private GameObject enemy;
    private GameObject Wall;

    public GameObject wallObject;

    public GameObject player;

    private int monsterLevel;
    private bool enemyLive;

    float currentTime;
    public float creattime;

    void Start()
    {
        monsterLevel = PlayerPrefs.GetInt("SelectLevel");
        enemyLive = false;
    }

    // Update is called once per frame
    void Update()
    {
        SummonEnemy();
    }
    public void EndAppear()
    {
        monsterLevel = 4;
    }

    public void SummonPlayer()
    {
        player.transform.position = new Vector3(0f, -1.3f, 0f);

    }
    void SummonEnemy()
    {
        currentTime += Time.deltaTime;
        if (currentTime > creattime)
        {
            if (monsterLevel == 1)
            {
                monsterLevel = 10;
                enemy = Instantiate(enemy1);
                Wall = Instantiate(wallObject);

                enemy.transform.position = new Vector3(0f, 2.5f, 0f);
                SummonPlayer();
                player.transform.position = new Vector3(0f, -1.3f, 0f);
                enemyLive = true;
            }
            else if (monsterLevel == 2)
            {
                monsterLevel = 10;
                enemy = Instantiate(enemy2);
                Wall = Instantiate(wallObject);

                enemy.transform.position = new Vector3(0f, 2.5f, 0f);
                SummonPlayer();
                enemyLive = true;
            }
            else if (monsterLevel == 3)
            {
                monsterLevel = 10;
                enemy = Instantiate(enemy3Dumy);
                Wall = Instantiate(wallObject);

                enemy.transform.position = new Vector3(0f, 2.5f, 0f);
                SummonPlayer();
                enemyLive = true;
            }
            else if (monsterLevel == 4)
            {
                monsterLevel = 10;
                enemy = Instantiate(enemy3);
                enemy.transform.position = new Vector3(0f, 2.5f, 0f);
                enemyLive = true;
            }
            else if (monsterLevel == 5)
            {
                monsterLevel = 10;
                enemy = Instantiate(enemy4);
                Wall = Instantiate(wallObject);

                enemy.transform.position = new Vector3(0f, 2.5f, 0f);
                SummonPlayer();
                enemyLive = true;
            }
        }
    }
    public void DeadEnemy()
    {
        endAni.SetBool("isEnd", true);
    }
}
