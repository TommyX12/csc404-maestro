using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PlayerAgentController))]
public class TargettingRay : MonoBehaviour {

    private LineRenderer lineRenderer;
    private PlayerAgentController playerAgentController;

    public TargettingRay() {
        
    }

    protected void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        playerAgentController = GetComponent<PlayerAgentController>();
    }

    protected void Start() {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    protected void Update() {
        Agent target = playerAgentController.GetTarget();
        if (target) {
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[]{
                playerAgentController.transform.position,
                target.transform.position
            });
        }
        else {
            lineRenderer.enabled = false;
        }
    }
}
