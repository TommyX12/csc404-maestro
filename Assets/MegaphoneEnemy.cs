using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaphoneEnemy : BasicAgent
{
    [Range(0, 10)]
    public float forwardSpeed;
    [Range(0, 100)]
    public float frequency;
    [Range(0, 100)]
    public float amplitude;

    private float timer = 0;

    private new void Start()
    {
        base.Start();
        this.type = Agent.Type.ENEMY;
        foreach (BasicWeapon w in weapons) {
            w.SetAutoFire(true);
        }
        AgentManager.current.AddAgent(this);
    }

    private void FixedUpdate()
    {
        Vector3 deltaVector = transform.right * Mathf.Sin(timer * frequency)*amplitude;
        timer += Time.deltaTime;

        transform.position += (transform.forward * forwardSpeed + deltaVector) * Time.fixedDeltaTime;
    }
}
