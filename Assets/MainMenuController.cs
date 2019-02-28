using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    private void Update()
    {
        if (ControllerProxy.GetButton("Fire1")) {
            SceneManager.LoadScene("Alpha");
        }
    }
}
