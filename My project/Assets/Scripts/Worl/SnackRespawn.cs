using System.Collections;
using UnityEngine;

public class SnackRespawn : MonoBehaviour
{
    [SerializeField] private Collider col;
    [SerializeField] private Renderer ren;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    IEnumerator Respawn()
    {
        float spawnTime = Random.Range(4, 10);
        col.enabled = false;
        ren.enabled = false;
        Debug.Log(spawnTime);
        yield return new WaitForSeconds(spawnTime);
        col.enabled = true;
        ren.enabled = true;
        Debug.Log("Done");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Respawn());
        }
    }
}
