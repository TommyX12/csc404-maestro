using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuController : MonoBehaviour
{

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void Toggle() {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else {
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
