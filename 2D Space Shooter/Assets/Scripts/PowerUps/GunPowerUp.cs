using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().GunSwitcher += 1;
            Destroy(gameObject);
        }
    }
}
