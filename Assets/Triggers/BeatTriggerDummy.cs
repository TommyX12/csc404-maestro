using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BeatTriggerDummy : MonoBehaviour
{
    public UnityEvent OnBeat;
    public float time;
    private float timer;
    private void Start()
    {
        timer = time;
    }

    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0) {
            timer += time;
            OnBeat.Invoke();
        }
    }
}
