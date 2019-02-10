using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DissappearingBlock : MonoBehaviour
{

    public UnityEvent OnDissappear;
    public UnityEvent OnAppear;

    public bool Appeared = true;

    public void Dissappear() {
        OnDissappear.Invoke();
        Appeared = false;
    }

    public void Appear() {
        OnAppear.Invoke();
        Appeared = true;
    }

    public void Toggle() {
        if (Appeared)
        {
            Dissappear();
        }
        else {
            Appear();
        }
    }

}
