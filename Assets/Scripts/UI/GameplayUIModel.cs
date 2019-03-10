using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameplayUIModel : ScriptableObject {

    // Event data
    public enum BeatPressedEventScore {
        MISSED, GOOD, PERFECT
    }
    public class BeatPressedEvent {
        public BeatPressedEventScore Score {get; set;}
    }

    // Delegates
    public delegate void BeatPressedHandler(BeatPressedEvent eventData);

    // Properties
    public float PlayerHealth {
        get; set;
    }
    public float PlayerTotalHealth {
        get; set;
    }

    // Events
    public event BeatPressedHandler BeatPressed;

    public void NotifyBeatPressed(BeatPressedEvent eventData) {
        if (BeatPressed != null) {
            BeatPressed(eventData);
        }
    }

}
