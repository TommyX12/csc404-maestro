using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScreen : MonoBehaviour
{
    public SpriteRenderer display;
    public List<Sprite> frames;
    private int idx;
    private bool active = true;
    private void Start()
    {
        if (frames == null || frames.Count < 1) {
            active = false;
        }
        idx = -1;
    }

    public void nextImage() {
        idx = (idx + 1) % frames.Count;
        display.sprite = frames[idx];
    }
}
