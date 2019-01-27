using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnalogDebug : MonoBehaviour
{
    public Text target;


    public void Update()
    {
        target.text = "Analog: " + new Vector2(Input.GetAxisRaw("RightStickHorizontal"), Input.GetAxisRaw("RightStickVertical"));
    }
}
