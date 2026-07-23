using UnityEngine;

public class WallBreaker : MonoBehaviour
{
    [SerializeField] GameObject w1;
    [SerializeField] GameObject w2;
    [SerializeField] GameObject w3;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(w1);
            Destroy(w2);
            Destroy(w3);
        }
    }
}
