using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone : MonoBehaviour
{
    public int HazardDamage = 100;

    // some things for tick rate and stuff later

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Damageable>().OnHit(HazardDamage, 1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Damageable>().OnHit(HazardDamage, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Damageable>().OnHit(HazardDamage, 1);
        }
    }
}
