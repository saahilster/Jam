using UnityEngine;
using UnityEngine.Events;

public class AlterScript : MonoBehaviour
{
    [SerializeField] GameObject finCanvas;
    [SerializeField] private ItemEventManager iem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (iem.hasShovel != true)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("End invoked");
            finCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        finCanvas.SetActive(false);
    }
}
