using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HotbarSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private GameObject selectedItemHighlight;

    public void Refresh(Slots slot)
    {
        if (slot.IsEmpty)
        {
            iconImage.enabled = false;
            quantityText.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = slot.item.icon;

            quantityText.enabled = slot.quantity > 1;
            quantityText.text = slot.quantity.ToString();
        }
    }

    public void SetSelected(bool isSelected)
    {
        selectedItemHighlight.SetActive(isSelected);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
