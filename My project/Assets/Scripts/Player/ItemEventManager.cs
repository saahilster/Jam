using NUnit.Framework;
using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    public bool hasShovel = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shovel"))
        {
            hasShovel = true;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Rock" && hasShovel)
        {
            Debug.Log("worked");
            Destroy(other.gameObject);
        }
    }
}
