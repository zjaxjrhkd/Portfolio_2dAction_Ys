using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuSound : MonoBehaviour
{
    public AudioClip[] songs; // 오디오 클립 배열로 노래 목록을 저장합니다.
    private AudioSource audioSource; // 오디오 소스 컴포넌트를 저장할 변수입니다.
    private int easy;
    private int nomal;
    private int hard;
    public Slider volumeSlider;
    private float volumeValue;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옵니다.
        easy = PlayerPrefs.GetInt("EasyClear");
        nomal = PlayerPrefs.GetInt("NomalClear");
        hard = PlayerPrefs.GetInt("HardClear");
        SetVolume();
        PlaySongByLevel(); // 레벨에 따라 노래를 재생합니다.
    }
    private void Update()
    {

    }
    private void PlaySongByLevel()
    {
        
        if(easy==1&&nomal==1&&hard==1)
        {
            audioSource.clip = songs[1]; // 선택한 레벨에 해당하는 오디오 클립을 가져옵니다.
            audioSource.Play();
        }
        else
        {
            audioSource.clip = songs[0]; // 선택한 레벨에 해당하는 오디오 클립을 가져옵니다.
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
