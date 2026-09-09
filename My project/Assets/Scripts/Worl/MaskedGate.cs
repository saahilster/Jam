using System.Collections;
using TMPro;
using UnityEngine;

public class MaskedGate : MonoBehaviour
{
    [SerializeField] ItemEventManager iem;
    [SerializeField] GameObject responseObj;
    [SerializeField] TextMeshProUGUI response;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator ActivateText(string say)
    {
        responseObj.SetActive(true);
        response.text = say;
        yield return new WaitForSeconds(4);
        response.text = null;
        responseObj.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (iem.hasMask)
            {
                StartCoroutine(ActivateText("tread with caution fool."));
                Destroy(gameObject);
            }
        }
    }


}
