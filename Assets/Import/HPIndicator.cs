using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPIndicator : MonoBehaviour
{
    public Text text;

    public void Update()
    {
        if (text && Gladiator.player) {
            text.text = "HP" + Gladiator.player.HitPoints;
        }
    }
}
