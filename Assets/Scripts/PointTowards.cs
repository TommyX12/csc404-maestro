using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowards : MonoBehaviour
{
    public GameObject Target;

    private void FixedUpdate()
    {
        if (Target)
        {
            Vector3 delta = Target.transform.position - transform.position;
            if (delta.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position, Vector3.up);
            }
        }
    }
}
