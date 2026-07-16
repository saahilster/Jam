using System.Collections;
using UnityEditor;
using UnityEngine;

public class RandomLight : MonoBehaviour
{
    Light light;
    bool canCall = true;

    IEnumerator LightDuration()
    {
        canCall = false;
        int chance = Random.Range(1, 10);
        if(chance >= 8)
        {
            light.intensity = Random.Range(300, 800);
            yield return new WaitForSeconds(Random.Range(0.5f, 4));
            light.intensity = 0;
            canCall = true;
        }
        else
        {
            light.intensity = 0;
            canCall = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canCall)
        {
            StartCoroutine(LightDuration());
        }
    }
}
