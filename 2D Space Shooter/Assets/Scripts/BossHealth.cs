using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int Health;
    [SerializeField] int ApplyMechOneAt = 80;
    private bool mechOneApplyCheck = false;

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

            if(!mechOneApplyCheck && Health < ApplyMechOneAt)
            {
                Debug.Log("Activated");
                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAi>().EnemySpawnCheck = true;
                mechOneApplyCheck = true;
            }
        }
    }
}
