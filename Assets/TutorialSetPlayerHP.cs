using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSetPlayerHP : MonoBehaviour
{

    public void SetHP(int amt) {
        CombatGameManager.current.player.GetComponent<PlayerAgent>().hitPoint = amt;
    }
}
