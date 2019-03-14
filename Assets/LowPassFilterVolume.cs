using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassFilterVolume : MonoBehaviour
{
    public int frequency;

    static int collisions = 0;

    private void OnTriggerEnter(Collider other)
    {
        collisions++;
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO: temporarily disabled
        // MixerManager.current.SetTargetLowpassFreq(frequency);
    }

    private void OnTriggerExit(Collider other)
    {
        collisions--;
        if (collisions == 0) {
            // MixerManager.current.SetTargetLowpassFreq(22000);
        }
    }
}
