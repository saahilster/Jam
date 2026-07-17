using System.Collections;
using TMPro;
using UnityEngine;

//Script is to add a journal entry to player's inventory
public class JournalTrigger : MonoBehaviour
{


    [SerializeField] private GameObject uiNoti;

    [SerializeField] private JournalSO entry;
    [SerializeField] private JournalCollection manager;

    void Awake()
    {
        uiNoti.SetActive(false);
    }
    IEnumerator ActivationTime(float time, GameObject ui)
    {
        ui.SetActive(true);
        yield return new WaitForSeconds(time);
        ui.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Found");
            if (entry == null)
            {
                Debug.Log("Entry null");
                return;
            }
            manager.AddToCollection(entry);
            StartCoroutine(ActivationTime(2f, uiNoti));
            manager.GoToPage();
        }
    }
}
