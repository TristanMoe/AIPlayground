using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GInventory
{
    public List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject i)
    {
        items.Add(i);
    }

    public GameObject FindItemWithTag(string tag)
    {
        return items.FirstOrDefault(item => item.tag == tag); 
    }

    public void RemoveItem(GameObject i)
    {
        if (items.Contains(i))
            items.Remove(i);
    }
}
