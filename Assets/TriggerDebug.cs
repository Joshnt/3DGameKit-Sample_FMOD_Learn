using UnityEngine;

public class TriggerDebug : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger with: " + other.gameObject.name + " " + other.gameObject.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger with: " + other.gameObject.name);
    }
}

