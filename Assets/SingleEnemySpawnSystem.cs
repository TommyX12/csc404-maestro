using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class SingleEnemySpawnSystem : MonoBehaviour
{

    private DiContainer container;

    [Inject]
    public void Construct(DiContainer container) {
        this.container = container;
    }

    private class SpawnEventFactory
    {
        GameObject prefab;
        SingleEnemySpawnSystem host;
        DiContainer container;

        public SpawnEventFactory(GameObject prefab, SingleEnemySpawnSystem host, DiContainer container)
        {
            this.prefab = prefab;
            this.host = host;
            this.container = container;
        }

        public void HandleNoteHitEvent(Riff.NoteHitEvent hit)
        {
            if (hit.noteIndex != -1)
            {
                // spawn shit
                GameObject spawned = container.InstantiatePrefab(prefab);
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
            spawnEventFactory = new SpawnEventFactory(enemyPrefab, this, container);
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
