using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<(ItemType, string), Item> items = new Dictionary<(ItemType, string), Item>();

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    public static void AddItem(Item item)
    {
        if(instance.items.ContainsKey((item.Type, item.ItemName)))
        {
            Debug.LogWarning($"Inventory has such item {item.ItemName}, {item.Type}");

            return;
        }

        instance.items.Add((item.Type, item.ItemName), item);
    }
    public static void DeleteItem(string itemName, ItemType type)
    {
        if (!instance.items.ContainsKey((type, itemName)))
        {
            Debug.LogWarning($"Inventory has no such item {itemName}, {type}");

            return;
        }

        instance.items.Remove((type, itemName));
    }  
    public static bool ContainsItem(string itemName, ItemType type)
    {
        if(instance.items.ContainsKey((type, itemName)))
        {
            return true;
        }

        return false;
    }
}
