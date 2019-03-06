using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStretcher : MonoBehaviour
{
	public ParticleSystem laserImpact;
	public LineRenderer actualLaser;
	public float maxDistance;
	private Component[] children;
	private ParticleSystem laserAesthetics;

	void Start()
	{
		children = GetComponentsInChildren<ParticleSystem>(); 
		foreach (ParticleSystem childParticleSystem in children){
        	if (childParticleSystem.name == "LaserAesthetics"){
            	laserAesthetics = childParticleSystem;
        	}
        }
        // Set initial position
    	actualLaser.SetPosition(0, transform.position);
    	actualLaser.enabled = true;
    	//laserImpact.Play();
	}

    // Update is called once per frame
    void Update()
    {
    	int layerMask = 1 << 10;
    	layerMask = ~layerMask;
    	ParticleSystem.MainModule aestheticsMain;
    	//ParticleSystem.ShapeModule aestheticsShape;

    	// Get the effects of the "woosh lines", if it exists.
    	if(laserAesthetics){
    		aestheticsMain = laserAesthetics.main;
    		//aestheticsShape = laserAesthetics.shape;
    	}

    	actualLaser.SetPosition(0, transform.position);

        // For storing raycast results.
        RaycastHit hit;
        bool contact = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask);
        if (contact){
        	actualLaser.SetPosition(1, hit.point);
        	laserImpact.transform.position = hit.point;
        	if(laserAesthetics){
    			aestheticsMain.startSpeed = hit.distance * 2;
    		}
        }
        else{
        	Vector3 endPoint = transform.position + (transform.forward * maxDistance);
        	actualLaser.SetPosition(1, endPoint);
        	laserImpact.transform.position = endPoint;
        	if(laserAesthetics){
    			aestheticsMain.startSpeed = maxDistance * 2;
    		}
        }
    }
}
