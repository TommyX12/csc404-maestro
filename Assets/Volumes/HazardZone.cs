using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone : MonoBehaviour
{
    public int HazardDamage = 100;
    public bool Active = true;
    public float activeTime = 0.4f;
    // some things for tick rate and stuff later

    private void Update()
    {
        activeTime -= Time.deltaTime;
        if (activeTime < 0) {
            this.Active = false;
            this.enabled = false;
        }
    }

    private void DoDamage(BasicAgent other) {
        BasicAgent.Event.Damage damage = new BasicAgent.Event.Damage();
        damage.amount = HazardDamage;
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
