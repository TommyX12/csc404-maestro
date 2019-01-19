using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFadeIn : MonoBehaviour
{
public Image img;
    // Update is called once per frame
    void Start() {
        img.color = new Color(0,0,0,0);
    }
    void Update()
    {
        img.color = Color.Lerp(img.color, new Color(1,1,1,1), Time.deltaTime);
    }
}
