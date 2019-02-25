using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToBand : MonoBehaviour
{
    private float average = 0;
    [Range(0, 1)]
    public float averageDecay = 0.5f;
    public int band = 0;
    public float yScale = 2;
    public float yMin = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        average = average * (1 - averageDecay) + (averageDecay) * FrequencyBander.GetBand(band);
        transform.localScale = new Vector3(1, Mathf.Max(yMin,average*yScale), 1);
    }
}
