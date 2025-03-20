/*
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JudgeNotes : MonoBehaviour
{
    [SerializeField]
    GameObject[] messageObj;    //プレイヤーに判定を伝えるオブジェクト
    [SerializeField]
    NotesManager notesManager;

    [SerializeField]
    TextMeshProUGUI comboText;
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI timingText;

    [SerializeField]
    GameObject finish;

    private float endTime = 0f;

    private void Start()
    {
        //曲終了時間
        endTime = notesManager.notesTime[notesManager.notesTime.Count - 1] + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMGR.instance.isStarted)
        {
            // 各キー入力時に、対応するレーンのノーツの判定時間との差を musicAdjust 分も加味して計算
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (notesManager.laneNum[0] == 0)
                {
                    float expectedTime = notesManager.notesTime[0] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 0);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
                else if (notesManager.notesTime.Count > 1 && notesManager.laneNum[1] == 0)
                {
                    float expectedTime = notesManager.notesTime[1] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 1);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.laneNum[0] == 1)
                {
                    float expectedTime = notesManager.notesTime[0] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 0);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
                else if (notesManager.notesTime.Count > 1 && notesManager.laneNum[1] == 1)
                {
                    float expectedTime = notesManager.notesTime[1] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 1);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.laneNum[0] == 2)
                {
                    float expectedTime = notesManager.notesTime[0] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 0);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
                else if (notesManager.notesTime.Count > 1 && notesManager.laneNum[1] == 2)
                {
                    float expectedTime = notesManager.notesTime[1] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 1);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesManager.laneNum[0] == 3)
                {
                    float expectedTime = notesManager.notesTime[0] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 0);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
                else if (notesManager.notesTime.Count > 1 && notesManager.laneNum[1] == 3)
                {  
                    float expectedTime = notesManager.notesTime[1] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
                    Judgement(GetAbs(Time.time - expectedTime), 1);
                    timingText.text = (Time.time - expectedTime).ToString("F2");
                }
            }

            // 本来たたくべき時間から0.2秒以上遅れた場合はミスとする（musicAdjust を考慮）
            if (notesManager.notesTime.Count != 0)
            {
                if (Time.time > notesManager.notesTime[0] + 0.2f   + SettingsManager.instance.judgeAdjust * 0.01f  
                    + GameMGR.instance.startTime)
                {
                    JudgeEffect(2);
                    DeleteNotes(0);
                    GameMGR.instance.miss++;
                    GameMGR.instance.combo = 0;
                    Debug.Log("Miss");
                }
                else if (notesManager.notesTime.Count > 1
                    && Time.time > notesManager.notesTime[1] + 0.2f   + SettingsManager.instance.judgeAdjust * 0.01f
                    + GameMGR.instance.startTime)
                {
                    JudgeEffect(2);
                    DeleteNotes(0);
                    GameMGR.instance.miss++;
                    GameMGR.instance.combo = 0;
                    Debug.Log("Miss");
                }
            }

            // リザルトシーンへの遷移（musicAdjust を考慮）
            if (Time.time > endTime + GameMGR.instance.startTime)
            {
                finish.SetActive(true);
                Invoke("LoadResultScene", 3f);
                return;
            }
        }
    }

    private void Judgement(float timeLag, int offset)
    {
        if(timeLag <= 0.033)
        {
            JudgeEffect(0);
            DeleteNotes(offset);
            GameMGR.instance.ratioScore += 5;
            GameMGR.instance.perfect++;
            GameMGR.instance.combo++;
            Debug.Log("Perfect");
        }
        else if(timeLag <= 0.080)
        {
            JudgeEffect(1);
            DeleteNotes(offset);
            GameMGR.instance.ratioScore += 3;
            GameMGR.instance.near++;
            GameMGR.instance.combo++;
            Debug.Log("Near");
        }
    }

    private float GetAbs(float num)
    {
        if (num >= 0) return num;
        else return -num;
    }

    private void DeleteNotes(int offset)
    {
        notesManager.notesTime.RemoveAt(offset);
        notesManager.laneNum.RemoveAt(offset);
        notesManager.noteType.RemoveAt(offset);

        GameMGR.instance.score = (int)Math.Round(1000000 * Math.Floor(GameMGR.instance.ratioScore / GameMGR.instance.maxScore * 1000000) / 1000000);

        comboText.text = GameMGR.instance.combo.ToString();
        scoreText.text = GameMGR.instance.score.ToString();
    }

    private void JudgeEffect(int judge)
    {
        //Instantiate(messageObj[judge], new Vector3(notesManager.laneNum[0] - 1.5f, 0.76f, 0.05f), Quaternion.identity);
        Instantiate(messageObj[judge], new Vector3(notesManager.laneNum[0] - 1.5f, 0.76f, 0.05f), Quaternion.Euler(40f, 0f, 0f));
    }

    private void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
*/

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JudgeNotes : MonoBehaviour
{
    [SerializeField] private GameObject[] messageObj;  // 判定表示用オブジェクト（0: Perfect, 1: Near, 2: Miss）
    [SerializeField] private NotesManager notesManager;  // NotesManager により各リストを管理
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timingText;
    [SerializeField] private GameObject finish;

    // 判定タイミングの閾値（秒）
    private const float PerfectThreshold = 0.033f;
    private const float NearThreshold = 0.080f;
    private const float MissThreshold = 0.200f;

    private float endTime = 0f;

    // キーと対応するレーン番号のマッピング
    private Dictionary<KeyCode, int> keyToLane = new Dictionary<KeyCode, int> {
        { KeyCode.D, 0 },
        { KeyCode.F, 1 },
        { KeyCode.J, 2 },
        { KeyCode.K, 3 }
    };

    private void Start()
    {
        // 曲終了時間：最後のノーツのタイミング＋1秒
        if (notesManager.notesTime != null && notesManager.notesTime.Count > 0)
        {
            endTime = notesManager.notesTime[notesManager.notesTime.Count - 1] + 1f;
        }
    }

    private void Update()
    {
        if (!GameMGR.instance.isStarted)
            return;

        // 各キー入力に対する処理
        foreach (var pair in keyToLane)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                HandleKeyPress(pair.Value);
            }
        }

        // ミス判定（リストはタイム順に並んでいる前提）
        CheckMissedNotes();

        // 曲終了後、リザルトシーンに遷移
        if (Time.time > endTime + GameMGR.instance.startTime)
        {
            finish.SetActive(true);
            Invoke("LoadResultScene", 3f);
        }
    }

    /// <summary>
    /// 指定されたレーンの最も早いノーツのインデックスを返す
    /// </summary>
    /// <param name="lane">キーに対応するレーン番号</param>
    /// <returns>該当ノーツのインデックス。存在しなければ -1。</returns>
    private int FindNoteIndexInLane(int lane)
    {
        for (int i = 0; i < notesManager.laneNum.Count; i++)
        {
            if (notesManager.laneNum[i] == lane)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 指定されたレーンのキー入力に対して判定を実行する
    /// </summary>
    /// <param name="lane">キーに対応するレーン番号</param>
    private void HandleKeyPress(int lane)
    {
        int noteIndex = FindNoteIndexInLane(lane);
        if (noteIndex != -1)
        {
            // 判定補正値（judgeAdjust）を考慮して期待タイミングを計算
            float expectedTime = notesManager.notesTime[noteIndex] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
            float timeLag = Mathf.Abs(Time.time - expectedTime);
            timingText.text = (Time.time - expectedTime).ToString("F2");

            ProcessJudgement(timeLag, noteIndex);
        }
    }

    /// <summary>
    /// 判定タイミングに応じた処理を行う（Perfect / Near）
    /// </summary>
    /// <param name="timeLag">実際のタイミングと期待タイミングのずれ</param>
    /// <param name="noteIndex">判定対象のノーツインデックス</param>
    private void ProcessJudgement(float timeLag, int noteIndex)
    {
        if (timeLag <= PerfectThreshold)
        {
            JudgeEffect(0, notesManager.laneNum[noteIndex]);
            DeleteNoteAt(noteIndex);
            GameMGR.instance.ratioScore += 5;
            GameMGR.instance.perfect++;
            GameMGR.instance.combo++;
            Debug.Log("Perfect");
        }
        else if (timeLag <= NearThreshold)
        {
            JudgeEffect(1, notesManager.laneNum[noteIndex]);
            DeleteNoteAt(noteIndex);
            GameMGR.instance.ratioScore += 3;
            GameMGR.instance.near++;
            GameMGR.instance.combo++;
            Debug.Log("Near");
        }
    }

    /// <summary>
    /// ノーツのミス判定を先頭のノーツから順にチェックする
    /// </summary>
    private void CheckMissedNotes()
    {
        // ノーツはタイム順に並んでいる前提なので、最初のノーツのみをチェック
        while (notesManager.notesTime.Count > 0)
        {
            float noteTime = notesManager.notesTime[0] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
            if (Time.time > noteTime + MissThreshold)
            {
                JudgeEffect(2, notesManager.laneNum[0]);
                DeleteNoteAt(0);
                GameMGR.instance.miss++;
                GameMGR.instance.combo = 0;
                Debug.Log("Miss");
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// 指定されたインデックスのノーツを削除し、スコアとコンボの表示を更新する
    /// </summary>
    /// <param name="index">削除するノーツのインデックス</param>
    private void DeleteNoteAt(int index)
    {
        notesManager.notesTime.RemoveAt(index);
        notesManager.laneNum.RemoveAt(index);
        notesManager.noteType.RemoveAt(index);
        if (index < notesManager.notesObj.Count)
        {
            Destroy(notesManager.notesObj[index]);
            notesManager.notesObj.RemoveAt(index);
        }
        UpdateScore();
    }

    /// <summary>
    /// スコア計算と UI 表示の更新
    /// </summary>
    private void UpdateScore()
    {
        GameMGR.instance.score = (int)Math.Round(1000000 * Math.Floor(GameMGR.instance.ratioScore / GameMGR.instance.maxScore * 1000000) / 1000000);
        comboText.text = GameMGR.instance.combo.ToString();
        scoreText.text = GameMGR.instance.score.ToString();
    }

    /// <summary>
    /// 判定エフェクトを指定レーンの位置で生成する
    /// </summary>
    /// <param name="judgeType">0: Perfect, 1: Near, 2: Miss</param>
    /// <param name="lane">対象ノーツのレーン番号</param>
    private void JudgeEffect(int judgeType, int lane)
    {
        // 各レーンの X 座標は (lane - 1.5f) として計算（必要に応じて調整）
        Vector3 spawnPosition = new Vector3(lane - 1.5f, 0.76f, 0.05f);
        Instantiate(messageObj[judgeType], spawnPosition, Quaternion.Euler(40f, 0f, 0f));
    }

    private void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}