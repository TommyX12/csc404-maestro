using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationMenuController : MonoBehaviour
{
    public GameObject startPos;
    public GameObject endPos;
    public GameObject slider;

    private Vector3 delta;

    private void Start()
    {
        delta = endPos.transform.position - startPos.transform.position;
    }

    private void Update()
    {
        slider.transform.position = startPos.transform.position + delta * GlobalConfiguration.Current.AudioDelay/(GlobalConfiguration.Current.AudioDelayMax - GlobalConfiguration.Current.AudioDelayMin);
    }
}
