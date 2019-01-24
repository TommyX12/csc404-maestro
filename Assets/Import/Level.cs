using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level ActiveLevel;

    [Range(-10,10)]
    public float AmbientCrowdExcitement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ActiveLevel = this;
    }
}
