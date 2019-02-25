using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBander : MonoBehaviour
{
    private static float[] samples = new float[512];
    private static float[] bands = new float[8];

    private void Start()
    {
        
    }

    private void Update()
    {
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        int count = 0;
        for (int i = 0; i < bands.Length; i++) {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            float average = 0;
            if (i == 7) {
               sampleCount +=2;
            }
            for (int j = 0; j < sampleCount; j++) {
                average += samples[count] * (count+1);
                count++;
            }
            average /= sampleCount;
            bands[i] = average;
        }
    }

    public static float GetBand(int band) {
        return bands[band];
    }

}
