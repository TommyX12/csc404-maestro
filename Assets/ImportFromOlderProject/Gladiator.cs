using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladiator : Damageable {

    public static Gladiator GetInstance() {
        return player;
    }

    public AnimationCurve motionCurve;

    public AudioSource DeathSound;

    public static Gladiator player;
    public float HitPoints = 100;
    public float speed;

    public List<GameObject> WeaponPositions;
    public List<Weapon> Weapons;

    private Quaternion rot_target;

    private Vector2 motionVal;

    public bool AddWeapon(Weapon w) {
        if (Weapons.Count < WeaponPositions.Count)
        {
            Weapons.Add(w);
            w.gameObject.transform.SetParent(WeaponPositions[Weapons.Count - 1].transform);
            w.gameObject.transform.localPosition = Vector3.zero;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnHit(int damage, int DamageFilter)
    {
        if ((DamageFilter & this.DamageFilter) != 0) {
            HitPoints -= damage;
            if (HitPoints <= 0) {
                HitPoints = 100;
                transform.position = CheckpointManager.instance.GetActiveCheckpoint().gameObject.transform.position;
                DeathSound.Play();
            }
        }
    }

    private void Start()
    {
        player = this;
    }

    public void Update()
    {
        if (Input.GetButtonDown("RB")) {
            if (Weapons.Count > 0) {
                Weapons[0].Fire();
            }
        }

        if (Input.GetButtonDown("A")) {
            MusicManager.Current.Mixer.audioMixer.SetFloat("Volume", -30);
            if (Weapons.Count > 0) {
                Weapons[0].BeginPreview();
            }
        }

        if (Input.GetButtonUp("A"))
        {
            MusicManager.Current.Mixer.audioMixer.SetFloat("Volume", -10);
            if (Weapons.Count > 0)
            {
                Weapons[0].StopPreview();
            }
        }

    }

    public void FixedUpdate()
    {

        Vector3 dl = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 dr = new Vector3(Input.GetAxisRaw("RightStickHorizontal"), 0, Input.GetAxisRaw("RightStickVertical"));

        Debug.DrawRay(transform.position, dl, Color.white);
        Debug.DrawRay(transform.position, dr, Color.red);

        Vector3 v = new Vector3(motionCurve.Evaluate(motionVal.x), 0, motionCurve.Evaluate(motionVal.y))*Time.fixedDeltaTime*speed;

        transform.position += v *speed;

        if (dr.magnitude > 0.3) {
            rot_target = Quaternion.LookRotation(dr, Vector3.up);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rot_target, Time.fixedDeltaTime * speed);

        motionVal = Vector2.Lerp(motionVal, Vector3.zero, Time.fixedDeltaTime*10);
        motionVal += new Vector2(dl.x, dl.z)*Time.fixedDeltaTime;
        motionVal = Vector2.ClampMagnitude(motionVal, 1);

    }
}
