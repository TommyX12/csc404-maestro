using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuController : MonoBehaviour
{

    private List<AudioSource> paused = new List<AudioSource>();

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void Toggle() {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;
            foreach (AudioSource s in paused)
            {
                s.UnPause();
            }
            paused.Clear();
        }
        else {
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
            AudioSource[] sources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource s in sources)
            {
                if (s.isPlaying)
                {
                    s.Pause();
                    paused.Add(s);
                }
            }
        }
    }
}
