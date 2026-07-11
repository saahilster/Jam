using UnityEngine;

//Script is to add a journal entry to player's inventory
public class JournalTrigger : MonoBehaviour
{
    [SerializeField] private JournalSO entry; 
    [SerializeField] private JournalCollection manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Found");
            if(entry == null)
            {
                Debug.Log("Entry null");
                return;
            }
            manager.AddToCollection(entry);
            manager.PrintCollection();
        } 
    }
}
