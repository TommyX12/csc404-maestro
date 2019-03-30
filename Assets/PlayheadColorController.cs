using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayheadColorController : MonoBehaviour
{

    public Image image;
    public Color baseColor;
    void Update()
    {
        Color col = new Color(FrequencyBander.GetBand(5), FrequencyBander.GetBand(3), FrequencyBander.GetBand(6));
        col = baseColor - col;
        col.a = 1;
        image.color = col;
    }
}
