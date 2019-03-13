using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeTrigger : MonoBehaviour
{
    public float time = 1f;
    public bool running = false;
    public UnityEvent evt;
    public void StartTimer()
    {
        running = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (!running) {
            return;
        }

        time -= Time.deltaTime;
        if(time < 0) {
            evt.Invoke();
            Destroy(this);
        }
    }
}
