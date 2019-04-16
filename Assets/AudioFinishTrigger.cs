using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioFinishTrigger : MonoBehaviour
{
    public AudioSource source;
    bool playing = false;
    public UnityEvent onFinish;

    public void Play() {
        playing = true;
        source.Play();
    }

    private void Update()
    {

        if (playing) {
            if (Mathf.Abs(source.time - source.clip.length) < 0.1) {
                playing = false;
                onFinish.Invoke();
            }
        }
    }
}
