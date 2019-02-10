using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRotatingBasicAgentMovement : BasicAgentMovement
{

    // self reference
    protected Rigidbody rigidbody;

    // exposed parameters
    public float thrust = 0.01f;
    public float friction = 0.875f;

    public NonRotatingBasicAgentMovement()
    {

    }

    public override void ReceiveEvent(Event.DirectionalMove directionalMove)
    {
        if (directionalMove.right != 0 || directionalMove.up != 0)
        {
            Vector3 direction = Vector3.ClampMagnitude(directionalMove.right * right + directionalMove.up * forward, 1);
            force = direction * thrust;
        }
    }

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    protected void Update()
    {

    }

    protected void UpdateMovement()
    {
        // apply force
        velocity += force;
        force = Vector3.zero;

        // friction
        velocity *= friction;

        // rotation and finalize
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f);
        rigidbody.position += velocity;
    }

    protected void FixedUpdate()
    {
        UpdateMovement();
    }
}
