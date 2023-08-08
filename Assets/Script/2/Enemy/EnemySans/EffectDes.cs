using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDes : MonoBehaviour
{
    public GameObject targetObject; // �����Ϸ��� ������Ʈ�� ����
    private Animator hand;

    private void Start()
    {
        hand = GetComponent<Animator>();
    }

    public void DeleteObjects()
    {
        Destroy(targetObject);
    }

    public void HandDown()
    {
        hand.SetBool("moveHandDown", true);
    }
    public void HandUp()
    {
        hand.SetBool("moveHandDown", false);
    }


}
