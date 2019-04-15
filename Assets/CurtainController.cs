using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    public float targetScale;
    public float interpTime;
    private float interpTimer;
    private float delta;
    // Start is called before the first frame update
    void Start()
    {
        delta = Mathf.Abs(transform.localScale.x - targetScale);
        interpTimer = interpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (interpTimer < 0) {
            Destroy(this);
        }
        transform.localScale = new Vector3(targetScale + delta * (interpTimer / interpTime), transform.localScale.y, transform.localScale.z);
        interpTimer -= Time.deltaTime;
    }
}
