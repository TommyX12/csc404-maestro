using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladiator : Damageable {

    public static Gladiator GetInstance() {
        return player;
    }

    public AnimationCurve motionCurve;

    public static Gladiator player;
    public float HitPoints = 100;
    public float speed;

    public List<GameObject> WeaponPositions;
    public List<Weapon> Weapons;

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
        }
    }

    private void Start()
    {
        player = this;
    }

    public void Update()
    {
        // refactor in an input class!
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
        // Code that gets the player moving. Uses the motion curve for a "nice feel"
        Vector3 dl = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Debug.DrawRay(transform.position, dl, Color.white);
        Vector3 v = new Vector3(motionCurve.Evaluate(motionVal.x), 0, motionCurve.Evaluate(motionVal.y))*Time.fixedDeltaTime*speed;
        transform.position += v *speed;
        motionVal = Vector2.Lerp(motionVal, Vector3.zero, Time.fixedDeltaTime*10);
        motionVal += new Vector2(dl.x, dl.z)*Time.fixedDeltaTime;
        motionVal = Vector2.ClampMagnitude(motionVal, 1);
    }
}
