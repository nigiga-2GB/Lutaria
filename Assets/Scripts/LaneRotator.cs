using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(SettingsManager.instance.laneAngle, 0, 0);
    }
}
