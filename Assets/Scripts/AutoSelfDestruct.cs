using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSelfDestruct : MonoBehaviour {

    [SerializeField]
    public float Delay = 5.0f;

    private void Update() {
        Delay -= Time.deltaTime;
        if (Delay <= 0) {
            GameObject.Destroy(this.gameObject);
        }
    }
    
}
