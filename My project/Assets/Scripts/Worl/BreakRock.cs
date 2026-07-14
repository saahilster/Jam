using UnityEngine;
using UnityEngine.Events;

public class BreakRock : MonoBehaviour
{
    public UnityEvent breakRock;
    [SerializeField] private ItemEventManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found player");
            Destroy(gameObject);
            breakRock.Invoke();
        }
    }

    public void DestroyRock(){
        
    }
}
