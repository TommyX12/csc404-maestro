using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

 */

public class Shotgun : Weapon
{
    public AudioSource FireSound;
    public AudioSource FireSoundReload1;
    public AudioSource FireSoundReload2;
    public AudioSource JammedSound;
    
    public float tol = 0.05f;

    public bool Jammed = false;

    private void Start()
    {
        Init();
        riff.hitOffset = 0;
    }

    public void StopAllSounds() {
        FireSound.Stop();
        FireSoundReload1.Stop();
        FireSoundReload2.Stop();
        JammedSound.Stop();
    }

    public override void Fire()
    {
        Riff.ButtonPressResult press = riff.ButtonPress();
        if (Jammed) {
            return;
        }
        if (press.noteIndex != -1)
        {
            Debug.Log(press.noteIndex);
            switch (press.noteIndex) {
                case 0:
                    FireSoundReload1.Play();
                    break;
                case 1:
                    FireSound.Play();
                    break;
                case 2:
                    FireSoundReload2.Play();
                    break;
                default:
                    Jammed = true;
                    StopAllSounds();
                    JammedSound.Play();
                    break;
            }
        } else {
            Debug.Log(press.noteIndex);
            Jammed = true;
            StopAllSounds();
            JammedSound.Play();
        }
    }

    public override void OnBeat(int beatIdx)
    {
        Debug.Log("BEAT " + beatIdx);
    }

    public void FixedUpdate()
    {
        this.riff.Update();

        if (Jammed) {
            if (!JammedSound.isPlaying) {
                Jammed = false;
            }
        }

        if (PreviewRhythm)
        {
            riff.hitOffset = 0.2f;
            Riff.ButtonPressResult press = riff.ButtonPress();
            if (press.noteIndex != -1 && press.deltaTime < tol)
            {
                StaticAudioManager.current.GetPreviewSound().Play();
            }
        }
        else {
            riff.hitOffset = 0;
        }
    }
}
