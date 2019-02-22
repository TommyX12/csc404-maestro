using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class FastInput : MonoBehaviour
{

    public UnityEvent OnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            OnButton.Invoke();
        }
    }
}
