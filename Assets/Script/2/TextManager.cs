using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; //대화를 저장할 딕서너리 선언
    public Text end;                    //레벨에 따른 스크립트를 저장
    private int level;                  //레벨에 따른 스크립트을 위한 변수 선언

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>(); //생성
        level = PlayerPrefs.GetInt("SelectLevel"); //레벨값을 불러옴
        EndMassage(level);//레벨에 따른 성공시 스크립트 생성
        GenerateData();//레벨에 따른 대화 생성 메소드 호출
    }

    void GenerateData()
    {
        //대사 생성 
        talkData.Add(1, new string[] { "NYEH HEH HEH!'" });
        talkData.Add(2, new string[] { "En guarde!." });
        talkData.Add(3, new string[] { "Goodbye." });
        talkData.Add(5, new string[] { "let's just get to the point" });
        talkData.Add(6, new string[] { "have a terrible time" });
    }

    public string GetTalk(int monsterLevel) //Object의 id , string배열의 index
    {
        return talkData[monsterLevel][0]; //GameMansger에서 리턴값을 받아서 출력
    }

    private void EndMassage(int level)
    {
        if(level==1)
        {
            end.text = "EasyClear!";
        }
        else if (level == 2)
        {
            end.text = "NomalClear!";
        }
        else if (level == 3)
        {
            end.text = "HardClear!";
        }
        else if (level == 5)
        {
            end.text = "Clear!";
        }
    }
    public void PlayerEnd()
    {
        if (level == 1)
        {
            end.text = "벽을 부수자";
        }
        else if (level == 2)
        {
            end.text = "창을 부수자";
        }
        else if (level == 3)
        {
            end.text = "붉은 불";
        }
        else if (level == 5)
        {
            end.text = "끔찍한 시간";
        }
    }
}