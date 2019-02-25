using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject moveplatform;
    private bool move;
    private void OnTriggerStay()	{
    	moveplatform.transform.position += moveplatform.transform.up * Time.deltaTime;
    	//moveplatform.transform.position += moveplatform.transform.right * Time.deltaTime;
    }
}
