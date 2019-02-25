using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        PlayerAgent player = collision.gameObject.GetComponent<PlayerAgent>();
        player.ReceiveEvent(new Agent.Event.Damage() { amount = 8 });
    }
}
