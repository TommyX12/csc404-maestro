using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardMenuController : MonoBehaviour
{
    public VinylMenuController controller;
    public LeaderboardUI leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        controller.OnChange += OnChange;
    }

    void OnChange() {
    }
}
