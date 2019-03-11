using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GlobalConfiguration : ScriptableObject {

    public static GlobalConfiguration Current {get; set;}

    public float AudioDelay {get; set;}
    public float AudioMinLoadTime {get; set;}

    public float RiffHitMarginBefore {get; set;}
    public float RiffHitMarginAfter {get; set;}
    public float RiffHitFailedBlockBeats {get; set;} // in beats
    public float RiffAutoResetThreshold {get; set;}
    public float RiffHitPerfectThreshold {get; set;} // in seconds

    public float ScoreColorEffectDuration {get; set;}
    public Color ScoreIncreaseColor {get; set;}
    public Color ScoreDecreaseColor {get; set;}
    public Color ScoreIdleColor {get; set;}

    public string CorrectNoteHitSoundName {get; set;}

    public GlobalConfiguration() {
        Current = this;

        RiffHitMarginBefore = 0.2f;
        RiffHitMarginAfter = 0.2f;
        RiffHitFailedBlockBeats = 0.5f;
        RiffAutoResetThreshold = 0.5f;
        RiffHitPerfectThreshold = 0.1f;

        ScoreColorEffectDuration = 1.0f;
        ScoreIncreaseColor = new Color(0, 1, 0);
        ScoreDecreaseColor = new Color(1, 0, 0);
        ScoreIdleColor = new Color(1, 1, 1);

        CorrectNoteHitSoundName = "chord-1";

        AudioDelay = 0.2f;
        AudioMinLoadTime = 0.02f;

    }

    public float GetBPM() {
        return 110;
    }
}
