using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayRetryOrEnd : MonoBehaviour
{
    [SerializeField]
    Button retryButton;
    [SerializeField]
    Button endButton;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(OnRetryButtonPressed);
        endButton.onClick.AddListener(OnEndButtonPressed);
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

    private void OnEndButtonPressed()
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
}
