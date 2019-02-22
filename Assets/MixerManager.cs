using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerManager : MonoBehaviour
{
    public MixerManager current;

    private void Awake()
    {
        current = this;
    }
}
