using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEnemy : BasicAgent
{
    [Range(1, 10)]
    public int beatsPerRotation = 4;
    public bool active = false;

    private void FixedUpdate()
    {
        if (!active) {
            foreach (BasicWeapon w in weapons) {
                w.SetAutoFire(false);
            }
            return;
        }

        foreach (BasicWeapon w in weapons)
        {
            w.SetAutoFire(true);
        }

        transform.rotation = transform.rotation * Quaternion.Euler(0, (Time.fixedDeltaTime * MusicManager.Current.bpm/60f)*360f/beatsPerRotation, 0);
    }

}
