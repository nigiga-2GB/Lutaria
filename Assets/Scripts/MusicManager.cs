using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip clip;

    string songName;
    bool isPlaying = false;

    [SerializeField]
    SongDatabase database;

    float prepTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameMGR.instance.isStarted = false;
        songName = database.songDatas[GameMGR.instance.songId].songName;
        audioSource = GetComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("Musics/" + songName);
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isPlaying)
        {
            GameMGR.instance.isStarted = true;
            GameMGR.instance.startTime = Time.time;
            isPlaying = true;
            //audioSource.PlayOneShot(clip);
            audioSource.clip = clip;
            audioSource.PlayDelayed(prepTime + SettingsManager.instance.musicAdjust * 0.01f);
        }
    }
}
