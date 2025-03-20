using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CursorManager : MonoBehaviour
{
    public List<GameObject> selects = new();
    public GameObject cursor;

    [SerializeField]
    AudioClip cursorMoveClip;
   
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            TitleMGR.Instance.nowCount--;
            if(TitleMGR.Instance.nowCount < 0)
            {   
                TitleMGR.Instance.nowCount = selects.Count - 1;
            }

            audioSource.Stop();
            audioSource.PlayOneShot(cursorMoveClip);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            TitleMGR.Instance.nowCount++;
            if(TitleMGR.Instance.nowCount > selects.Count - 1)
            {
                TitleMGR.Instance.nowCount = 0;
            }

            audioSource.Stop();
            audioSource.PlayOneShot(cursorMoveClip);
        }

        cursor.GetComponent<RectTransform>().localPosition = new Vector3(-82, selects[TitleMGR.Instance.nowCount].GetComponent<Transform>().localPosition.y + 8f, 0);
    }
}
