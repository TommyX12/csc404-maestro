using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone : MonoBehaviour
{
    public int HazardDamage = 100;
    public bool Active = true;
    // some things for tick rate and stuff later

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Active)
        {
            collision.gameObject.GetComponent<Damageable>().OnHit(HazardDamage, 1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Active)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Active)
        {
            other.gameObject.GetComponent<Damageable>().OnHit(HazardDamage, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Active)
        {
            other.gameObject.GetComponent<Damageable>().OnHit(HazardDamage, 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Active)
        {
            other.gameObject.GetComponent<Damageable>().OnHit(HazardDamage, 1);
        }
    }
}
