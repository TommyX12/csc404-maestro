using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public float Delay = 1;

    public string sceneName;

    bool Load = false;

    private void Update()
    {
        if (Load) {
            if (Delay > 0)
            {
                Delay -= Time.deltaTime;
            }
            else {
                SceneManager.LoadScene(sceneName);
            }
        }
 
    }

    public void Reload() {
        Load = true;
    }
}
