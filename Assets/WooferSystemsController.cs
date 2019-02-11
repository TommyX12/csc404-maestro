using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferSystemsController : MonoBehaviour
{

    public ParticleSystem Charge;
    public ParticleSystem Death;
    public ParticleSystem Muzzle;

    float ChargeTime = 1;
    private float Timer;

    private void Start()
    {
        BasicWeapon w = GetComponent<BasicWeapon>();
        w.SetAutoFire(true);
        w.SetHost(GetComponent<Agent>());
    }

    public void StartCharge() {
        Timer = 1;
    }

    public void MuzzleFlash() {
        Muzzle.Emit(40);
    }

    public void MuzzleCharge() {
        Charge.Emit(40);
    }

}
