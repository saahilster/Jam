using UnityEngine;
[System.Serializable]
public class Slots
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty => item == null;
    public void SetItem(ItemData newItem, int amount)
    {
        item = newItem;
        quantity = amount;
    }

    public void Clear()
    {
        item = null;
        quantity = 0;
    }
}
