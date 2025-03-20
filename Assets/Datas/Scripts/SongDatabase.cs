using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongDatabase", menuName = "Create song database")]

public class SongDatabase : ScriptableObject
{
    [SerializeField]
    public SongData[] songDatas;
}