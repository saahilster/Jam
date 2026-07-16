using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class JournalCollection : MonoBehaviour
{
    public int currentIndex = -1;
    [SerializeField] public List<JournalSO> journalEntries = new List<JournalSO> {};
    [SerializeField] TextMeshProUGUI entryTitle;
    [SerializeField] TextMeshProUGUI entryBody;
    [SerializeField] TextMeshProUGUI entryCount;

    void FixedUpdate()
    {
        entryTitle.text = journalEntries[currentIndex].title;
        entryBody.text = journalEntries[currentIndex].entry;
        entryCount.text = journalEntries[currentIndex].number.ToString();
    }

    public void AddToCollection(JournalSO entry)
    {
        if (journalEntries.Count == 0)
        {
            journalEntries.Add(entry);
        }

        if (!journalEntries.Contains(entry))
        {
            journalEntries.Add(entry);
        }
    }
    public void PrintCollection()
    {
        foreach (JournalSO entry in journalEntries)
        {
            Debug.Log($"Entry: {entry.title}");
        }
    }

    public void IncrementEntry()
    {
        if (currentIndex < journalEntries.Count)
        {
            currentIndex++;
        }
        
    }
    public void DecrementEntry()
    {
        if(currentIndex > 0)
        {
            currentIndex--;
        }
        
    }

    public void GoToPage()
    {
        currentIndex = journalEntries.Count - 1;
    }
}
