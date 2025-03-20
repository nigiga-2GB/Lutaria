using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMGR : MonoBehaviour
{
    public static GameMGR instance = null;

    public float maxScore;
    public float ratioScore;

    public int songId;
    public float noteSpeed;

    public bool isStarted;
    public float startTime;

    public int combo;
    public int score;

    public int perfect;
    public int near;
    public int miss;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
