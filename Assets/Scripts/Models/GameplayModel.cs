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

    // Events
    public event Action<BeatPressedEvent> BeatPressed;

    // Methods
    
    public void NotifyBeatPressed(BeatPressedEvent eventData) {
        if (BeatPressed != null) {
            BeatPressed(eventData);
        }
    }

    public void AddScore(float score) {
        Score += score;
    }

}
