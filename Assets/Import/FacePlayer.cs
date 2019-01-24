using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 target = Gladiator.GetInstance().transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(target, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.fixedDeltaTime);
    }
}
