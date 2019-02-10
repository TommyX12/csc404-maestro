using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CombatPlayer : MonoBehaviour {
    
    public AudioSource DeathSound;

    private Vector3 up = Vector3.up;
    private Vector3 forward = Vector3.fwd;
    private Vector3 right = Vector3.right;
    public float thrust = 20.0f;
    public float friction = 0.9f;

    public List<GameObject> WeaponPositions;
    public List<Weapon> Weapons;

    private Quaternion targetRotation;

    // self reference
    private Rigidbody rigidbody;

    public CombatPlayer() {
        
    }

    public bool AddWeapon(Weapon w) {
        if (Weapons.Count < WeaponPositions.Count)
        {
            Weapons.Add(w);
            w.gameObject.transform.SetParent(WeaponPositions[Weapons.Count - 1].transform);
            w.gameObject.transform.localPosition = Vector3.zero;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    protected void Update() {
        if (Input.GetButtonDown("RB")) {
            if (Weapons.Count > 0) {
                Weapons[0].Fire();
            }
        }
    }

    private void UpdateMovement() {
        // input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            Vector3 direction = horizontal * right + vertical * forward;
            rigidbody.AddForce(direction * thrust);
            targetRotation = Quaternion.LookRotation(direction);
        }
        
        // rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f);

        // friction
        Vector3 vel = rigidbody.velocity;
        vel *= friction;
        rigidbody.velocity = vel;
    }

    public void FixedUpdate() {
        UpdateMovement();
    }
}
