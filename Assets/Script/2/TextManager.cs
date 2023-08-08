using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; //��ȭ�� ������ �񼭳ʸ� ����
    public Text end;                    //������ ���� ��ũ��Ʈ�� ����
    private int level;                  //������ ���� ��ũ��Ʈ�� ���� ���� ����

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>(); //����
        level = PlayerPrefs.GetInt("SelectLevel"); //�������� �ҷ���
        EndMassage(level);//������ ���� ������ ��ũ��Ʈ ����
        GenerateData();//������ ���� ��ȭ ���� �޼ҵ� ȣ��
    }

    void GenerateData()
    {
        //��� ���� 
        talkData.Add(1, new string[] { "NYEH HEH HEH!'" });
        talkData.Add(2, new string[] { "En guarde!." });
        talkData.Add(3, new string[] { "Goodbye." });
        talkData.Add(5, new string[] { "let's just get to the point" });
        talkData.Add(6, new string[] { "have a terrible time" });
    }

    public string GetTalk(int monsterLevel) //Object�� id , string�迭�� index
    {
        return talkData[monsterLevel][0]; //GameMansger���� ���ϰ��� �޾Ƽ� ���
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
            end.text = "���� �μ���";
        }
        else if (level == 2)
        {
            end.text = "â�� �μ���";
        }
        else if (level == 3)
        {
            end.text = "���� ��";
        }
        else if (level == 5)
        {
            end.text = "������ �ð�";
        }
    }
}