using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Weapon))]
public class ProximityAddToPlayerWeapon : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (other.gameObject.GetComponent<Gladiator>().AddWeapon(GetComponent<Weapon>())) {
                GetComponent<Weapon>().enabled = true;
                this.enabled = false;
            }
        }
    }
}
