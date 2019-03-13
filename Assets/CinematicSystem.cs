using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSystem : MonoBehaviour
{
    public static CinematicSystem current;

    GameObject target = null;
    bool cinematicMode = false;
    int idx = 0;

    private Dictionary<string, GameObject> cameraPositions = new Dictionary<string, GameObject>();

    public void StartCinematicCamera() {
        ControllerProxy.inputEnabled = false;
        cinematicMode = true;
        MainCamera.current.enabled = false;
    }

    public void StopCinematicCamera() {
        ControllerProxy.inputEnabled = true;
        cinematicMode = false;
        MainCamera.current.enabled = true;
    }

    public void SetActiveCameraPosition(string name) {
        if (!cinematicMode) {
            StartCinematicCamera();
        }
        Debug.Log(name);
        target = cameraPositions[name];
    }

    public void RegisterCinematicCameraPosition(CinematicCameraPosition pos, string name) {
        cameraPositions[name] = pos.gameObject;
    }

    private void Update()
    {
        // interp
        if (cinematicMode && target != null) {
            MainCamera.current.transform.position = Vector3.Lerp(MainCamera.current.transform.position, target.transform.position, Time.deltaTime);
            MainCamera.current.transform.rotation = Quaternion.Lerp(MainCamera.current.transform.rotation, target.transform.rotation, Time.deltaTime);
        }
    }

    private void Awake()
    {
        current = this;
    }

}
