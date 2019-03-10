using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public float lateTolerance = 0.1f;
    public GameObject flamePrefab;
    public GridSelector selector;
    public Sequencer sequencer;
    public List<GameObject> targets;
    private float time = 0;
    private bool played = false;

    private void Start()
    {
        for (int i = 0; i < sequencer.data.Length; i++) {
            if (sequencer.data[i]) {
                time = sequencer.GetTime(i);
                break;
            }
        }
    }

    private void Update()
    {
        if (Scrubber.instance && Scrubber.instance.source) {
            if (Scrubber.instance.source.time - time >= 0 && Scrubber.instance.source.time - time < lateTolerance) {
                played = true;
                for (int i = 0; i < selector.height; i++) {
                    for (int j = 0; j < selector.width; j++) {
                        if (selector.gridData[j + i * selector.width])
                        {
                            Vector3 targetPos = targets[j + i * selector.width].transform.position;
                            GameObject go = GameObject.Instantiate(flamePrefab);
                            go.transform.position = targetPos;
                        }
                    }
                }
            }
        }
        if (played)
        {
            this.enabled = false;
        }
    }
}
