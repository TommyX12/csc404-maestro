using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Damageable
{

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    // interp collider to model
    public GameObject Collider;
    public GameObject Model;
    public GameObject MovementIndicator;

    public AudioSource PlayerDeath;

    public float Range = 1f;

    private Riff riff;

    public override void OnHit(int damage, int DamageFilter = 0)
    {
        if ((DamageFilter & this.DamageFilter) != 0) {
            OnDeath();
        }
    }
    
    private void Init() {
        List<Riff.Note> notes = new List<Riff.Note>();
        notes.Add(new Riff.Note(0));
        notes.Add(new Riff.Note(1));
        notes.Add(new Riff.Note(2));
        notes.Add(new Riff.Note(3));
        riff = new Riff(4, notes, MusicManager.Current);
        riff.hitMarginBefore = 0.2f;
        riff.hitMarginAfter = 0.2f;
        riff.hitOffset = -0.2f;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Vector3 inputVector = new Vector3(ControllerProxy.GetAxisRaw("Horizontal"), 0, ControllerProxy.GetAxisRaw("Vertical"));

        float maxRange = Range;
        RaycastHit rayHit;
        Physics.Raycast(transform.position, inputVector, out rayHit);

        if (rayHit.collider != null) {
            maxRange = Mathf.Min(rayHit.distance, maxRange);
        }

        if (ControllerProxy.GetButton("RB")) {
            maxRange = maxRange / 2;
        }

        Debug.DrawRay(transform.position, inputVector* maxRange, Color.white);


        MovementIndicator.transform.localPosition = inputVector * maxRange + new Vector3(0, MovementIndicator.transform.localPosition.y, 0);
        // check if should move
        if (ControllerProxy.GetButtonDown("A"))
        {
            Riff.NoteHitEvent noteHit = riff.ButtonPress();
            if (noteHit.noteIndex != -1) {
                StaticAudioManager.current.GetPreviewSound().Play();
                transform.position += inputVector* maxRange;
                Model.transform.localPosition -= inputVector* maxRange;
            }
        }
    }

    private void FixedUpdate()
    {
        riff.Update();

        Model.transform.localPosition = Vector3.Lerp(Model.transform.localPosition, Vector3.zero, Time.fixedDeltaTime * 10);
    }

    void OnDeath() {
        transform.position = CheckpointManager.instance.GetActiveCheckpoint().transform.position;
        PlayerDeath.Play();
    }
}
