using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameplayModel : ScriptableObject {

    // Event data
    public enum BeatPressedEventScore {
        MISSED, GOOD, PERFECT
    }
    public class BeatPressedEvent {
        public BeatPressedEventScore Score {get; set;}
    }

    // Properties
    public float PlayerHealth {get; set;}
    public float PlayerTotalHealth {get; set;}
    public float Score {get; set;}
    public int combo = 1;
    public PickupEffect CurrentPickupEffect {get; set;}
    public float CurrentPickupTimeLeftPercentage = 0;
    public bool LevelStarted = false;
    public bool CanSpawnPickup {
        get {
            return LevelStarted;
        }
    }

    // Events
    public event Action<BeatPressedEvent> BeatPressed;
    public event Action TutorialFinished;
    public event Action ShowMoveTutorial;
    public event Action PickupEffectActivated;
    public event Action PickupEffectEnded;

    // Methods

    public void NotifyPickupEffectActivated() {
        if (PickupEffectActivated != null) {
            PickupEffectActivated();
        }
    }
    
    public void NotifyPickupEffectEnded() {
        if (PickupEffectEnded != null) {
            PickupEffectEnded();
        }
    }
    
    public void NotifyBeatPressed(BeatPressedEvent eventData) {
        if (BeatPressed != null) {
            BeatPressed(eventData);
        }
    }

    public void NotifyTutorialFinished() {
        if (TutorialFinished != null) {
            TutorialFinished();
        }
    }

    public void NotifyShowMoveTutorial() {
        if (ShowMoveTutorial != null) {
            ShowMoveTutorial();
        }
    }

    public void AddScore(float score) {
        Score += score * combo;
    }

    public void PlayerDied() {
        Score = 0;
        combo = 1;
    }

    public void PlayerHit() {
        combo = 0;
    }

    public void IncrementCombo() {
        combo++;
    }

}
