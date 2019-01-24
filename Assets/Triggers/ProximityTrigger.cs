using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ProximityTrigger : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerLeave;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) {
            return;
        }
        TriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) {
            return;
        }
        TriggerLeave.Invoke();
    }

}
