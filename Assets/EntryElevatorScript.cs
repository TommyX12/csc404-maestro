using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryElevatorScript : MonoBehaviour
{
    
    public float LeaveTime = 5;
    public float Speed = 20;
    public float StartOffset = 30;
    public enum EntryElevatorState {
        ELEVATING_IN,
        STABLE,
        ELEVATING_OUT,
    }

    [SerializeField]
    public EntryElevatorState ElevatorState = EntryElevatorState.ELEVATING_IN;

    public GameObject[] Outerdoors;
    public GameObject PlayerStartPosition;

    public GameObject InnerProximityTrigger;

    private Vector3 cachedStartPos;


    private void Start()
    {
        foreach (GameObject game in Outerdoors)
        {
            game.transform.SetParent(null);
        }
        cachedStartPos = transform.position;
        transform.position += Vector3.down * StartOffset;
        Player.instance.transform.position = PlayerStartPosition.transform.position;
        InnerProximityTrigger.SetActive(false);
    }

    public void BeginTransition() {
        ElevatorState = EntryElevatorState.ELEVATING_OUT;
    }

    private void FixedUpdate()
    {
        switch (ElevatorState) {
            case EntryElevatorState.ELEVATING_IN:
                Vector3 delta = cachedStartPos - transform.position;
                if (delta.magnitude < Time.fixedDeltaTime * Speed)
                {
                    transform.position = cachedStartPos;
                    ElevatorState = EntryElevatorState.STABLE;
                }
                else {
                    transform.position += delta.normalized * Time.fixedDeltaTime * Speed;
                }
                break;
            case EntryElevatorState.STABLE:
                InnerProximityTrigger.SetActive(true);
                break;
            case EntryElevatorState.ELEVATING_OUT:
                LeaveTime -= Time.fixedDeltaTime;
                if (LeaveTime <= 0)
                {
                    Destroy(gameObject);
                }
                else {
                    transform.position += Vector3.down * Speed * Time.fixedDeltaTime;
                }
                break;
        }
    }

}
