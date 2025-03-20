using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "Create song data")]

public class SongData : ScriptableObject
{
    [SerializeField]
    public string songId;
    [SerializeField]
    public string songName;
    [SerializeField]
    public string songLevel;
    [SerializeField]
    public Sprite songImage;
}