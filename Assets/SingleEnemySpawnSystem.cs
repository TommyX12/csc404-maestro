using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEnemySpawnSystem : MonoBehaviour
{
    private class SpawnEventFactory
    {
        GameObject prefab;
        SingleEnemySpawnSystem host;

        public SpawnEventFactory(GameObject prefab, SingleEnemySpawnSystem host)
        {
            this.prefab = prefab;
            this.host = host;
        }

        public void HandleNoteHitEvent(Riff.NoteHitEvent hit)
        {
            if (hit.noteIndex != -1)
            {
                // spawn shit
                GameObject spawned = Instantiate(prefab);
                spawned.transform.position = host.transform.position;
                spawned.transform.rotation = host.transform.rotation;
            }
        }
    }

    public GameObject enemyPrefab;
    private Riff riff;
    private SpawnEventFactory spawnEventFactory;
    public Sequencer sequencer;

    private void Start()
    {

        List<Riff.Note> notes = new List<Riff.Note>();
        for (int i = 0; i < sequencer.data.Length; i++) {
            if (sequencer.data[i]) {
                notes.Add(new Riff.Note(i, sequencer.beatDiv));
            }
        }
        if (notes.Count > 0)
        {
            // create riff and a spawn event class and set them up
            riff = new Riff(sequencer.data.Length, notes, MusicManager.current);
            // Debug.Log(sequencer.beatNum + " " + notes.Count);
            spawnEventFactory = new SpawnEventFactory(enemyPrefab, this);
            riff.noteHitEvent += spawnEventFactory.HandleNoteHitEvent;
        }
    }


    public void FixedUpdate()
    {
        if (riff != null) {
            riff.Update();
        }
    }
}
