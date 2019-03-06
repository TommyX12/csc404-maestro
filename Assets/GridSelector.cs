using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelector : MonoBehaviour
{
    public int width;
    public int height;

    [HideInInspector]
    public bool[] gridData;
    [HideInInspector]
    public bool dirty = true;
}