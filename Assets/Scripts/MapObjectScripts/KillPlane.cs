using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillPlane : MonoBehaviour
{

    private void Update()
    {
        if (CombatGameManager.current.player) {
            PlayerAgentController player = CombatGameManager.current.player;
            if (player.transform.position.y < this.transform.position.y) {
                player.gameObject.GetComponent<Agent>().ReceiveEvent(
                    new Agent.Event.Damage { amount = 100 });
            }
        }
    }
}
