using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public ParticleGroup scoreParticles;

    public GameObject floatingScorePrefab;

    public static ScoreManager current;
    public int[] starScoreThresholds;
    public UIScoreDisplay scoreDisplay;

    public int score = 0;

    private void Awake()
    {
        current = this;
    }

    // public void AddScore(int score) {
    //     this.score += score;
    // }
}
