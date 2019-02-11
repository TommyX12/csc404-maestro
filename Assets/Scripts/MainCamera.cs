using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject followTarget;
    public float smoothTime = 0.1f;
    public float distance;
    private Vector3 offset;

    private Camera camera;
    // raycasting later

    private void Awake() {
        camera = GetComponent<Camera>();
        offset = -camera.transform.forward * distance;
    }

    protected void Start() {
        
    }

    protected void FixedUpdate() {
        if (followTarget) {
            Vector3 v = new Vector3();
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, followTarget.transform.position + offset, ref v, smoothTime);
        }
    }
}
