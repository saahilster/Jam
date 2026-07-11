using UnityEngine;

public class HotBar : MonoBehaviour
{
    [SerializeField]
    private HotbarSlotUI[] slotUIs;
    private Slots[] slots;
    private int selectedItemIndex = 0;

    public void Awake()
    {
        slotUIs = GetComponentsInChildren<HotbarSlotUI>();
    }
    
    private void AddItem(ItemData item, int amount, int slotIndex)
    {
        slots[slotIndex].SetItem(item,amount);
        
    }

    private void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slotUIs[i].Refresh(slots[i]);
            slotUIs[i].SetSelected(i == selectedItemIndex);
        }
    }

    public void SelectSlot(int index)
    {
        selectedItemIndex = index;
        RefreshUI();
    }
}
