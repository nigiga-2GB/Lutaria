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
    GameObject[] messageObj;    //�v���C���[�ɔ����`����I�u�W�F�N�g
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
        //�ȏI������
        endTime = notesManager.notesTime[notesManager.notesTime.Count - 1] + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMGR.instance.isStarted)
        {
            // �e�L�[���͎��ɁA�Ή����郌�[���̃m�[�c�̔��莞�ԂƂ̍��� musicAdjust �����������Čv�Z
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

            // �{���������ׂ����Ԃ���0.2�b�ȏ�x�ꂽ�ꍇ�̓~�X�Ƃ���imusicAdjust ���l���j
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

            // ���U���g�V�[���ւ̑J�ځimusicAdjust ���l���j
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
    [SerializeField] private GameObject[] messageObj;  // ����\���p�I�u�W�F�N�g�i0: Perfect, 1: Near, 2: Miss�j
    [SerializeField] private NotesManager notesManager;  // NotesManager �ɂ��e���X�g���Ǘ�
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timingText;
    [SerializeField] private GameObject finish;

    // ����^�C�~���O��臒l�i�b�j
    private const float PerfectThreshold = 0.033f;
    private const float NearThreshold = 0.080f;
    private const float MissThreshold = 0.200f;

    private float endTime = 0f;

    // �L�[�ƑΉ����郌�[���ԍ��̃}�b�s���O
    private Dictionary<KeyCode, int> keyToLane = new Dictionary<KeyCode, int> {
        { KeyCode.D, 0 },
        { KeyCode.F, 1 },
        { KeyCode.J, 2 },
        { KeyCode.K, 3 }
    };

    private void Start()
    {
        // �ȏI�����ԁF�Ō�̃m�[�c�̃^�C�~���O�{1�b
        if (notesManager.notesTime != null && notesManager.notesTime.Count > 0)
        {
            endTime = notesManager.notesTime[notesManager.notesTime.Count - 1] + 1f;
        }
    }

    private void Update()
    {
        if (!GameMGR.instance.isStarted)
            return;

        // �e�L�[���͂ɑ΂��鏈��
        foreach (var pair in keyToLane)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                HandleKeyPress(pair.Value);
            }
        }

        // �~�X����i���X�g�̓^�C�����ɕ���ł���O��j
        CheckMissedNotes();

        // �ȏI����A���U���g�V�[���ɑJ��
        if (Time.time > endTime + GameMGR.instance.startTime)
        {
            finish.SetActive(true);
            Invoke("LoadResultScene", 3f);
        }
    }

    /// <summary>
    /// �w�肳�ꂽ���[���̍ł������m�[�c�̃C���f�b�N�X��Ԃ�
    /// </summary>
    /// <param name="lane">�L�[�ɑΉ����郌�[���ԍ�</param>
    /// <returns>�Y���m�[�c�̃C���f�b�N�X�B���݂��Ȃ���� -1�B</returns>
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
    /// �w�肳�ꂽ���[���̃L�[���͂ɑ΂��Ĕ�������s����
    /// </summary>
    /// <param name="lane">�L�[�ɑΉ����郌�[���ԍ�</param>
    private void HandleKeyPress(int lane)
    {
        int noteIndex = FindNoteIndexInLane(lane);
        if (noteIndex != -1)
        {
            // ����␳�l�ijudgeAdjust�j���l�����Ċ��҃^�C�~���O���v�Z
            float expectedTime = notesManager.notesTime[noteIndex] + GameMGR.instance.startTime + SettingsManager.instance.judgeAdjust * 0.01f;
            float timeLag = Mathf.Abs(Time.time - expectedTime);
            timingText.text = (Time.time - expectedTime).ToString("F2");

            ProcessJudgement(timeLag, noteIndex);
        }
    }

    /// <summary>
    /// ����^�C�~���O�ɉ������������s���iPerfect / Near�j
    /// </summary>
    /// <param name="timeLag">���ۂ̃^�C�~���O�Ɗ��҃^�C�~���O�̂���</param>
    /// <param name="noteIndex">����Ώۂ̃m�[�c�C���f�b�N�X</param>
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
    /// �m�[�c�̃~�X�����擪�̃m�[�c���珇�Ƀ`�F�b�N����
    /// </summary>
    private void CheckMissedNotes()
    {
        // �m�[�c�̓^�C�����ɕ���ł���O��Ȃ̂ŁA�ŏ��̃m�[�c�݂̂��`�F�b�N
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
    /// �w�肳�ꂽ�C���f�b�N�X�̃m�[�c���폜���A�X�R�A�ƃR���{�̕\�����X�V����
    /// </summary>
    /// <param name="index">�폜����m�[�c�̃C���f�b�N�X</param>
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
    /// �X�R�A�v�Z�� UI �\���̍X�V
    /// </summary>
    private void UpdateScore()
    {
        GameMGR.instance.score = (int)Math.Round(1000000 * Math.Floor(GameMGR.instance.ratioScore / GameMGR.instance.maxScore * 1000000) / 1000000);
        comboText.text = GameMGR.instance.combo.ToString();
        scoreText.text = GameMGR.instance.score.ToString();
    }

    /// <summary>
    /// ����G�t�F�N�g���w�背�[���̈ʒu�Ő�������
    /// </summary>
    /// <param name="judgeType">0: Perfect, 1: Near, 2: Miss</param>
    /// <param name="lane">�Ώۃm�[�c�̃��[���ԍ�</param>
    private void JudgeEffect(int judgeType, int lane)
    {
        // �e���[���� X ���W�� (lane - 1.5f) �Ƃ��Čv�Z�i�K�v�ɉ����Ē����j
        Vector3 spawnPosition = new Vector3(lane - 1.5f, 0.76f, 0.05f);
        Instantiate(messageObj[judgeType], spawnPosition, Quaternion.Euler(40f, 0f, 0f));
    }

    private void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}