using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class JournalCollection : MonoBehaviour
{
    public int currentIndex = 0;
    [SerializeField] List<JournalSO> journalEntries = new List<JournalSO> {};
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
        currentIndex++;
    }
    public void DecrementEntry()
    {
        currentIndex--;
    }
}
