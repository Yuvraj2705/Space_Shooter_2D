using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [Header("FirePoints")]
    [SerializeField] Transform[] repeaterFirePoints;
    [SerializeField] Transform[] firePoints;

    [Header("Bullets")]
    [SerializeField] Rigidbody2D BulletRepeater;
    [SerializeField] Rigidbody2D NormalBullets;

    [Header("Normal Fire Settings")]
    [SerializeField] float timer;
    [SerializeField] float spawnTime;
    [SerializeField] float timeRate;

    [Header("Normal Gun Settings")]
    [SerializeField] float nBulletSpeed = 5;
    [SerializeField] float DestroyNBulletAfter = 2;

    [Header("Repeat Timer Settings")]
    [SerializeField] float repeatTimer;
    [SerializeField] float repeatSpawnTime;
    [SerializeField] float repeatTimeRate;

    [Header("Repeater Settings")]
    [SerializeField] int RepeatCounts;
    [SerializeField] float repeaterFireRate;
    [SerializeField] float BulletSpeed;
    [SerializeField] float DestroyRepeatBulletsAt = 2;

    void Update()
    {
        Repeater();
        Shooting();
    }

    void Shooting()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            ShootingAi();
            timeRate = Random.Range(1,3);
            spawnTime = timer + timeRate;
        }
    }

    void ShootingAi()
    {
        foreach(var firePoint in firePoints)
        {
            var Instance = Instantiate(NormalBullets,firePoint.position,firePoint.rotation);
            Instance.AddForce(Vector2.down * nBulletSpeed, ForceMode2D.Impulse);
            Destroy(Instance.gameObject, DestroyNBulletAfter);
        }
    }

    void Repeater()
    {
        repeatTimer += Time.deltaTime;
        if(repeatTimer > repeatSpawnTime)
        {
            StartCoroutine(RepeaterAi());
            repeatTimeRate = Random.Range(5,10);
            repeatSpawnTime = repeatTimer + repeatTimeRate;
        }
    }

    IEnumerator RepeaterAi()
    {
        RepeatCounts = Random.Range(5,10);
        for(int i=0;i<RepeatCounts;i++)
        {
            foreach(var firePoint in repeaterFirePoints)
            {
                var Instance = Instantiate(BulletRepeater,firePoint.position,Quaternion.identity);
                Instance.AddForce(Vector2.down * BulletSpeed, ForceMode2D.Impulse);
                Destroy(Instance.gameObject, DestroyRepeatBulletsAt);                
            }
            yield return new WaitForSeconds(repeaterFireRate);
        }
        yield break;
    }
}
