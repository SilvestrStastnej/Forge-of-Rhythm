using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static Song;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterTypeName;
    public ItemData item;
    public Sprite model2D;
}
