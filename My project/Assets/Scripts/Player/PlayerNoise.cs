using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    public static PlayerNoise Instance;
    [SerializeField] private float decayRate = 1f;
    private float currentNoiseLevel;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        currentNoiseLevel = Mathf.MoveTowards(currentNoiseLevel, 0f, decayRate * Time.deltaTime);
    }
    public void AddNoise(float amount)
    {
        currentNoiseLevel = Mathf.Max(currentNoiseLevel, amount);
    }

    public float GetNoiseLevel() 
    {
      return currentNoiseLevel;  
    } 
}
