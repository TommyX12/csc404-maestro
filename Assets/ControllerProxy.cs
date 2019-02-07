using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerProxy
{

    public static bool GetButtonDown(string id) {
        return Input.GetButtonDown(id);
    }

    public static bool GetButton(string id) {
        return Input.GetButton(id);
    }

    public static float GetAxisRaw(string id)
    {
        return Input.GetAxisRaw(id);
    }

}
