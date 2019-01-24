using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPeriodically : MonoBehaviour
{
    public GameObject FirePoint;
    public float CoolDown;
    private float cooldownTimer;
    public GameObject projectile;

    private void Start()
    {
        cooldownTimer += CoolDown;
    }

    private void FixedUpdate()
    {
        if (cooldownTimer <= 0) {
            cooldownTimer += CoolDown;
            Fire();
        }
        cooldownTimer -= Time.fixedDeltaTime;
    }

    private void Fire() {
        GameObject clone = GameObject.Instantiate(projectile);
        clone.transform.position = FirePoint.transform.position;
        clone.transform.rotation = FirePoint.transform.rotation;
        clone.GetComponent<DamageSource>().DamageFilter = ~GetComponent<Damageable>().DamageFilter;
    }
}
