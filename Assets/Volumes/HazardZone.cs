using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone : MonoBehaviour
{
    public int HazardDamage = 100;
    public bool Active = true;
    // some things for tick rate and stuff later

    private void DoDamage(BasicAgent other) {
        BasicAgent.Event.Damage damage;
        damage.amount = 100;
        other.ReceiveEvent(damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Active)
        {
            DoDamage(collision.gameObject.GetComponent<BasicAgent>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Active)
        {
            DoDamage(other.gameObject.GetComponent<BasicAgent>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Active)
        {
            DoDamage(other.gameObject.GetComponent<BasicAgent>());
        }
    }
}
