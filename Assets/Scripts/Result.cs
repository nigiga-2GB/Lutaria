using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI perfectText;
    [SerializeField]
    TextMeshProUGUI nearText;
    [SerializeField]
    TextMeshProUGUI missText;

    [SerializeField]
    TextMeshProUGUI scoreGradeText;
    [SerializeField]
    Image songImage;
    [SerializeField]
    TextMeshProUGUI songNameText;

    [SerializeField]
    SongDatabase database;

    [SerializeField]
    Button retryButton;
    [SerializeField]
    Button songSelectButton;
    [SerializeField]
    Button titleButton;
    [SerializeField]
    TextMeshProUGUI specialText;

    private void OnEnable()
    {
        scoreText.text = GameMGR.instance.score.ToString();
        perfectText.text = GameMGR.instance.perfect.ToString();
        nearText.text = GameMGR.instance.near.ToString();
        missText.text = GameMGR.instance.miss.ToString();

        if(GameMGR.instance.score > 990000)
        {
            scoreGradeText.text = "S";
            scoreGradeText.color = new Color32(0xff, 0xd7, 0x00, 0xff);
            scoreGradeText.fontSize = 164;
        }
        else if(GameMGR.instance.score > 980000)
        {
            scoreGradeText.text = "AAA+";
            scoreGradeText.color = new Color32(0xff, 0x45, 0x00, 0xff);
            scoreGradeText.fontSize = 86;
        }
        else if(GameMGR.instance.score > 970000)
        {
            scoreGradeText.text = "AAA";
            scoreGradeText.color = new Color32(0xff, 0x63, 0x47, 0xff);
            scoreGradeText.fontSize = 100;
        }
        else if(GameMGR.instance.score > 950000)
        {
            scoreGradeText.text = "AA+";
            scoreGradeText.color = new Color32(0xff, 0x8c, 0x00, 0xff);
            scoreGradeText.fontSize = 100;

        }
        else if(GameMGR.instance.score > 930000)
        {
            scoreGradeText.text = "AA";
            scoreGradeText.color = new Color32(0xff, 0xa5, 0x00, 0xff);
            scoreGradeText.fontSize = 164;
        }
        else if(GameMGR.instance.score > 900000)
        {
            scoreGradeText.text = "A+";
            scoreGradeText.color = new Color32(0x2e, 0x8b, 0x57, 0xff);
            scoreGradeText.fontSize = 164;
        }
        else if(GameMGR.instance.score > 870000)
        {
            scoreGradeText.text = "A";
            scoreGradeText.color = new Color32(0x3c, 0xb3, 0x71, 0xff);
            scoreGradeText.fontSize = 164;
        }
        else if(GameMGR.instance.score > 750000)
        {
            scoreGradeText.text = "B";
            scoreGradeText.color = new Color32(0x1e, 0x90, 0xff, 0xff);
            scoreGradeText.fontSize = 164;
        }
        else if(GameMGR.instance.score > 650000)
        {
            scoreGradeText.text = "C";
            scoreGradeText.color = new Color32(0x8a, 0x2b, 0xe2, 0xff);
            scoreGradeText.fontSize = 164;
        }
        else
        {
            scoreGradeText.text = "D";
            scoreGradeText.color = new Color32(0xff, 0x69, 0xb4, 0xff);
            scoreGradeText.fontSize = 164;
        }

        if(GameMGR.instance.miss == 0)
        {
            if(GameMGR.instance.near == 0)
            {
                specialText.text = "All Prefect";
                specialText.color = new Color32(0xff, 0xd7, 0x00, 0xff);
            }
            else
            {
                specialText.text = "FullCombo";
                specialText.color = new Color32(0xff, 0x45, 0x00, 0xff);
            }
        }

        songImage.sprite = database.songDatas[GameMGR.instance.songId].songImage;
        songNameText.text = database.songDatas[GameMGR.instance.songId].songName;

        retryButton.onClick.AddListener(OnRetryButtonPressed);
        songSelectButton.onClick.AddListener(OnSongSelectButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRetryButtonPressed()
    {
        GameMGR.instance.combo = 0;
        GameMGR.instance.miss = 0;
        GameMGR.instance.near = 0;
        GameMGR.instance.perfect = 0;
        GameMGR.instance.score = 0;
        GameMGR.instance.isStarted = false;
        GameMGR.instance.ratioScore = 0;

        SceneManager.LoadScene("PlayScene");
    }

    private void OnSongSelectButtonPressed()
    {
        GameMGR.instance.combo = 0;
        GameMGR.instance.miss = 0;
        GameMGR.instance.near = 0;
        GameMGR.instance.perfect = 0;
        GameMGR.instance.score = 0;
        GameMGR.instance.isStarted = false;
        GameMGR.instance.ratioScore = 0;

        SceneManager.LoadScene("MusicSelect");
    }

    private void OnTitleButtonPressed()
    {
        GameMGR.instance.combo = 0;
        GameMGR.instance.miss = 0;
        GameMGR.instance.near = 0;
        GameMGR.instance.perfect = 0;
        GameMGR.instance.score = 0;
        GameMGR.instance.isStarted = false;
        GameMGR.instance.ratioScore = 0;

        SceneManager.LoadScene("Title");
    }
}
