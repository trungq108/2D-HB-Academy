using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Loot System/Item")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public float dropWeight;
}
