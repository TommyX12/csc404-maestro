using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public bool topDown = false;
    public float obliqueAngle = 45f;
    public GameObject followTarget;
    protected GameObject lastFollowTarget;
    protected PlayerAgentController player;
    public float smoothTime = 0.1f;
    public float distance;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 offset;

    public static MainCamera current;

    private Camera camera;
    // raycasting later

    private void Awake() {
        camera = GetComponent<Camera>();
        current = this;
    }

    protected void FixedUpdate() {
        if (followTarget != lastFollowTarget) {
            lastFollowTarget = followTarget;
            player = followTarget.GetComponent<PlayerAgentController>();
        }
        if (followTarget) {
            Vector3 v = new Vector3();
            float angleTarget = 0;

            if (topDown) {
                angleTarget = 80;
            }
            else
            {
                angleTarget = obliqueAngle;
            }

            Vector3 targetPosition = followTarget.transform.position;

            if (player) {
                var playerTarget = player.GetTarget();
                if (playerTarget) {
                    // TODO temporally disabled panning
                    // targetPosition = (targetPosition + playerTarget.transform.position) / 2;
                }
            }

            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, targetPosition + -camera.transform.forward * distance, ref v, smoothTime);
            Quaternion rotation = Quaternion.Euler(angleTarget, 0, 0);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, rotation, Time.fixedDeltaTime);
        }
    }

    public void ToggleTopDown() {
        topDown = !topDown;
    }
}
