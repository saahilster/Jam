using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    public bool hasShovel = false;
    public bool hasMask = false;
    [SerializeField] private GameObject responseObj;
    [SerializeField] private TextMeshProUGUI response;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ActivateText(string say)
    {
        responseObj.SetActive(true);
        response.text = say;
        yield return new WaitForSeconds(4);
        response.text = null;
        responseObj.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shovel"))
        {
            hasShovel = true;
            Destroy(other.gameObject);
            StartCoroutine(ActivateText("Grabbed a shovel."));

        }
        else if (other.gameObject.tag == "Rock" && hasShovel)
        {
            Debug.Log("worked");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Mask"))
        {
            hasMask = true;
            Destroy(other.gameObject);
            StartCoroutine(ActivateText("Grabbed the mask"));
            Debug.Log("mask obtained.");
        }
    }
}
