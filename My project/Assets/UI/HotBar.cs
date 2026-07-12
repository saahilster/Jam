using System;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    private HotbarSlotUI[] slotUIs;
    private Slots[] slots;
    private InputSystem_Actions input;
    private int selectedItemIndex = 0;
    [SerializeField] private ItemData testItem;

    private void Start()
    {
        AddItem(testItem, 1, 0);
        RefreshUI();
    }
    public void Awake()
    {
        input = new InputSystem_Actions();
        slotUIs = GetComponentsInChildren<HotbarSlotUI>();
        slots = new Slots[slotUIs.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new Slots();
        }
        RefreshUI();
    }
    
    private void OnEnable()
    {
        input.Enable();
        input.UI.Slot1.performed += ctx => SelectSlot(0);
        input.UI.Slot2.performed += ctx => SelectSlot(1);
        input.UI.Slot3.performed += ctx => SelectSlot(2);
        input.UI.Slot4.performed += ctx => SelectSlot(3);
        input.UI.Slot5.performed += ctx => SelectSlot(4);
    
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
