using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GlobalConfiguration : ScriptableObject {

    public static GlobalConfiguration Current {get; set;}

    public float RiffHitMarginBefore {get; set;}
    public float RiffHitMarginAfter {get; set;}
    public float RiffHitFailedBlockBeats {get; set;} // in beats
    public float RiffAutoResetThreshold {get; set;}

    public float RiffHitPerfectThreshold {get; set;} // in seconds

    public GlobalConfiguration() {
        Current = this;
        
        RiffHitMarginBefore = 0.2f;
        RiffHitMarginAfter = 0.2f;
        RiffHitFailedBlockBeats = 0.5f;
        RiffAutoResetThreshold = 0.5f;
        RiffHitPerfectThreshold = 0.1f;
    }

    public float GetBPM() {
        return 110;
    }
}
