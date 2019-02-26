using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToBand : MonoBehaviour
{
    public bool additive = false;
    public bool setRandom = false;
    private float average = 0;
    [Range(0, 1)]
    public float averageDecay = 0.5f;
    public int band = 0;
    public float yScale = 2;
    public float yMin = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (setRandom) {
            this.band = Random.Range(0, 8);
        }
    }

    // Update is called once per frame
    void Update()
    {
        average = average * (1 - averageDecay) + (averageDecay) * FrequencyBander.GetBand(band);
        if (!additive)
        {
            transform.localScale = new Vector3(1, Mathf.Max(yMin, average * yScale), 1);
        }
        else {
            transform.localScale = new Vector3(1, yMin + average * yScale, 1);
        }
    }
}
