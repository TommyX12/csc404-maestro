using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.transform.root.gameObject.SetActive(false);
        AgentManager.current.RemoveAgent(collision.gameObject.transform.root.GetComponent<Agent>());
    }
}
