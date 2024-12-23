using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Audio Data", order = 2)]
public class AudioData : ScriptableObject
{
    public List<Audio> audios;   
}

[System.Serializable]
public class Audio
{
    public AudioType AudioType;
    public AudioClip AudioClip;
}
