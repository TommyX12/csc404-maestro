using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKiller : MonoBehaviour
{
    private ParticleSystem PS;
    //private Light loghtLoc;
    //public Light lightComp;
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        //loghtLoc = Instantiate(lightComp, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(PS){
        	if(!PS.IsAlive())
        	{
        		//Destroy(loghtLoc)
        		Destroy(gameObject);
        	}
        }
    }
}
