using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerProxy : MonoBehaviour
{

    private static bool axisInUseVertical = false;
    private static bool axisUsedThisFrameVertical = false;

    public static bool inputEnabled = true;

    public static bool GetButtonDown(string id, bool bypass = false) {
        if (inputEnabled || bypass)
        {
            return Input.GetButtonDown(id);
        }
        else {
            return false;
        }
    }

    public static bool GetButton(string id, bool bypass = false) {
        if (inputEnabled || bypass)
        {
            return Input.GetButton(id);
        }
        else {
            return false;
        }
    }

    public static float GetAxisRaw(string id, bool bypass = false)
    {
        if (inputEnabled || bypass)
        {
            return Input.GetAxisRaw(id);
        }
        else
        {
            return 0;
        }
    }

    public static int GetVerticalAxisOnce(bool bypass = false)
    {
        if (inputEnabled || bypass)
        {
            if (!axisInUseVertical && Input.GetAxisRaw("Vertical") >= 0.5)
            {
                axisUsedThisFrameVertical = true;
                return 1;
            }
            else if (!axisInUseVertical && Input.GetAxisRaw("Vertical") <= -0.5)
            {
                axisUsedThisFrameVertical = true;
                return -1;
            }
        }
        return 0;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") > -0.5 && Input.GetAxisRaw("Vertical") < 0.5)
        {
            axisInUseVertical = false;
        }
    }

    private void LateUpdate()
    {
        if (axisUsedThisFrameVertical) {
            axisUsedThisFrameVertical = false;
            axisInUseVertical = true;
        }
    }

}
