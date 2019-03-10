using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GlobalRules {

    private GlobalConfiguration config;

    public GlobalRules(GlobalConfiguration config) {
        this.config = config;
    }

    public GameplayUIModel.BeatPressedEventScore GetHitScore(Riff.NoteHitEvent e) {
        if (e.noteIndex == 0) {
            return GameplayUIModel.BeatPressedEventScore.MISSED;
        }
        return Mathf.Abs(e.deltaTime) <= config.RiffHitPerfectThreshold ?
            GameplayUIModel.BeatPressedEventScore.PERFECT :
            GameplayUIModel.BeatPressedEventScore.GOOD;
    }
}
