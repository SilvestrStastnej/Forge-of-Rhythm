using System;
using System.Collections.Generic;
using UnityEngine;

public enum HitObjectType {}

[Serializable]
public struct HitObjectData
{
    public HitObjectType type;
    public float timeInSong;
    public Vector2 position;
    public float duration;
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemData : ScriptableObject
{
    [Header("BasicInfo")]
    public string name;
    public GameObject model;

    [Header("Informace o skladbě")]
    public string songTitle;
    public string artist;
    public AudioClip audioClip;
    public float approachRate;

    [Header("Seznam not")]
    public List<HitObjectData> hitObjects = new List<HitObjectData>();
}