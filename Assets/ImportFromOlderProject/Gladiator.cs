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

    public override void OnHit(DamageSource damage)
    {
        if ((damage.DamageFilter & this.DamageFilter) != 0) {
            HitPoints -= damage.DamageAmount;
        }
    }

    private void Start()
    {
        player = this;
    }

    public void Update()
    {
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
