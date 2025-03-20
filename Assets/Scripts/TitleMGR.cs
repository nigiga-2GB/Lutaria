using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMGR : MonoBehaviour
{
    public static TitleMGR Instance = null;

    public int nowCount = 0;

    // Update is called once per frame
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //Exitなら終了
            if (nowCount == 1)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
                Application.Quit();//ゲームプレイ終了
#endif
            }
            else if (nowCount == 0)
            {
                SceneManager.LoadScene("MusicSelect");
            }
        }        
    }
}
