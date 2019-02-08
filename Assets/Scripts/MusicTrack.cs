using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTrack {

    private double preplayOffset = 1.0;
    
    public string Name {
        get; private set;
    }
    
    public bool Playing {
        get {
            return this.audioSource.isPlaying;
        }
    }
    
    public bool Stopping {
        get; private set;
    }
    
    public float Volume = 1.0f;
    private float volumeMul = 0.0f;
    private float volumeMulFade = 0.0f;

    private float beatsPerCycle = -1; // negative means not a pattern
    private double nextPlayDSPTime = -1;
    private double nextScheduledTime = -1;

    private AudioSource[] audioSources = new AudioSource[2];
    private int flipID = 0;
    
    private AudioSource audioSource {
        get {
            return this.audioSources[flipID];
        }
    }

    private MusicManager musicManager;
    
    public MusicTrack(MusicManager musicManager, string name, AudioClip clip, GameObject audioSourceContainer, AudioMixerGroup mixer) {
        this.musicManager = musicManager;
        this.Name        = name;
        this.Stopping    = false;
        
        this.CreateAudioSource(clip, audioSourceContainer, mixer);
    }
    
    public void CreateAudioSource(AudioClip clip, GameObject audioSourceContainer, AudioMixerGroup mixer) {
        for (int i = 0; i < 2; ++i) {
            AudioSource a = audioSourceContainer.AddComponent<AudioSource>();
            a.clip = clip;
            a.outputAudioMixerGroup = mixer;
            a.bypassEffects = true;
            a.bypassListenerEffects = true;
            a.bypassReverbZones = true;
            a.dopplerLevel = 0;
            a.playOnAwake = false;
            a.reverbZoneMix = 0;
            a.spatialize = false;
        
            audioSources[i] = a;
        }
    }
    
    public void Update(float deltaTime) {
        this.volumeMul = Util.Clamp(
            this.volumeMul + deltaTime * this.volumeMulFade,
            0, 1
        );
        if (this.Playing && this.Stopping && this.volumeMul <= 0) {
            this.audioSource.Stop();
        }
        this.UpdateVolume();
        if (beatsPerCycle > 0) {
            this.UpdatePattern();
        }
    }

    public void UpdateVolume() {
        this.audioSource.volume = 
            musicManager.MasterVolume * 
            musicManager.ContextVolume * 
            this.Volume * this.volumeMul;
    }

    public void UpdatePattern() {
        if (AudioSettings.dspTime >= nextScheduledTime) {
            float timeToNextBeat = musicManager.BeatToTime(beatsPerCycle - musicManager.GetBeatIndex(beatsPerCycle, false));
            nextPlayDSPTime = Math.Max(AudioSettings.dspTime + timeToNextBeat, nextPlayDSPTime);

            if ((nextPlayDSPTime - AudioSettings.dspTime) < preplayOffset) {
                Debug.Log("test this");
                // THANKS THOMMY ITS NOT LIKE I WAS USING UR MUSIC MANAGER OR ANYTHING >:((((
                this.audioSource.PlayScheduled(nextPlayDSPTime);
                nextScheduledTime = nextPlayDSPTime;
                FlipAudioSource();
            }
        }
    }

    private void FlipAudioSource() {
        flipID = flipID == 0 ? 1 : 0;
    }

    public void PlayAsPattern(float beatsPerCycle, float fadeInTime) {
        this.beatsPerCycle = beatsPerCycle;
        this.Start(fadeInTime, false);
    }
    
    public void Start(float fadeInTime, bool loop) {
        this.audioSource.loop = loop;
        this.audioSource.Play();
        
        this.Stopping = false;
        this.volumeMul = 0;
        this.SetFade(true, fadeInTime);
        this.Update(0);
    }
    
    public void Stop(float fadeOutTime) {
        if (!this.Playing) return;
        this.Stopping = true;
        this.SetFade(false, fadeOutTime);
        this.Update(0);
    }

    public void Resume(float fadeInTime) {
        if (!this.Playing) return;
        this.Stopping = false;
        this.SetFade(true, fadeInTime);
        this.Update(0);
    }

    public void Silence(float fadeOutTime) {
        if (!this.Playing) return;
        this.Stopping = false;
        this.SetFade(false, fadeOutTime);
        this.Update(0);
    }

    public float GetPosition() {
        return this.audioSource.time;
    }
    
    private void SetFade(bool fadeIn, float fadeTime) {
        this.volumeMulFade = (fadeIn ? 1 : -1) / (fadeTime == 0 ? 0.0001f : fadeTime);
    }
    
    public void ForceStop() {
        this.audioSource.Stop();
    }
    
}
