using System;
using System.Collections.Generic;
using UnityEngine;
using static Song;

public enum HitObjectType {}


[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public GameObject model;
    public List<Song> songs = new List<Song>();
}