using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}


[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

public class NotesManager : MonoBehaviour
{
    //総ノーツ数
    public int noteNum;
    //曲名
    private string songName;

    public List<int> laneNum = new List<int>();
    public List<int> noteType = new List<int>();
    public List<float> notesTime = new List<float>();
    public List<GameObject> notesObj = new List<GameObject>();

    [SerializeField]
    float notesSpeed;   //ノーツスピード
    [SerializeField]
    GameObject noteObj;

    [SerializeField]
    SongDatabase database;

    //added judgeAdjustをSettingsManagerから取得する
    private float offset = 0f;

    //レーン用に追加したアア
    [SerializeField]
    Transform lanePivot;

    private void OnEnable()
    {
        if(SettingsManager.instance != null)
        {
            SettingsManager.instance.LoadSettings();
            //offset = SettingsManager.instance.adjust;
            //offset = SettingsManager.instance.judgeAdjust;
        }
        
        notesSpeed = GameMGR.instance.noteSpeed;
        noteNum = 0;
        songName = database.songDatas[GameMGR.instance.songId].songName;
        Load(songName);
    }

    private void Load(string songName)
    {
        string inputString = Resources.Load<TextAsset>(songName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;
        GameMGR.instance.maxScore = noteNum * 5;


        float prepTime = 2.0f;

        //ここから変えます
        /*
        for (int i = 0; i < noteNum; i++)
        {
            float duration = 60f / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = duration * (float)inputJson.notes[i].LPB;
            //float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.001f + prepTime;
            notesTime.Add(time);
            laneNum.Add(inputJson.notes[i].block);
            noteType.Add(inputJson.notes[i].type);

            //float z = notesTime[i] * notesSpeed;
            float z = notesTime[i] * notesSpeed + offset;
            notesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.005f, z), Quaternion.identity));
            Debug.Log(notesSpeed);
        }
        */
        for (int i = 0; i < noteNum; i++)
        {
            float duration = 60f / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = duration * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB)
                         + inputJson.offset * 0.001f + prepTime;
            notesTime.Add(time);
            laneNum.Add(inputJson.notes[i].block);
            noteType.Add(inputJson.notes[i].type);

            float z = notesTime[i] * notesSpeed + offset;
            // ローカル座標での発生位置を算出（例：laneは 0～3 として中央調整）
            Vector3 localPos = new Vector3(inputJson.notes[i].block - 1.5f, 0.005f, z);
            // lanePivot の回転を考慮して、ローカル座標をワールド座標に変換
            Vector3 spawnPos = lanePivot.TransformPoint(localPos);
            // lanePivot を親にすることで、今後もレーンの回転を引き継ぐ
            GameObject noteInstance = Instantiate(noteObj, spawnPos, Quaternion.identity, lanePivot);
            notesObj.Add(noteInstance);
            Debug.Log(notesSpeed);
        }
    }
}
