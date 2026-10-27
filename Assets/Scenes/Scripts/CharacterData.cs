using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterTypeName;
    public ScriptableObject item;
    public Sprite model2D;
}
