using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatTutorial : BasicAgent
{

    bool ready = false;
    Riff riff;
    private List<BeatTutorialDisplay> displays = new List<BeatTutorialDisplay>();
    private int noteNum;
    private int noteIndex;
    private bool[] hits;

    public GameObject start;
    public GameObject end;
    public GameObject pos;
    public GameObject displayPrefab;

    public AudioSource failSound;
    public AudioSource successSound;

    public UnityEvent onSuccess;

    // Start is called before the first frame update
    public void SetRiffToPlayerWeapon() {
        riff = CombatGameManager.current.player.GetCurrentWeapon().GetRiff();
        noteNum = riff.GetNotes().Count;
        noteIndex = 0;
        hits = new bool[noteNum];
        Vector3 diff = end.transform.position - start.transform.position;
        Quaternion rot = pos.transform.rotation;
        for (int i = 0; i < noteNum; i++) {
            GameObject display = GameObject.Instantiate(displayPrefab);
            displays.Add(display.GetComponent<BeatTutorialDisplay>());
            display.transform.position = start.transform.position + (diff / noteNum) * i;
            display.transform.rotation = rot;
            displays[i].SetState(2);
            display.transform.SetParent(transform);
        }

        riff.noteHitEvent += OnBeat;
        riff.delayedNoteHitEvent += OnDelayedBeat;

        GetComponent<Collider>().enabled = true;
        ready = true;
    }

    new void Start()
    {
        base.Start();
        AgentManager.current.AddAgent(this);
    }

    void OnDelayedBeat(Riff.NoteHitEvent hit) {
        if (!this.enabled || hit.noteIndex == -1)
        {
            return;
        }
        displays[hit.noteIndex].Pulse();
    }

    void OnBeat(Riff.NoteHitEvent hit) {
        if (!this.enabled || hit.noteIndex == -1) {
            return;
        }
        noteIndex = hit.noteIndex;
        if (hit.noteIndex == 0)
        {
            for (int i = 0; i < noteNum; i++)
            {
                if (!hits[i])
                {
                    // play fail sound
                    failSound.Play();
                    foreach (BeatTutorialDisplay d in displays) {
                        d.SetState(2);
                    }
                    break;
                }

                if (hits[i] && i == noteNum - 1)
                {
                    // success
                    foreach (BeatTutorialDisplay d in displays)
                    {
                        d.SetState(2);
                        d.Pulse();
                    }
                    successSound.Play();
                    onSuccess.Invoke();
                    this.enabled = false;
                    riff.delayedNoteHitEvent -= OnDelayedBeat;
                    riff.noteHitEvent -= OnBeat;
                    GameObject.Destroy(gameObject, 1f);
                    return;
                }

                // clear
                hits[i] = false;
            }
        }
        else {
            displays[noteIndex - 1].SetState(hits[noteIndex-1] ? 1 : 0);
        }
    }

    public override void ReceiveEvent(Event.Damage damage)
    {
        if (!this.enabled || !this.ready)
        {
            return;
        }
        hits[noteIndex] = true;
        displays[noteIndex].SetState(1);
    }
}
