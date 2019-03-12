using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ProximityTrigger : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerLeave;

    public bool fireOnlyOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) {
            return;
        }
        TriggerEnter.Invoke();
        if (fireOnlyOnce) {
            Destroy(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) {
            return;
        }
        TriggerLeave.Invoke();
        if (fireOnlyOnce)
        {
            Destroy(this);
        }
    }

}
