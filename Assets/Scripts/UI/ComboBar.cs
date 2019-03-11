using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using Zenject;

public class ComboBar : MonoBehaviour
{
    // Fields
    private float lastScore = 0;
    private float colorEffectFactor = 0;
    private Color colorEffectColor = new Color(1, 1, 1);

    // Self references
    public Text _scoreText;

    // Injected references
    private GameplayModel model;
    private GlobalConfiguration config;

    // Methods

    private void Awake()
    {
        Assert.IsNotNull(_scoreText);
    }

    [Inject]
    public void Construct(GameplayModel model,
                          GlobalConfiguration config)
    {
        this.model = model;
        this.config = config;
    }

    private void UpdateColor()
    {
        if (colorEffectFactor > 0)
        {
            colorEffectFactor -= Time.deltaTime;
        }
        else
        {
            colorEffectFactor = 0;
        }

        _scoreText.color = config.ScoreIdleColor + (colorEffectFactor / config.ScoreColorEffectDuration) * (colorEffectColor - config.ScoreIdleColor);
    }

    private void Update()
    {
        var score = model.combo;
        if (score != lastScore)
        {
            _scoreText.text = "x" + score.ToString("0");
            colorEffectFactor = config.ScoreColorEffectDuration;
            if (score > lastScore)
            {
                colorEffectColor = config.ScoreIncreaseColor;
            }
            else if (score < lastScore)
            {
                colorEffectColor = config.ScoreDecreaseColor;
            }
        }
        lastScore = score;

        UpdateColor();
    }
}
