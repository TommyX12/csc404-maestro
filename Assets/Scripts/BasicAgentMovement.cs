using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicAgentMovement : AgentMovement {

    protected Vector3 up = Vector3.up;
    protected Vector3 forward = Vector3.fwd;
    protected Vector3 right = Vector3.right;

    protected Quaternion targetRotation;

    protected Vector3 velocity = Vector3.zero;
    protected Vector3 force = Vector3.zero;
    
    // self reference
    protected Rigidbody rigidbody;

    // exposed parameters
    public float maxSpeedPerSecond = 2.0f;
    public float friction = 0.125f;
    public float rotationFriction = 0.925f;
    
    public BasicAgentMovement() {
        
    }

    /// <summary>
    ///   Should be in FixedUpdate
    /// </summary>
    public override void ReceiveEvent(Event.DirectionalMove directionalMove) {
        if (directionalMove.right != 0 || directionalMove.up != 0) {
            Vector3 direction = Vector3.ClampMagnitude(directionalMove.right * right + directionalMove.up * forward, 1);

            float thrust = (friction * (maxSpeedPerSecond * Time.fixedDeltaTime)) / (1 - friction);

            force = direction * thrust;
            targetRotation = Quaternion.LookRotation(direction);
        }
    }

    public override void ReceiveEvent(Event.LookAt lookAt) {
        targetRotation = Quaternion.LookRotation(lookAt.position - transform.position);
    }

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX 
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;
    }

    protected void Update() {
        
    }

    protected void UpdateMovement() {
        // apply force
        velocity += force;
        force = Vector3.zero;
        
        // friction
        velocity *= 1 - friction;
        
        // rotation and finalize
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 - rotationFriction);
        rigidbody.position += velocity;
    }

    protected void FixedUpdate() {
        UpdateMovement();
    }
}
