using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuSound : MonoBehaviour
{
    public AudioClip[] songs; // ����� Ŭ�� �迭�� �뷡 ����� �����մϴ�.
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ�� ������ �����Դϴ�.
    private int easy;
    private int nomal;
    private int hard;
    public Slider volumeSlider;
    private float volumeValue;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� �����ɴϴ�.
        easy = PlayerPrefs.GetInt("EasyClear");
        nomal = PlayerPrefs.GetInt("NomalClear");
        hard = PlayerPrefs.GetInt("HardClear");
        SetVolume();
        PlaySongByLevel(); // ������ ���� �뷡�� ����մϴ�.
    }
    private void Update()
    {

    }
    private void PlaySongByLevel()
    {
        
        if(easy==1&&nomal==1&&hard==1)
        {
            audioSource.clip = songs[1]; // ������ ������ �ش��ϴ� ����� Ŭ���� �����ɴϴ�.
            audioSource.Play();
        }
        else
        {
            audioSource.clip = songs[0]; // ������ ������ �ش��ϴ� ����� Ŭ���� �����ɴϴ�.
            audioSource.Play();
        }
    }
    public void SetVolume()
    {
        if (PlayerPrefs.GetFloat("volume") > 0.1f)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume"); 
        }
        else
        {
            volumeSlider.value = 0.8f;
        }
    }
}
