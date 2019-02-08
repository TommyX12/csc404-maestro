using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillPlane : MonoBehaviour
{

    private void Update()
    {
        if (Player.instance) {
            if (Player.instance.transform.position.y < this.transform.position.y) {
                Player.instance.OnHit(9999, 1);
            }
        }
    }
}
