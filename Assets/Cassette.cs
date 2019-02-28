using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cassette : MonoBehaviour
{

    public string authorName;
    public string songName;

    public Text authorLabel;
    public Text songLabel;

    private void Start()
    {
        SetAuthor(authorName);
        SetSong(songName);
    }

    public void SetAuthor(string author) {
        authorLabel.text = author;
    }
    public void SetSong(string song) {
        songLabel.text = song;
    }
}
