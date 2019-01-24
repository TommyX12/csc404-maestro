using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : GridObject
{
    public GameObject DoorLeft;
    public GameObject DoorRight;

    public GameObject DoorLeftClosePos;
    public GameObject DoorLeftOpenPos;
    public GameObject DoorRightClosePos;
    public GameObject DoorRightOpenPos;

    public bool IsOpen = true;

    public float Rate = 10;

    public void Open() {
        IsOpen = true;
    }

    public void Close() {
        IsOpen = false;
    }
    
    public void FixedUpdate()
    {
        if (IsOpen)
        {
            DoorLeft.transform.position = Vector3.Lerp(DoorLeft.transform.position, DoorLeftOpenPos.transform.position, Time.fixedDeltaTime*Rate);
            DoorRight.transform.position = Vector3.Lerp(DoorRight.transform.position, DoorRightOpenPos.transform.position, Time.fixedDeltaTime*Rate);
        }
        else {
            DoorLeft.transform.position = Vector3.Lerp(DoorLeft.transform.position, DoorLeftClosePos.transform.position, Time.fixedDeltaTime*Rate);
            DoorRight.transform.position = Vector3.Lerp(DoorRight.transform.position, DoorRightClosePos.transform.position, Time.fixedDeltaTime*Rate);
        }
    }

}
