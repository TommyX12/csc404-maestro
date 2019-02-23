using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerProxy
{

    public static bool inputEnabled = true;

    public static bool GetButtonDown(string id) {
        if (inputEnabled)
        {
            return Input.GetButtonDown(id);
        }
        else {
            return false;
        }
    }

    public static bool GetButton(string id) {
        if (inputEnabled)
        {
            return Input.GetButton(id);
        }
        else {
            return false;
        }
    }

    public static float GetAxisRaw(string id)
    {
        if (inputEnabled)
        {
            return Input.GetAxisRaw(id);
        }
        else
        {
            return 0;
        }
    }
}
