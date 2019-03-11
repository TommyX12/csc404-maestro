using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu
 (fileName = "Global Configuration",
  menuName = "Configurations/Global Configuration")]
public class GlobalConfiguration : ScriptableObject {

    public static GlobalConfiguration Current {get; set;}

    public float RiffHitMarginBefore = 0.2f;
    public float RiffHitMarginAfter = 0.2f;
    public float RiffHitFailedBlockBeats = 0.5f; // in beats
    public float RiffAutoResetThreshold = 0.5f;
    public float RiffHitPerfectThreshold = 0.1f; // in seconds

    public float ScoreColorEffectDuration = 1.0f;
    public Color ScoreIncreaseColor = new Color(0, 1, 0);
    public Color ScoreDecreaseColor = new Color(1, 0, 0);
    public Color ScoreIdleColor = new Color(1, 1, 1);

    public string CorrectNoteHitSoundName = "chord-1";

    public float AudioDelay = 0.2f;
    public float AudioMinLoadTime = 0.02f;

    public float RiffSoundVolume = 0.4f;

    public GlobalConfiguration() {
        Current = this;
    }

    public float GetBPM() {
        return 110;
    }
}
