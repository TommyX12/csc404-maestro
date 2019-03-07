using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public string sceneToLoad = "Alpha";

    private void Update()
    {
        if (ControllerProxy.GetButton("Fire1")) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
