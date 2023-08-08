using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansStart : MonoBehaviour
{
    public GameObject startMenu;
    public void SansGo()
    {
        startMenu.SetActive(true);
    }
}
