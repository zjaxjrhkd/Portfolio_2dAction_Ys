using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioClip[] songs; // 오디오 클립 배열로 노래 목록을 저장합니다.
    private AudioSource audioSource; // 오디오 소스 컴포넌트를 저장할 변수입니다.

    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옵니다.
        PlaySongByLevel(); // 레벨에 따라 노래를 재생합니다.
        SetVolume();
    }

    private void PlaySongByLevel()
    {
        int monsterLevel = PlayerPrefs.GetInt("SelectLevel");
        if(monsterLevel==5)
        {
            monsterLevel = 4;
        }
        if (monsterLevel >= 1 && monsterLevel <= songs.Length)
        {
            // 노래 재생
            audioSource.clip = songs[monsterLevel - 1]; // 선택한 레벨에 해당하는 오디오 클립을 가져옵니다.
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid monsterLevel!"); // 유효하지 않은 레벨에 대한 경고를 출력합니다.
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
