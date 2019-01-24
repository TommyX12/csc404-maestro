using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public  uint DamageFilter;
    public virtual void OnHit(DamageSource damage) {

    }
}
