using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioClip[] songs; // ����� Ŭ�� �迭�� �뷡 ����� �����մϴ�.
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ�� ������ �����Դϴ�.

    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� �����ɴϴ�.
        PlaySongByLevel(); // ������ ���� �뷡�� ����մϴ�.
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
            // �뷡 ���
            audioSource.clip = songs[monsterLevel - 1]; // ������ ������ �ش��ϴ� ����� Ŭ���� �����ɴϴ�.
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid monsterLevel!"); // ��ȿ���� ���� ������ ���� ��� ����մϴ�.
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
