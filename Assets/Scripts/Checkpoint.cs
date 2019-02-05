using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioSource ActivationSound;
    public Material OffMaterial;
    public Material OnMaterial;
    public bool Active = false;
    private int id;

    private void Start()
    {
        id = CheckpointManager.instance.AddCheckpoint(this);
        if (DefaultCheckpoint) {
            MakeActiveCheckpoint();
        }
    }

    public MeshRenderer OnOffPlatform;

    public bool DefaultCheckpoint = false;

    public void MakeActiveCheckpoint() {
        if (Active) {
            return;
        }
        ActivationSound.Play();
        CheckpointManager.instance.SetActiveCheckpoint(id);
        OnOffPlatform.material = OnMaterial;
        Active = true;
    }

    public void UnMakeActiveCheckpoint() {
        OnOffPlatform.material = OffMaterial;
        Active = false;
    }
}
