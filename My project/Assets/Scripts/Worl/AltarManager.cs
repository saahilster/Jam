using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AltarManager : MonoBehaviour
{
    public UnityEvent EndGame;
    [SerializeField] List<AltarSlot> slots = new List<AltarSlot>{};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckWin();
    }

    public void CheckWin()
    {
        if (slots[0].currentSelected == 0 &&slots[1].currentSelected == 1  && slots[2].currentSelected == 2)
        {
            Debug.Log("Fin.");
            EndGame.Invoke();
        }
    }
}
