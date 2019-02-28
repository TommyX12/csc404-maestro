using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIScoreDisplay : MonoBehaviour
{
    public bool updateTargetAutomatically = true;

    public int targetNumber;
    public int currentNumber;

    public Gradient scoreColor;
    public int scoreColorMaxValue = 5000;

    public Text text;

    public static UIScoreDisplay instance;

    public float lerpRate = 1;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (updateTargetAutomatically) {
            targetNumber = ScoreManager.current.score;
        }

        currentNumber = (int)Mathf.Lerp(currentNumber, targetNumber, Time.deltaTime * lerpRate);
        text.text = currentNumber.ToString();
        text.color = scoreColor.Evaluate(Mathf.Min(1, 1 - (float)currentNumber / scoreColorMaxValue));
    }

    public void SetTarget(int target) {
        this.targetNumber = target;
        updateTargetAutomatically = false;
    }

    public void UpdateAutomatically() {
        updateTargetAutomatically = true;
    }

}
