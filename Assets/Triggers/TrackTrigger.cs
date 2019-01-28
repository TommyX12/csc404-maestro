using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackTrigger : MonoBehaviour
{
    public UnityEvent OnBeat;
    public float tol = 0.05f;
    public int bars = 1;
    int last_hit = 0;
    public List<Riff.Note> notes;
    private Riff audioRiff;

    private void Start()
    {
        audioRiff = new Riff(4, notes, MusicManager.Current);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        audioRiff.Update();
        Riff.ButtonPressResult press = audioRiff.ButtonPress();
        if (press.noteIndex != -1 && press.deltaTime < tol) {
            OnBeat.Invoke();
        }
    }
}
