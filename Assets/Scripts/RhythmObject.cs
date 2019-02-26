using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmObject : MonoBehaviour
{
    public int beatsPerCycle = 4;
    protected Riff riff;
    public List<int> actionIndices = new List<int>();
    public List<Riff.Note> notes = new List<Riff.Note> { new Riff.Note(0) };
    private List<AudioSource> noteSounds = new List<AudioSource>();

    public string defaultSound = "kick-1";

    protected void Init() {
        // create audio listeners for reach note
        riff = new Riff(beatsPerCycle, notes, MusicManager.current);
        riff.delayedNoteHitEvent += DelayedNoteHitEventHandler;
        riff.noteHitEvent += NoteHitEventHandler;
        riff.defaultSound = defaultSound;
        riff.playing = false;

        riff.delayedNoteHitEvent += DelayedNoteHitEvent;
        riff.noteHitEvent += NoteHitEvent;

        AudioSource templateSource = GetComponent<AudioSource>();

        List<string> uniqueAudioSounds = new List<string>();
        foreach (Riff.Note note in riff.GetNotes())
        {
            if (!uniqueAudioSounds.Contains(note.sound))
            {
                uniqueAudioSounds.Add(note.sound);
            }
        }

        AudioClip defaultClip = ResourceManager.GetMusic(riff.defaultSound);
        if (!defaultClip)
        {
            Debug.LogError("No default clip " + defaultClip);
        }

        AudioSource defaultSource;

        defaultSource = gameObject.AddComponent<AudioSource>();
        if (templateSource != null) {
            Util.CopyAudioSource(templateSource, defaultSource);
        }

        defaultSource.clip = defaultClip;
        defaultSource.outputAudioMixerGroup = MusicManager.current.Mixer; //  temp set do better @TODO;

        Dictionary<string, AudioSource> soundMapping = new Dictionary<string, AudioSource>();

        foreach (string audioSound in uniqueAudioSounds)
        {
            AudioClip clip = ResourceManager.GetMusic(audioSound);

            if (clip)
            {
                AudioSource source;
                source = gameObject.AddComponent<AudioSource>();
                if (templateSource != null)
                {
                    Util.CopyAudioSource(templateSource, source);
                }
                source.playOnAwake = false;
                source.clip = clip;
                source.outputAudioMixerGroup = MusicManager.current.Mixer; // temp set. do better later
                // @todo set a mixer
                soundMapping[audioSound] = source;
            }

        }
        foreach (Riff.Note note in riff.GetNotes())
        {
            if (soundMapping.ContainsKey(note.sound))
            {
                this.noteSounds.Add(soundMapping[note.sound]);
            }
            else
            {
                this.noteSounds.Add(defaultSource);
            }
        }
    }

    private void FixedUpdate()
    {
        if (riff!=null)
        {
            riff.Update();
        }
    }

    private void DelayedNoteHitEvent(Riff.NoteHitEvent e) {
        DelayedNoteHitEventHandler(e);
    }

    private void NoteHitEvent(Riff.NoteHitEvent e) {
        if (actionIndices.Contains(e.noteIndex)) {
            RhythmAction(e);
        }

        if (e.noteIndex != -1) {
            noteSounds[e.noteIndex].Play();
        }

        NoteHitEventHandler(e);
    }

    protected virtual void DelayedNoteHitEventHandler(Riff.NoteHitEvent e)
    {

    }

    protected virtual void NoteHitEventHandler(Riff.NoteHitEvent e)
    {

    }

    protected virtual void RhythmAction(Riff.NoteHitEvent e) {

    }

}
