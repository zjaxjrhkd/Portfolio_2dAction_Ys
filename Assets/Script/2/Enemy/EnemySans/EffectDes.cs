using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDes : MonoBehaviour
{
    public GameObject targetObject; // 삭제하려는 오브젝트의 참조
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
