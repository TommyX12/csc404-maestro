using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 2;   
    }
}
