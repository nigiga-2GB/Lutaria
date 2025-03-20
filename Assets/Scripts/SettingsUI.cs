using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    Slider speedSlider;
    [SerializeField]
    TextMeshProUGUI speedText;

    /*
    [SerializeField]
    TextMeshProUGUI adjustText;
    [SerializeField]
    Button decrease;
    [SerializeField]
    Button increase;
    */
    [SerializeField]
    TextMeshProUGUI judgeAdjustText;
    [SerializeField]
    Button judgeDecrease;
    [SerializeField]
    Button judgeIncrease;

    /*
    [SerializeField]
    Slider volumeSlider;
    [SerializeField]
    TextMeshProUGUI volumeText;
    */
    [SerializeField]
    TextMeshProUGUI musicAdjustText;
    [SerializeField]
    Button musicDecrease;
    [SerializeField]
    Button musicIncrease;

    [SerializeField]
    Button saveButton;
    [SerializeField]
    Button backSceneButton;
    [SerializeField]
    Button resetButton;

    [SerializeField]
    Slider angleSlider;
    [SerializeField]
    TextMeshProUGUI angleText;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.instance.LoadSettings();
        speedSlider.value = SettingsManager.instance.noteSpeed;
        speedSlider.interactable = true;
        speedText.text = speedSlider.value.ToString("F1");
        //adjustText.text = SettingsManager.instance.adjust.ToString();
        judgeAdjustText.text = SettingsManager.instance.judgeAdjust.ToString();
        musicAdjustText.text = SettingsManager.instance.musicAdjust.ToString();
        angleSlider.value = SettingsManager.instance.laneAngle;
        angleSlider.interactable = true;
        angleText.text = angleSlider.value.ToString("F1");

        //イベントリスナー
        speedSlider.onValueChanged.AddListener(OnNoteSpeedChanged);
        //decrease.onClick.AddListener(OnDecreaseButtonPressed);
        //increase.onClick.AddListener(OnIncreaseButtonPressed);
        judgeDecrease.onClick.AddListener(OnJudgeDecrease);
        judgeIncrease.onClick.AddListener(OnJudgeIncrease);
        musicDecrease.onClick.AddListener(OnMusicDecrease);
        musicIncrease.onClick.AddListener(OnMusicIncrease);
        angleSlider.onValueChanged.AddListener(OnLaneAngleChanged);
        //

        backSceneButton.onClick.AddListener(OnBackSceneButtonPressed);
        saveButton.onClick.AddListener(OnSaveSettings);
        resetButton.onClick.AddListener(OnResetButtonPressed);

        audioSource = GetComponent<AudioSource>();
    }

    private void OnNoteSpeedChanged(float value)
    {
        SettingsManager.instance.SetNoteSpeed(value);
        speedText.text = value.ToString("F1");
        GameMGR.instance.noteSpeed = value;
    }

    /*
    private void OnDecreaseButtonPressed()
    {
        SettingsManager.instance.adjust -= 1.0f;
        adjustText.text = SettingsManager.instance.adjust.ToString();
    }

    private void OnIncreaseButtonPressed()
    {
        SettingsManager.instance.adjust += 1.0f;
        adjustText.text = SettingsManager.instance.adjust.ToString();
    }

    private void OnAdjustValueChanged(string value)
    {
        if(float.TryParse(value, out float offset))
        {
            SettingsManager.instance.SetAdjustValue(offset);
        }
    }
    */

    private void OnLaneAngleChanged(float value)
    {
        SettingsManager.instance.SetLaneAngle(value);
        angleText.text = value.ToString("F1");
    }

    private void OnJudgeDecrease()
    {
        SettingsManager.instance.SetJudgeAdjust(SettingsManager.instance.judgeAdjust - 1.0f);
        judgeAdjustText.text = SettingsManager.instance.judgeAdjust.ToString();
    }

    private void OnJudgeIncrease()
    {
        SettingsManager.instance.SetJudgeAdjust(SettingsManager.instance.judgeAdjust + 1.0f);
        judgeAdjustText.text = SettingsManager.instance.judgeAdjust.ToString();
    }

    private void OnMusicDecrease()
    {
        SettingsManager.instance.SetMusicAdjust(SettingsManager.instance.musicAdjust - 1.0f);
        musicAdjustText.text = SettingsManager.instance.musicAdjust.ToString();
    }

    private void OnMusicIncrease()
    {
        SettingsManager.instance.SetMusicAdjust(SettingsManager.instance.musicAdjust + 1.0f);
        musicAdjustText.text = SettingsManager.instance.musicAdjust.ToString();
    }

    private void OnBackSceneButtonPressed()
    {
        audioSource.Stop();
        SceneManager.LoadScene("MusicSelect");
    }

    private void OnResetButtonPressed()
    {
        SettingsManager.instance.SetNoteSpeed(7f);
        speedText.text = SettingsManager.instance.noteSpeed.ToString();
        speedSlider.value = 7f;
        SettingsManager.instance.SetLaneAngle(0f);
        angleText.text = SettingsManager.instance.laneAngle.ToString();
        angleSlider.value = 0f;
        SettingsManager.instance.SetMusicAdjust(0f);
        musicAdjustText.text = 0f.ToString();
        SettingsManager.instance.SetJudgeAdjust(0f);
        judgeAdjustText.text = 0f.ToString();
    }

    private void OnSaveSettings()
    {
        SettingsManager.instance.SaveSettings();
    }
}
