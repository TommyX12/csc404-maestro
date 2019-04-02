using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylMenuController : MonoBehaviour
{
    public VinylMenuTextController textController;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            textController.ScrollDown();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            textController.ScrollUp();
        }
    }
}
