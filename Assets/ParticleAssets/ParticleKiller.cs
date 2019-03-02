using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{
	private ParticleSystem PS;
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PS){
        	if(PS.particleCount == 0 || !PS.IsAlive())
        	{
        		Destroy(gameObject);
        	}
        }
    }
}
