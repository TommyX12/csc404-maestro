using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerProxySystem : MonoBehaviour
{
    public static ControllerProxySystem current;
    private void Awake()
    {
        current = this;
    }
    public void EnableInput() {
        ControllerProxy.inputEnabled = true;
    }

    public void DisableInput() {
        ControllerProxy.inputEnabled = false;
    }

}
