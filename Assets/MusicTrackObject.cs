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
        MusicManager.Current.AddMusicTrack(audioName);
    }
    public void Play() {
        MusicManager.Current.StartRiff(audioName, 80);
        MusicManager.Current.MasterVolume = volume;
    }

    public void Stop() {
        MusicManager.Current.Stop(audioName);
    }
}
