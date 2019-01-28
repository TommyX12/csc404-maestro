using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 0 - fire1
 * 2 - Reload_1_0
 * 3 - Reload_1_1
 * 4 - fire2
 * 5 - fire3
 * 6 - Reload_3_0
 * 7 - Reload_3_1
 */

public class Shotgun : Weapon
{
    public AudioSource FireSound;
    public float tol = 0.05f;

    private void Start()
    {
        Init();
    }

    public override void Fire()
    {
        
    }

    public override void OnBeat(int beatIdx)
    {
        Debug.Log("BEAT " + beatIdx);
    }

    public void FixedUpdate()
    {
        this.riff.Update();
        Riff.ButtonPressResult press = riff.ButtonPress();
        if (press.noteIndex != -1 && press.deltaTime < tol)
        {
            if (press.noteIndex == 0 || press.noteIndex == 4 || press.noteIndex == 5) {
                FireSound.Play();
            }
            Debug.Log("NOTE " + press.noteIndex);
        }
    }

}
