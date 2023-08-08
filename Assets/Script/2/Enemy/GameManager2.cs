using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public TextManager talkManager;

    //대화창 
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
        Talk(monsterLevel);//talkData에 출력할 스크립트를 저장
        menuCount = 0;//옵션을 호출할때 호출 됬는지 체크용 변수 초기화
        Type();//출력 코루틴 실행, 텍스트이미지 오브젝트 활성화
    }

    private void Update()
    {
        MenuCall();
    }
    public void Type()
    {
        StartCoroutine(_typing());
        textMom.SetActive(true); //오브젝트 활성화
    }

    IEnumerator _typing()
    {
        for (int i = 0; i<= talkData.Length;i++)
        {
            UITalkText.text = talkData.Substring(0,i);//0~i번째 까지의 텍스트를 출력
            yield return new WaitForSeconds(0.15f);//0.15f의 텀을 둬서 한글자씩 출력하는 연출
        }
        yield return new WaitForSeconds(1f);//출력이 끝나고 1초 텀을 둠
        textMom.SetActive(false);//텍스트 오브젝트 비활성화
        StopCoroutine(_typing());//코루틴 종료
    }
    //실제 대사들을 UI에 출력하는 함수     
    public void Talk(int monsterLevel)
    {
        talkData = talkManager.GetTalk(monsterLevel);
    }
    public void ReturnMenu() //메뉴창에서 메인메뉴로 이동
    {

        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("1. Menu");
        
    }

    public void MenuCall() //전투화면에서 메뉴창을 호출
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