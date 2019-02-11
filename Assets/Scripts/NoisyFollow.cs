using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NoisyFollow : MonoBehaviour {

    protected static Vector2 noiseDirection1 = new Vector2(1, 0);
    protected static Vector2 noiseDirection2 = new Vector2(0, 1);
    protected static Vector2 noiseDirection3 = new Vector2(1, 1);

    // exposed parameters
    public Transform target;
    public float friction = 0.8f;
    public Vector3 targetPosition;
    public Vector3 minCorner = new Vector3(-1.5f, 1.5f, -1.5f);
    public Vector3 maxCorner = new Vector3(1.5f, 1.5f, 1.5f);
    public float noiseSpeed = 0.35f;
    
    protected float seed = 0;
    
    public NoisyFollow() {
        
    }

    protected void Awake() {
        targetPosition = transform.position;
        seed = UnityEngine.Random.Range(0, 100.0f);
    }

    protected void Start() {
        
    }

    protected void FixedUpdate() {
        if (target) {
            targetPosition = target.position;
        }

        Vector3 noise = new Vector3();
        Vector2 noiseDirection;
        float rand;

        noiseDirection = noiseDirection1 * (Time.time * noiseSpeed + seed);
        rand = Mathf.PerlinNoise(noiseDirection.x, noiseDirection.y);
        noise.x = minCorner.x + rand * (maxCorner.x - minCorner.x);

        noiseDirection = noiseDirection2 * (Time.time * noiseSpeed + seed);
        rand = Mathf.PerlinNoise(noiseDirection.x, noiseDirection.y);
        noise.y = minCorner.y + rand * (maxCorner.y - minCorner.y);

        noiseDirection = noiseDirection3 * (Time.time * noiseSpeed + seed);
        rand = Mathf.PerlinNoise(noiseDirection.x, noiseDirection.y);
        noise.z = minCorner.z + rand * (maxCorner.z - minCorner.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition + noise, 1 - friction);
    }
}
