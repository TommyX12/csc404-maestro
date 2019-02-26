using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{

    private class SpawnEventFactory {
        GameObject prefab;
        EnemySpawnSystem host;

        public SpawnEventFactory(GameObject prefab, EnemySpawnSystem host) {
            this.prefab = prefab;
            this.host = host;
        }

        public void HandleNoteHitEvent(Riff.NoteHitEvent hit) {
            if (hit.noteIndex != -1) {
                // spawn shit
                GameObject spawned = Instantiate(prefab);
                spawned.transform.position = host.transform.position;
                spawned.transform.rotation = host.transform.rotation;
                host.enemies.Add(spawned);
            }
        }
    }

    public List<GameObject> enemyPrefabs;
    public BeatSequencer sequencer;
    private List<Riff> riffs = new List<Riff>();
    private List<SpawnEventFactory> spawnEventFactories = new List<SpawnEventFactory>();
    private List<GameObject> enemies = new List<GameObject>();
    [Range(0, 10)]
    public float speed = 1f;    // for now, probably should inject some layer between enemy and basic agent
                                // for scrolling enemy

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
            // create riff and a spawn event class and set them up
            Riff riff = new Riff(sequencer.beatNum, notes, MusicManager.current);

            Debug.Log(sequencer.beatNum + " " + notes.Count);
            spawnEventFactories.Add(new SpawnEventFactory(enemyPrefabs[i], this));
            riff.noteHitEvent += spawnEventFactories[i].HandleNoteHitEvent;
            riffs.Add(riff);
        }
    }

    public void FixedUpdate()
    {
        foreach (Riff riff in riffs) {
            riff.Update();
        }
    }
}
