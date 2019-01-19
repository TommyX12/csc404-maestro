using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteScroller : MonoBehaviour
{
    public float dv;
    private RectTransform rt;
    public Image img;
    public Text txt;
    void Start() {
        rt = GetComponent<RectTransform>();
    }

    void FixedUpdate() {
        Vector2 vec = rt.anchoredPosition;
        vec.x -= dv*Time.fixedDeltaTime;
        rt.anchoredPosition = vec;
    }
}
