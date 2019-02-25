using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizerTest : MonoBehaviour
{
    private static float[] samples = new float[512];
    public GameObject cubePrefab;

    [Range(0,50)]
    public float yScale = 1;

    public float yMax = 100;

    private List<GameObject> cubes;

    // Start is called before the first frame update
    void Start()
    {
        cubes = new List<GameObject>();
        for(int i = 0; i < samples.Length; i ++) {
            cubes.Add(GameObject.Instantiate(cubePrefab));
            cubes[i].transform.position = transform.position + Vector3.right * i;
            cubes[i].transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < samples.Length; i++) {
            cubes[i].transform.localScale = new Vector3(1, Mathf.Min(yMax, yScale*samples[i]), 1);
        }
    }
}
