using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnDestroyTrigger : MonoBehaviour
{
    public UnityEvent OnDestroyEvent;

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            OnDestroyEvent.Invoke();
        }
    }
}
