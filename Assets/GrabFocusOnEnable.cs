using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GrabFocusOnEnable : MonoBehaviour
{
    public Selectable comp;
    private void OnEnable()
    {
        comp.Select();
    }
}
