using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private float rotationThreshhold = 20f;
    [SerializeField] private Transform target;
    private float lastYRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        Vector3 direction = Camera.main.transform.position - transform.position;
        direction.y = 0f;
        float currentYRoation = Quaternion.LookRotation(direction).eulerAngles.y;
        float delta = Mathf.DeltaAngle(lastYRotation, currentYRoation);
        if (Mathf.Abs(delta) >= rotationThreshhold)
        {
            transform.eulerAngles = new Vector3(0f, currentYRoation, 0f);
            lastYRotation = currentYRoation;
        }
    }
    void LateUpdate()
    {
        
    }
}
