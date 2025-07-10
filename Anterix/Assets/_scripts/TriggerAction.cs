using UnityEngine;
using UnityEngine.Events;

public class TriggerAction : MonoBehaviour
{
    public UnityEvent onActivate;

    // Call this from anywhere (e.g., button, click, etc.)
    public void PreformAction()
    {
        onActivate.Invoke();
    }
}
