using System;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public delegate void RestartEventHandler();
    protected event RestartEventHandler restartEvent;

    private float currentTime = 0;

    protected void Start() {
        currentTime = 0;
    }

    protected void FixedUpdate() {
        var deltaTime = Time.fixedDeltaTime;
        currentTime += deltaTime;

        // TODO
    }

    public void AddRestartEventHandler(RestartEventHandler handler) {
        restartEvent += handler;
    }

    public void RemoveRestartEventHandler(RestartEventHandler handler) {
        restartEvent -= handler;
    }

    protected void EmitRestartEvent() {
        if (restartEvent != null) {
            restartEvent();
        }
    }

    /// <summary>
    ///   Returns (floating point) number of beats to the next phase position in a riff.
    ///   riffSize is the number of total beats in the riff.
    /// </summary>
    public float GetBeatsToNext(float phase, float riffSize) {
        return 0; // TODO
    }

    public float GetCurrentPhase(float riffSize) {
        return 0; // TODO
    }

    public reflection GetCurrentCycle(float riffSize) {
        return 0;
    }

}
