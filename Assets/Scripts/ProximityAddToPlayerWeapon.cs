using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Weapon))]
public class ProximityAddToPlayerWeapon : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject == CombatGameManager.current.player.gameObject) {
            CombatGameManager.current.player.AddWeapon(GetComponent<Weapon>());
            enabled = false;
        }
    }
}
