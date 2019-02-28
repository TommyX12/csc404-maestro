using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixTape : MonoBehaviour
{

    public void PlayMixTape()
    {
        GetComponent<Animator>().SetTrigger("play");
        GetComponent<AudioFinishTrigger>().Play();
    }
}
