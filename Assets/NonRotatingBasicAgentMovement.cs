using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRotatingBasicAgentMovement : BasicAgentMovement
{
    private new void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX 
            | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public override void ReceiveEvent(Event.DirectionalMove directionalMove)
    {
        if (directionalMove.right != 0 || directionalMove.up != 0)
        {
            Vector3 direction = Vector3.ClampMagnitude(directionalMove.right * right + directionalMove.up * forward, 1);
            // force = direction * thrust;
        }
    }
}
