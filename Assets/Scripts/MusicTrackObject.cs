using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrackObject : MonoBehaviour
{

    public string audioName;
    [Range(0,1)]
    public float volume = 1;
    private void Start()
    {
        MusicManager.current.AddMusicTrack(audioName);
    }
    public void Play() {
        MusicManager.current.StopAll();
        MusicManager.current.StartRiff(audioName, CombatGameManager.current.trackBPM);
        MusicManager.current.MasterVolume = volume;
    }

    public void Stop() {
        MusicManager.current.Stop(audioName);
    }
}
