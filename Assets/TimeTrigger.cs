using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeTrigger : MonoBehaviour
{
    public float time = 1f;
    public UnityEvent evt;
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0) {
            evt.Invoke();
        }
    }
}
