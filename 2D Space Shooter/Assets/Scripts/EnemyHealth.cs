using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int Health;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "FB")
        {
            Health -= 1;
            Destroy(other.gameObject);
            if(Health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
