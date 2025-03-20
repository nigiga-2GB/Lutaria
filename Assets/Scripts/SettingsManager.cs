using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance = null;

    public float noteSpeed = 1.0f;
    //public float adjust = 0.0f;

    public float judgeAdjust = 0.0f;
    public float musicAdjust = 0.0f;

    public float laneAngle = 0f;

    public delegate void OnSettingsChangedDelegate();
    public event OnSettingsChangedDelegate OnSettingsChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetNoteSpeed(float speed)
    {
        noteSpeed = speed;
        OnSettingsChanged?.Invoke();
    }

    /*
    public void SetAdjustValue(float adjustValue)
    {
        adjust = adjustValue;
        OnSettingsChanged?.Invoke();
    }
    */
    public void SetJudgeAdjust(float adjustValue)
    {
        judgeAdjust = adjustValue;
        OnSettingsChanged?.Invoke();
    }

    public void SetMusicAdjust(float adjustValue)
    {
        musicAdjust = adjustValue;
        OnSettingsChanged?.Invoke();
    }

    public void SetLaneAngle(float angle)
    {
        laneAngle = angle;
        OnSettingsChanged?.Invoke();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("NoteSpeed", noteSpeed);
        PlayerPrefs.SetFloat("LaneAngle", laneAngle);
        PlayerPrefs.SetFloat("JudgeAdjust", judgeAdjust);
        PlayerPrefs.SetFloat("MusicAdjust", musicAdjust);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        noteSpeed = PlayerPrefs.GetFloat("NoteSpeed", 1.0f);
        laneAngle = PlayerPrefs.GetFloat("LaneAngle", 0f);
        judgeAdjust = PlayerPrefs.GetFloat("JudgeAdjust", 0.0f);
        musicAdjust = PlayerPrefs.GetFloat("MusicAdjust", 0.0f);
    }
}
