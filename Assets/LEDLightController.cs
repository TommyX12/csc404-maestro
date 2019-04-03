using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDLightController : MonoBehaviour
{
    public Light LED;

    public void TurnOff() {
        LED.enabled = false;
    }

    public void TurnOn() {
        LED.enabled = true;
    }

}
