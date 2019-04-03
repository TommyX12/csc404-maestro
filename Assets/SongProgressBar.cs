using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongProgressBar : MonoBehaviour
{
    public AudioSource source;
    public Image image;
    public RectTransform bar;
    public RectTransform playhead;
    private void Start()
    {
        image.sprite = Sprite.Create(Util.AudioWaveform(source.clip, 400, 100, Color.white), new Rect(0,0,400,100), new Vector2(0.5f, 0.5f));
    }

    private void Update()
    {                              
        Vector3 position = playhead.anchoredPosition;
        position.x = bar.sizeDelta.x * (source.time / source.clip.length);
        // Debug.Log(bar.sizeDelta.x); // understandable, have a good day
        playhead.anchoredPosition = position;
    }
}   
