using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GlobalRules {

    private readonly GlobalConfiguration config;

    public GlobalRules(GlobalConfiguration config) {
        this.config = config;
    }

    public GameplayModel.BeatPressedEventScore GetHitScore(Riff.NoteHitEvent e) {
        if (e.noteIndex == 0) {
            return GameplayModel.BeatPressedEventScore.MISSED;
        }
        return Mathf.Abs(e.deltaTime) <= config.RiffHitPerfectThreshold ?
            GameplayModel.BeatPressedEventScore.PERFECT :
            GameplayModel.BeatPressedEventScore.GOOD;
    }
}
