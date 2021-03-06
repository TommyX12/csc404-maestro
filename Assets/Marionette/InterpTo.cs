﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterpTo : MonoBehaviour
{
    public GameObject TargetPosition;
    public float Speed = 1;
    public bool PointToTarget = true;
    public UnityEvent Next;
    private bool start = false;

    private void FixedUpdate()
    {
        if (start) {
            Vector3 delta = TargetPosition.transform.position - transform.position;
            if (delta.magnitude <= 0.1f) {
                transform.position = TargetPosition.transform.position;
                if (Next.GetPersistentEventCount() > 0)
                {
                    Next.Invoke();
                }
            }
            transform.position = Vector3.Lerp(transform.position, TargetPosition.transform.position, Speed * Time.fixedDeltaTime);
            if (PointToTarget) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(delta, Vector3.up), Time.fixedDeltaTime * Speed);
            }
        }
    }

    public void Begin() {
        start = true;
    }

}
