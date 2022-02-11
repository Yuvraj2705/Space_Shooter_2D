using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceShipHealth>().health = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceShipHealth>().maxHealth;
            Destroy(gameObject);
        }
    }
}
