using System.Collections.Generic;
using UnityEngine;

public class JournalCollection : MonoBehaviour
{
    [SerializeField] List<JournalSO> journalEntries = new List<JournalSO> { };
    public void AddToCollection(JournalSO entry)
    {
        if (journalEntries.Count == 0)
        {
            journalEntries.Add(entry);
        }


        foreach (JournalSO page in journalEntries)
        {
            if (page == entry)
            {
                Debug.Log("Already entered");
                continue;
            }

            Debug.Log($"Added {entry.title}");
            return;
        }
        journalEntries.Add(entry);
    }
    public void PrintCollection()
    {
        foreach (JournalSO entry in journalEntries)
        {
            Debug.Log($"Entry: {entry.title}");
        }
    }
}
