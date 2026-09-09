using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AltarSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slot;
    [SerializeField] List<string> strings = new List<string>{"mem", "ento", "mori"};
    public int currentSelected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slot.text = strings[currentSelected];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrent()
    {
        Debug.Log(currentSelected);
        if(currentSelected < 2)
        {
            currentSelected++;
            slot.text = strings[currentSelected];
        }
        else if(currentSelected >= 2)
        {
            currentSelected = 0;
            slot.text = strings[currentSelected];
        }
    }
}
