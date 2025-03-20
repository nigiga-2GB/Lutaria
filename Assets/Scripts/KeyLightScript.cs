using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLightScript : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;
    [SerializeField]
    int laneNum = -1;

    private Renderer rend;
    private float alfa = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!(rend.material.color.a <= 0))
        {
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
        }

        if (laneNum == 1)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                ChangeColor();
            }
        }
        if (laneNum == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ChangeColor();
            }
        }
        if (laneNum == 3)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ChangeColor();
            }
        }
        if (laneNum == 4)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ChangeColor();
            }
        }

        alfa -= speed * Time.deltaTime;
    }

    void ChangeColor()
    {
        alfa = 0.3f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
    }
}
