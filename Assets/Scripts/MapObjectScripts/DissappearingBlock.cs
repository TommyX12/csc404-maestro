using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class DissappearingBlock : RhythmObject
{
    private bool appeared = true;

    MeshRenderer rendy;
    Collider collidy;

    private void Start()
    {
        Init();
        rendy = GetComponent<MeshRenderer>();
        collidy = GetComponent<Collider>();
    }

    protected override void RhythmAction(Riff.NoteHitEvent e)
    {
        rendy.enabled = !appeared;
        collidy.enabled = !appeared;
        appeared = !appeared;
    }
}
