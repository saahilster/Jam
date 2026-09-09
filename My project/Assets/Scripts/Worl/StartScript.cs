using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class StartScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        InputSystem.onAnyButtonPress.CallOnce(ctrl => Destroy(gameObject));
    }

}
