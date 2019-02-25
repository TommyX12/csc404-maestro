using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BeatSequencer : MonoBehaviour
{
    public bool useClip;
    public AudioClip clip;
    public int bpm = 1;
    public int beatNum;
    public int trackNum;

    [HideInInspector]
    public bool[] tracks = null;

    public Texture2D sequencerOnTextureA;
    public Texture2D sequencerOffTextureA;
    public Texture2D sequencerOnTextureB;
    public Texture2D sequencerOffTextureB;
}
