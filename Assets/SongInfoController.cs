using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongInfoController : MonoBehaviour
{
    public VinylMenuController menu;
    public VinylMenuTextController controller;
    public Text songTitle;
    public Text artistTitle;
    public List<LEDLightController> difficultyLights;
    public AudioSource player;

    private void Start()
    {
        menu.OnChange += OnChange;
    }

    void OnChange() {
        if (controller.absoluteSelection > 0)
        {
            int song_idx = controller.absoluteSelection - 1;
            var level = GlobalConfiguration.Current.GetLevel(song_idx);
            songTitle.text = level.DisplayName;
            artistTitle.text = "Artist: " + level.artists;
            for (int i = 0; i < 5; i++)
            {
                if (i < level.difficulty)
                {
                    difficultyLights[i].TurnOn();
                }
                else
                {
                    difficultyLights[i].TurnOff();
                }
            }

            player.clip = ResourceManager.GetMusic(level.audioFile);
            player.Play();
            player.time = Random.Range(0, player.clip.length / 2f);
        }
        else
        {
            songTitle.text = "-----";
            artistTitle.text = "Artist: -----";
            for (int i = 0; i < 5; i++)
            {
                difficultyLights[i].TurnOff();
            }
            player.Stop();
        }
    }

}
