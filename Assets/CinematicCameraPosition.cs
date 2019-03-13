using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraPosition : MonoBehaviour
{
    public string positionName = "";

    private void Start()
    {
        CinematicSystem.current.RegisterCinematicCameraPosition(this, positionName);
    }
}
