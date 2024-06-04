using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{

    [SerializeField] private GameObject lootObjectPrefab;
    [SerializeField] private List<LootItem> lootItems; 


    public LootItem GetDropItems()
    {
        int randomPercent = Random.Range(1, 101);
        List<LootItem> dropableItem = new List<LootItem>();

        foreach(LootItem item in lootItems)
        {
            if(item.dropWeight <= randomPercent)
            {
                dropableItem.Add(item);
            }
        }
        if(dropableItem.Count > 0)
        {
            LootItem dropItem = dropableItem[Random.Range(0, dropableItem.Count)];
            return dropItem;
        }
        Debug.Log("This enemy dont have drop Item");
        return null;

    }

    public void InstantiateItem(Vector3 spawnPosition)
    {
        LootItem dropItem = GetDropItems();
        GameObject newItemDrop = Instantiate(lootObjectPrefab, spawnPosition, Quaternion.identity);
        newItemDrop.GetComponent<SpriteRenderer>().sprite = dropItem.itemSprite;
        newItemDrop.name = dropItem.itemName;
    }
}
