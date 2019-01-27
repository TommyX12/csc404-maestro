using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpringArmCamera : MonoBehaviour
{
    public GameObject cameraObject;
    public float positionStrength;
    public float rotationStrength;
    public bool rotateWithParent = false;
    private Vector3 offset;
    private GameObject parentObj;
    // raycasting later

    private void Start()
    {
        if (!rotateWithParent) {
            if (cameraObject) {
                Vector3 parentPos = transform.parent.position;
                parentObj = transform.parent.gameObject;
                offset = transform.position - parentPos;
            }
        }
    }

    private void FixedUpdate()
    {

        if (cameraObject)
        {
            if (rotateWithParent)
            {
                cameraObject.transform.rotation = Quaternion.Lerp(cameraObject.transform.rotation, transform.rotation, Time.fixedDeltaTime * rotationStrength);
                cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, transform.position, Time.fixedDeltaTime * positionStrength);
            }
            else {
                cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, parentObj.transform.position + offset, Time.fixedDeltaTime * positionStrength);
            }
        }
    }
}
