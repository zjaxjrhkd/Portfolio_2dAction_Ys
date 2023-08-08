using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public TextManager talkManager;

    //��ȭâ 
    //   public GameObject talkPanel;
    public GameObject textMom;

    public Text UITalkText;
    private int monsterLevel;
    private string talkData;
    private int phase;
    private int menuCount;
    public Slider volumeSlider;

    public Transform option;

    private void Start()
    {
        monsterLevel = PlayerPrefs.GetInt("SelectLevel");
        Talk(monsterLevel);//talkData�� ����� ��ũ��Ʈ�� ����
        menuCount = 0;//�ɼ��� ȣ���Ҷ� ȣ�� ����� üũ�� ���� �ʱ�ȭ
        Type();//��� �ڷ�ƾ ����, �ؽ�Ʈ�̹��� ������Ʈ Ȱ��ȭ
    }

    private void Update()
    {
        MenuCall();
    }
    public void Type()
    {
        StartCoroutine(_typing());
        textMom.SetActive(true); //������Ʈ Ȱ��ȭ
    }

    IEnumerator _typing()
    {
        for (int i = 0; i<= talkData.Length;i++)
        {
            UITalkText.text = talkData.Substring(0,i);//0~i��° ������ �ؽ�Ʈ�� ���
            yield return new WaitForSeconds(0.15f);//0.15f�� ���� �ּ� �ѱ��ھ� ����ϴ� ����
        }
        yield return new WaitForSeconds(1f);//����� ������ 1�� ���� ��
        textMom.SetActive(false);//�ؽ�Ʈ ������Ʈ ��Ȱ��ȭ
        StopCoroutine(_typing());//�ڷ�ƾ ����
    }
    //���� ������ UI�� ����ϴ� �Լ�     
    public void Talk(int monsterLevel)
    {
        talkData = talkManager.GetTalk(monsterLevel);
    }
    public void ReturnMenu() //�޴�â���� ���θ޴��� �̵�
    {

        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("1. Menu");
        
    }

    public void MenuCall() //����ȭ�鿡�� �޴�â�� ȣ��
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            option.transform.position = new Vector2(1000f, 600f);

            Time.timeScale = 0f;
            menuCount++;
        }

    }
    public void MenuOut()
    {
        Time.timeScale = 1f;
        option.transform.position = new Vector2(2500f, 2500f);
    }
    public void DownVolume()
    {
        volumeSlider.value -= 0.1f;
    }
    public void UpVolume()
    {
        volumeSlider.value += 0.1f;
    }

}