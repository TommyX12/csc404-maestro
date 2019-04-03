using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardMenuController : MonoBehaviour
{
    public VinylMenuController controller;
    public VinylMenuTextController text_controller;
    public LeaderboardUI leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        controller.OnChange += OnChange;
    }

    void OnChange() {
        if (text_controller.absoluteSelection > 0)
        {
            int song_idx = text_controller.absoluteSelection - 1;
            var level = GlobalConfiguration.Current.GetLevel(song_idx);
            leaderboard.saveFile = level.leaderboardFile;
            leaderboard.ShowLeaderboard();
        }
        else {
            leaderboard.saveFile = "";
            leaderboard.ShowLeaderboard();
        }
    }
}
