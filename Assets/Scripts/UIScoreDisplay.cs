using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIScoreDisplay : MonoBehaviour
{
    public bool updateTargetAutomatically = true;

    public string scorePrefix = "";

    public int targetNumber;
    public int currentNumber;

    public Gradient scoreColor;
    public int scoreColorMaxValue = 5000;

    public Text text;

    public float lerpRate = 1;

    public void Update()
    {
        if (updateTargetAutomatically) {
            targetNumber = ScoreManager.current.score;
        }

        int prev = currentNumber;
        currentNumber = (int)Mathf.Lerp(currentNumber, targetNumber, Time.deltaTime * lerpRate);

        if (prev == currentNumber)
        {
            currentNumber = targetNumber;
        }

        text.text = scorePrefix + currentNumber.ToString();
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
