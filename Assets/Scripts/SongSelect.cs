using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelect : MonoBehaviour
{
    [SerializeField]
    SongDatabase database;
    [SerializeField]
    TextMeshProUGUI[] songNameText;
    [SerializeField]
    TextMeshProUGUI[] songLevelText;
    [SerializeField]
    Image songImage;

    [SerializeField]
    Button settingsButton;
    [SerializeField]
    Button playButton;

    [SerializeField]
    Button titleButton;

    AudioSource audioSource;
    AudioClip audioClip;
    string songName;

    int nowSelect;

    // Start is called before the first frame update
    void Start()
    {
        nowSelect = 0;
        audioSource = GetComponent<AudioSource>();
        songName = database.songDatas[nowSelect].songName;
        UpdateAllSong();

        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        playButton.onClick.AddListener(OnPlayButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //nowSelect--;
            /*
            if(nowSelect < 0)
            {
                nowSelect = database.songDatas.Length - 1;
            }
            */
            nowSelect = (nowSelect + 1) % database.songDatas.Length;

            UpdateAllSong();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            nowSelect++;
            /*
            if(nowSelect > database.songDatas.Length - 1)
            {
                nowSelect = 0;
            }
            */
            nowSelect = (nowSelect + 4) % database.songDatas.Length;

            UpdateAllSong();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartSong();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SceneManager.LoadScene("SettingsScene");
        }
    }

    private void UpdateAllSong()
    {
        songName = database.songDatas[nowSelect % database.songDatas.Length].songName;
        audioClip = (AudioClip)Resources.Load("Musics/" + songName);
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
        for(int i = 0; i < 5; i++)
        {
            UpdateSong(i - 2);
        }
    }

    private void UpdateSong(int id)
    {
        try
        {
            int index = (nowSelect + id + database.songDatas.Length) % database.songDatas.Length;
            songNameText[id + 2].text = database.songDatas[index].songName;
            songLevelText[id + 2].text = database.songDatas[index].songLevel;
        }
        catch
        {
            songNameText[id + 2].text = "";
            songLevelText[id + 2].text = "";
        }
        
        if(id == 0)
        {
            songImage.sprite = database.songDatas[(nowSelect + id) % database.songDatas.Length].songImage;
        }
    }

    private void StartSong()
    {
        //int index = (nowSelect + database.songDatas.Length) % database.songDatas.Length;
        //GameMGR.instance.songId = index;
        GameMGR.instance.songId = nowSelect;
        SceneManager.LoadScene("PlayScene");
    }

    private void OnSettingsButtonPressed()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    private void OnPlayButtonPressed()
    {
        StartSong();
    }

    private void OnTitleButtonPressed()
    {
        SceneManager.LoadScene("Title");
    }
}
