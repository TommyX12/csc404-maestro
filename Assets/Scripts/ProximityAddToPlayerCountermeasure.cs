using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Countermeasure))]
public class ProximityAddToPlayerCountermeasure : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject == CombatGameManager.current.player.gameObject) {
            CombatGameManager.current.player.AddCountermeasure(GetComponent<Countermeasure>());
            enabled = false;
        }
    }
}
