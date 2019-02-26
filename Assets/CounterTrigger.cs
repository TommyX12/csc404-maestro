using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CounterTrigger : MonoBehaviour
{
    public int countTarget;
    public int count;

    public UnityEvent onCounted;
    public UnityEvent onCount;

    public bool repeat = false;

    public void Increment() {
        count++;
        onCount.Invoke();
        if (count == countTarget) {
            onCounted.Invoke();
        }
        if (count > countTarget && repeat) {
            onCounted.Invoke();
        }
    }

    public void IncrementByAmount(int amt) {
        count+=amt;
        onCount.Invoke();
        if (count == countTarget)
        {
            onCounted.Invoke();
        }
        if (count > countTarget && repeat)
        {
            onCounted.Invoke();
        }
    }

}
