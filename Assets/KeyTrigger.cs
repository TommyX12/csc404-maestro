using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KeyTrigger : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent evt;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key)) {
            evt.Invoke();
        }
    }
}
