using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu
 (fileName = "Global Configuration",
  menuName = "Configurations/Global Configuration")]
public class GlobalConfiguration : ScriptableObject {

    [Serializable]
    public class Level {
        public string SceneName;
        public string DisplayName;
    }

    // TODO: a hack
    public static float _globalAudioDelay = 0.2f;
    /////

    public static GlobalConfiguration Current {get; set;}

    public string Version = "Alpha 3.0";

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

    public float AudioDelay {
        get {
            return _globalAudioDelay;
        }
    }
    public float AudioDelayMin = 0.0f;
    public float AudioDelayMax = 0.5f;
    public float AudioDelayStep = 0.2f;
    public float AudioMinLoadTime = 0.02f;

    public Color SettingsOffsetTextIdleColor = new Color(1, 1, 1);
    public Color SettingsOffsetTextBeatColor = new Color(0.2f, 0.4f, 1.0f);
    public float SettingsOffsetTextBeatColorDuration = 0.3f;

    public float RiffSoundVolume = 0.6f;

    public string MoveTutorialText = "Use left stick to move";
    public float MoveTutorialTimeout = 20.0f;

    public float PlayerMaxHealth = 4.0f;

    public int HealthBarNumBars = 8;
    public float HealthBarSpacing = 0.1f;
    public float HealthBarMinHeight = 0.5f;
    public float HealthBarMaxHeight = 1.0f;

    public float CalibrationScaleFactor = 0.25f;

    [SerializeField]
    private Level[] levels = {
        new Level{
            SceneName = "Tutorial",
            DisplayName = "Tutorial"
        },
        new Level{
            SceneName = "Alpha3",
            DisplayName = "Dominion"
        }
    };

    public GlobalConfiguration() {
        Current = this;
    }

    public float GetBPM() {
        return 110;
    }

    public Level GetLevel(int index) {
        return levels[index];
    }

    // TODO: a hack
    public void SetGlobalAudioDelay(float value) {
        _globalAudioDelay = value;
    }
}
