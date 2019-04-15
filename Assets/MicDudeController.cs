using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MicDudeController : MonoBehaviour
{
    public float switchTime = 3;
    private float switchTimer;
    public int dances;
    private Animator animator;

    public List<GameObject> colorables;
    public List<Material> colors;

    // Start is called before the first frame update
    void Start()
    {
        switchTimer = switchTime;
        animator = GetComponent<Animator>();
        animator.SetInteger("dance", Random.Range(0, dances));
        Material color = colors[Random.Range(0, colors.Count)];
        foreach (GameObject go in colorables) {
            go.GetComponent<Renderer>().material = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTimer <= 0) {
            switchTimer = switchTime;
            animator.SetInteger("dance", Random.Range(0, dances));
        }
        switchTimer -= Time.deltaTime;
    }
}
