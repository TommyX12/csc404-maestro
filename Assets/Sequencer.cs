using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    [HideInInspector]

    public bool[] data;

    [Range(1,4)]
    public int beatDiv = 1;

    public float GetTime(int idx) {
        if (Scrubber.instance != null) {
            return (60f / (Scrubber.instance.bpm * Mathf.RoundToInt(Mathf.Pow(2, beatDiv - 1)))) * idx;
        }
        return -1;
    }
}