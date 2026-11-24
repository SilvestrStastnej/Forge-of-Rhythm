using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Scriptable Objects/Song")]
public class Song : ScriptableObject
{

    [Serializable] public struct HitObjectData
    {
        public enum HitObjectType
        {
            Circle
        };
        public float timeInSong;
        public float ApproachRate;
        public Vector2 position;
    }

    [Header("Informace o skladbě")]
    public string songTitle;
    public string artist;
    public AudioClip audioClip;

    [Header("Seznam not")]
    public List<HitObjectData> hitObjects = new List<HitObjectData>();
}
