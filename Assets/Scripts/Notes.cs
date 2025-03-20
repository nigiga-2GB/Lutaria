using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private float noteSpeed = 10f;
    bool isStarted = false;

    private void Start()
    {
        noteSpeed = GameMGR.instance.noteSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isStarted = true;
        }

        if(isStarted)
        {
            transform.position -= transform.forward * noteSpeed * Time.deltaTime;
        }
    }
}
