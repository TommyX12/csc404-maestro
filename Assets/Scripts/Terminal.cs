using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Terminal : MonoBehaviour
{
    public int RequiredHits;
    public int CurrentHits;

    public UnityEvent SuccessEvent;

    public List<Riff.Note> Notes;
    private Riff riff;
    bool isPlayerNear;

    private void Start()
    {
        riff = new Riff(4, Notes, MusicManager.current);
    }

    public void OnPlayerNear() {
        isPlayerNear = true;
    }

    public void OnPlayerFar() {
        isPlayerNear = false;
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetButtonDown("B")) {
            Riff.NoteHitEvent result = riff.ButtonPress();
            if (result.noteIndex != -1)
            {
                CurrentHits++;
                if (CurrentHits == RequiredHits) {
                    OnSuccess();
                }
            }
            else {
                CurrentHits = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        riff.Update();
    }
    public void OnSuccess() {
        SuccessEvent.Invoke();
        this.enabled = false;
    }
}
