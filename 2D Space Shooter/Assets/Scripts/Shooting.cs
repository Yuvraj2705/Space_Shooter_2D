using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] Rigidbody2D Bullet_01;
    [SerializeField] Rigidbody2D Bullet_02;
    [SerializeField] Rigidbody2D Bullet_03;

    [Header("FirePoints")]
    [SerializeField] Transform[] FirePoint_Set_One;
    [SerializeField] Transform[] FirePoint_Set_Two;
    [SerializeField] Transform[] FirePoint_Set_Three;

    [Header("BulletSettings")]
    [SerializeField] float BulletSpeed;
    [SerializeField] float DestroyBulletTime = 1.5f;
    [SerializeField] float bulletFireRate = 0.3f;

    [Header("Player Settings")]
    [SerializeField] float timeBetweenRepeat;

    [Header("Timer Settings")]
    float Timer;
    [SerializeField] float timeRate = 0.3f;
    float timeBase;

    float spawnTime;
    int GunSwitcher = 1;

    void Awake()
    {
        spawnTime = timeBetweenRepeat;
        timeBase = timeRate; 
    }

    void Update()
    {
        LocalDebugger();
        ShootAIAdvance();
        //ShootAINoRepeat();
        //ShootAI();
    }

    void ShootAI()
    {
        if(Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("ContinuousShooting",0.001f,bulletFireRate);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            CancelInvoke("ContinuousShooting");
        }
    }

    void ShootAINoRepeat()
    {
        if(Time.timeSinceLevelLoad > spawnTime)
        {
            if(Input.GetMouseButton(0))
            {
                ContinuousShooting();
            }
            spawnTime = Time.timeSinceLevelLoad + bulletFireRate;
        }
    }

    void ShootAIAdvance()
    {
        if(Input.GetMouseButton(0))
        {
            Timer += Time.deltaTime;
            if(Timer > timeBase)
            {
                ContinuousShooting();
                timeBase = Timer + timeRate;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            timeBase = timeRate;
            Timer = 0;
        }
    }

    void ContinuousShooting()
    {
        if(GunSwitcher == 1)
        {
            foreach(var FirePoint in FirePoint_Set_One)
            {
                var BulletInstance = Instantiate(Bullet_01, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 2)
        {
            foreach(var FirePoint in FirePoint_Set_Two)
            {
                var BulletInstance = Instantiate(Bullet_01, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 3)
        {
            foreach(var FirePoint in FirePoint_Set_Three)
            {
                var BulletInstance = Instantiate(Bullet_01, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 4)
        {
            foreach(var FirePoint in FirePoint_Set_One)
            {
                var BulletInstance = Instantiate(Bullet_02, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 5)
        {
            foreach(var FirePoint in FirePoint_Set_Two)
            {
                var BulletInstance = Instantiate(Bullet_02, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 6)
        {
            foreach(var FirePoint in FirePoint_Set_Three)
            {
                var BulletInstance = Instantiate(Bullet_02, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 7)
        {
            foreach(var FirePoint in FirePoint_Set_One)
            {
                var BulletInstance = Instantiate(Bullet_03, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 8)
        {
            foreach(var FirePoint in FirePoint_Set_Two)
            {
                var BulletInstance = Instantiate(Bullet_03, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
        else if(GunSwitcher == 9)
        {
            foreach(var FirePoint in FirePoint_Set_Three)
            {
                var BulletInstance = Instantiate(Bullet_03, FirePoint.position, FirePoint.rotation);
                BulletInstance.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                Destroy(BulletInstance.gameObject,DestroyBulletTime);
            }
        }
    }

    void LocalDebugger()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(GunSwitcher == 9)
            {
                GunSwitcher = 9;
            }
            else
            {
                GunSwitcher += 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(GunSwitcher == 1)
            {
                GunSwitcher = 1;
            }
            else
            {
                GunSwitcher -= 1;
            }
        }
    }
}
