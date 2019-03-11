using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonTrigger : MonoBehaviour
{
    public string button = "Fire1";
    public UnityEvent evt;
    // Update is called once per frame
    void Update()
    {
        if (ControllerProxy.GetButtonDown(button)) {
            evt.Invoke();
        }
    }
}
