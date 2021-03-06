﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class EnemySpawnSystem : MonoBehaviour
{

    private DiContainer container;

    [Inject]
    public void Construct(DiContainer container) {
        this.container = container;
    }

    private class SpawnEventFactory {
        GameObject prefab;
        EnemySpawnSystem host;
        DiContainer container;

        public SpawnEventFactory(GameObject prefab, EnemySpawnSystem host, DiContainer container) {
            this.prefab = prefab;
            this.host = host;
            this.container = container;
        }

        public void HandleNoteHitEvent(Riff.NoteHitEvent hit) {
            if (hit.noteIndex != -1) {
                // spawn shit
                GameObject spawned = container.InstantiatePrefab(prefab);
                spawned.transform.position = host.transform.position;
                spawned.transform.rotation = host.transform.rotation;
            }
        }
    }

    public List<GameObject> enemyPrefabs;
    public BeatSequencer sequencer;
    private List<Riff> riffs = new List<Riff>();
    private List<SpawnEventFactory> spawnEventFactories = new List<SpawnEventFactory>();

    private void Start()
    {
        for (int i = 0; i < sequencer.trackNum; i++) {
            List <Riff.Note> notes = new List<Riff.Note>();
            for (int j = 0; j < sequencer.beatNum; j++) {
                if (sequencer.tracks[i * sequencer.beatNum + j])
                {
                    notes.Add(new Riff.Note(j));
                }
            }
            if (notes.Count > 0)
            {
                // create riff and a spawn event class and set them up
                Riff riff = new Riff(sequencer.beatNum, notes, MusicManager.current);
                // Debug.Log(sequencer.beatNum + " " + notes.Count);
                spawnEventFactories.Add(new SpawnEventFactory(enemyPrefabs[i], this, container));
                riff.noteHitEvent += spawnEventFactories[i].HandleNoteHitEvent;
                riffs.Add(riff);
            }
            else {
                riffs.Add(null);
                spawnEventFactories.Add(null);
            }
        }
    }

    public void FixedUpdate()
    {
        foreach (Riff riff in riffs) {
            if (riff != null)
            {
                riff.Update();
            }
        }
    }
}
