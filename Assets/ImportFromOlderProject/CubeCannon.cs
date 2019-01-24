using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCannon : MonoBehaviour
{
    public GameObject Cube;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            FireCube();
        }
    }

    public void FireCube() {
        GameObject g = Instantiate(Cube);
        g.transform.position = transform.position;
        g.GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(10, 20);
    }
}
