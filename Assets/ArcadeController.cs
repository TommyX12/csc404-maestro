using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeController : MonoBehaviour
{
    public GameObject joyPivot;
    public Animator anim;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = joyPivot.transform.rotation;
        if (!anim) {
            anim = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        joyPivot.transform.rotation = initialRotation;
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        joyPivot.transform.Rotate(joyPivot.transform.right, yAxis*50);
        joyPivot.transform.Rotate(joyPivot.transform.forward, -xAxis * 50);
        if (ControllerProxy.GetButtonDown("Fire1")) {
            anim.SetTrigger("Fire");
        }
    }

}
