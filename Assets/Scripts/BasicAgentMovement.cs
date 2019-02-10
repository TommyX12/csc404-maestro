using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicAgentMovement : AgentMovement {
    
    private Vector3 up = Vector3.up;
    private Vector3 forward = Vector3.fwd;
    private Vector3 right = Vector3.right;

    private Quaternion targetRotation;

    private Vector3 velocity = Vector3.zero;
    private Vector3 force = Vector3.zero;
    
    // self reference
    protected Rigidbody rigidbody;

    // exposed parameters
    public float thrust = 0.01f;
    public float friction = 0.875f;
    
    public BasicAgentMovement() {
        
    }

    public override void ReceiveEvent(Event.DirectionalMove directionalMove) {
        if (directionalMove.right != 0 || directionalMove.up != 0) {
            Vector3 direction = Vector3.ClampMagnitude(directionalMove.right * right + directionalMove.up * forward, 1);
            force = direction * thrust;
            targetRotation = Quaternion.LookRotation(direction);
        }
    }

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    protected void Update() {
        
    }

    protected void UpdateMovement() {
        // apply force
        velocity += force;
        force = Vector3.zero;
        
        // friction
        velocity *= friction;
        
        // rotation and finalize
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f);
        rigidbody.position += velocity;
    }

    protected void FixedUpdate() {
        UpdateMovement();
    }
}
