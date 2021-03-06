using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int Health;
    [SerializeField] int ApplyMechOneAt = 80;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject ExplosionSoundObject;
    private bool mechOneApplyCheck = false;
    private Vector3 randomPos;
    int count = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "FB")
        {
            Health -= 1;
            Destroy(other.gameObject);
            if(Health == 0)
            {
                Destroy(gameObject);
                var sound = Instantiate(ExplosionSoundObject, transform.position, Quaternion.identity);
                Destroy(sound,4);
                //InvokeRepeating("ExplosionPlus",0.001f,0.5f);
                StartCoroutine(Explosion());
            }

            if(!mechOneApplyCheck && Health < ApplyMechOneAt)
            {
                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAi>().EnemySpawnCheck = true;
                mechOneApplyCheck = true;
            }
        }
    }
    void ExplosionPlus()
    {
        if(count == 5)
        {
            CancelInvoke("ExplosionPlus");
        }
        randomPos = new Vector3(Random.Range(-3,3),Random.Range(3,-3),1);
        var Instance = Instantiate(explosion, transform.position + randomPos, Quaternion.identity);
        Destroy(Instance.gameObject,4);
        Debug.Log("Explosion");
        count++;
    }

    IEnumerator Explosion()
    {
        for(int i=0; i<10; i++)
        {
            randomPos = new Vector3(Random.Range(-3,3),Random.Range(3,-3),1);
            var Instance = Instantiate(explosion, transform.position + randomPos, Quaternion.identity);
            Destroy(Instance.gameObject,1);
        }
        yield break;
    }
}
