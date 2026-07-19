using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    public UnityEvent dead;
    public int playerHP = 3;
    float[] fogDensity = {0.07f, 0.03f, 0.01f };
    private int currentFog = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderSettings.fogDensity = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHP <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            dead.Invoke();
        }
    }

    public void ImpairSight()
    {
        RenderSettings.fogDensity = fogDensity[currentFog];
    }

    public void LoseHP()
    {
        playerHP--;
        currentFog--;
    }
}
