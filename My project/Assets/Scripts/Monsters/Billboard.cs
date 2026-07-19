using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private float rotationThreshhold = 20f;
    private float lastYRotation;
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;
        direction.y = 0f;
        float currentYRoation = Quaternion.LookRotation(direction).eulerAngles.y;
        float delta = Mathf.DeltaAngle(lastYRotation, currentYRoation);
        if (Mathf.Abs(delta) >= rotationThreshhold)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentYRoation, transform.eulerAngles.z);
            lastYRotation = currentYRoation;
        }
    }

}