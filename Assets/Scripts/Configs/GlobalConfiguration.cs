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
        [SerializeField]
        public string SceneName;
        [SerializeField]
        public string DisplayName;
        [SerializeField]
        public string leaderboardFile;
        [SerializeField]
        public string artists;
        [SerializeField]
        public string audioFile;
        [SerializeField]
        public int difficulty;
        [SerializeField]
        public int bpm;
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

    public float PickupSpawnInterval = 32.0f; // beats
    public float PickupEffectDuration = 16.0f; // beats
    public float PickupItemDuration = 12.0f; // beats
    public string PickupSpawnedSound = "chord-1";
    public string PickupStartEffectSound = "powerup-1";
    public Vector3 PickupStartParticleOffset = new Vector3(0.0f, 3.0f, 0.0f);
    // public float PickupStartParticleOffsetDistance = 3.0f;
    public float PickupStartParticleScale = 4.0f;
    public float PickupStartParticleDuration = 5.0f;

    public float PowerupUILerpFactor = 0.25f;

    public float PowerupSpeedBoostMultiplier = 1.5f;
    public float PowerupScreenFlashDuration = 0.5f; // seconds
    public float PowerupScreenFlashIntensity = 0.5f;

    public float GuideLineAnimationSpeed = 2.0f;

    [SerializeField]
    private Level[] levels = {
        new Level{
            SceneName = "Tutorial",
            DisplayName = "Tutorial",
            leaderboardFile = " tutorial",
            difficulty = 1
        },
        new Level{
            SceneName = "Alpha3",
            DisplayName = "Dominion",
            leaderboardFile = "leaderboard",
            difficulty = 2
        }
    };

    private Dictionary<PickupEffect.PickupEffectType, string> pickupEffectTypeDescriptions =
        new Dictionary<PickupEffect.PickupEffectType, string>() {
        {PickupEffect.PickupEffectType.DOUBLE_SHOT, "Double Shot"},
        {PickupEffect.PickupEffectType.SPEED_BOOST, "Speed Boost"},
        {PickupEffect.PickupEffectType.SHIELD, "Shield"},
        {PickupEffect.PickupEffectType.EXTRA_BEATS, "Extra Beats"}
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

    public int GetLevelCount() {
        return levels.Length;
    }

    // TODO: a hack
    public void SetGlobalAudioDelay(float value) {
        _globalAudioDelay = value;
    }

    public string GetPickupEffectDescriptions(PickupEffect effect) {
        return pickupEffectTypeDescriptions[effect.effectType];
    }
}
