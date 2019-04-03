using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameFlowManager : MonoBehaviour
{
    private GameplayModel model;

    [Inject]
    public void Construct(GameplayModel model)
    {
        this.model = model;
    }

    public void StartGame() {
        model.LevelStarted = true;
    }
}
