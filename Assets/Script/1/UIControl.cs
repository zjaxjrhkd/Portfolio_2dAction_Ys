using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Animator levelMenu;
    public Animator option;

    public Animator sans;

    public bool easyClear;
    public bool nomalClear;
    public bool hardClear;

    public GameObject easyC;
    public GameObject nomalC;
    public GameObject hardC;

    public GameObject sound;
    public Slider volumeSlider;


    private void Start()
    {
        easyClear = PlayerPrefs.GetInt("EasyClear")==1;
        nomalClear = PlayerPrefs.GetInt("NomalClear")==1;
        hardClear = PlayerPrefs.GetInt("HardClear")==1;
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if(easyClear==true&& nomalClear == true && hardClear == true)
        {
            WaSans();
        }
        if (easyClear == true)
        {
            EasyClear();
        }
        if(nomalClear==true)
        {
            NomalClear();
        }
        if (hardClear == true)
        {
            HardlClear();
        }
    }

    // Update is called once per frame
    public void DataDelete()
    {
        easyClear = false;
        nomalClear = false;
        hardClear = false;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        easyC.SetActive(true);
        nomalC.SetActive(true);
        hardC.SetActive(true);

    }
    public void OnLevelMenu()
    {
        levelMenu.SetBool("isLevelMenu",true);
    }
    public void DownLevelMenu()
    {
        levelMenu.SetBool("isLevelMenu", false);
    }

    public void DownOption()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();

        option.SetBool("isOption", false);
    }
    public void OnOption()
    {
        option.SetBool("isOption", true);
    }

    public void SelectEasyLevel()
    {
        PlayerPrefs.SetInt("SelectLevel", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("2. Battle");
    }
    public void SelectNomalLevel()
    {
        PlayerPrefs.SetInt("SelectLevel", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene("2. Battle");

    }
    public void SelectHardLevel()
    {
        PlayerPrefs.SetInt("SelectLevel", 3);
        PlayerPrefs.Save();
        SceneManager.LoadScene("2. Battle");

    }

    public void SelectSansLevel()
    {
        PlayerPrefs.SetInt("SelectLevel", 5);
        PlayerPrefs.Save();
        SceneManager.LoadScene("2. Battle");
    }
    public void WaSans()
    {
        sans.SetBool("isGo", true);
    }
    public void EasyClear()
    {
        easyC.SetActive(false);
        easyClear = true;
    }
    public void NomalClear()
    {
        nomalC.SetActive(false);
        nomalClear = true;
    }
    public void HardlClear()
    {
        hardC.SetActive(false);
        hardClear = true;
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
