using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class ETOAI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Rigidbody2D Bullet;

    [Header("Player Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float force = 5f;
    [SerializeField] float destroyAfter;

    [Header("Fire Settings")]
    [SerializeField] float Intervals;
    [SerializeField] Transform[] FirePoints;
    float Timer;
    float SpawnTime; 

    [Header("Bullet Settins")]
    [SerializeField] float BulletSpeed; 
    [SerializeField] float DestroyBulletAfter = 4;

    public Node.Status treeStatus = Node.Status.RUNNING;

    Vector3 StayPoint;
    private Rigidbody2D rb;
    RootNode rootNode;
    float distanceBtwDestin = 0.0f;

    void Start()
    {
        InitVariables();

        rootNode = new RootNode();

        Sequence enemyAi = new Sequence("Enemy Ai");
        Leaf setPoint = new Leaf("Set Point", SetPoint);
        Leaf goToTarget = new Leaf("Go to Target", GoToTarget);
        Leaf suicideMechAi = new Leaf("Suicide Mech", SuicideMech);
        Leaf destruction = new Leaf("Destruction", Destruction);
        
        enemyAi.AddChild(setPoint);
        enemyAi.AddChild(goToTarget);
        //enemyAi.AddChild(destruction);
        rootNode.AddChild(enemyAi);
    }

    public Node.Status SetPoint()
    {
        StayPoint = GameObject.Find("Spawner").GetComponent<Spawner>().tempHoldPoint;
        return Node.Status.SUCCESS;
    }

    public Node.Status SuicideMech()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        return Node.Status.SUCCESS;
    }

    public Node.Status Destruction()
    {
        Destroy(gameObject,destroyAfter);
        return Node.Status.SUCCESS;
    }

    public Node.Status GoToTarget()
    {
        return SetDestination(StayPoint);
    }

    Node.Status SetDestination(Vector3 destination)
    {
        
        distanceBtwDestin = Vector3.Distance(transform.position,destination);

        if(distanceBtwDestin > 0.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination,speed * Time.deltaTime);
            return Node.Status.RUNNING;
        }
        if(distanceBtwDestin < 0.5)
        {
            return Node.Status.SUCCESS;
        }

        return Node.Status.RUNNING;
    }

    void InitVariables()
    {
        SpawnTime = Intervals;
        transform.Rotate(new Vector3(0,0,180));
    }

    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
        
        /*
        *Continuous Shooting Timer 
        */
        Timer += Time.deltaTime;
        if(Timer > SpawnTime)
        {
            ContinuousFiring();   
            Intervals = Random.Range(1,6);
            SpawnTime = Timer + Intervals;
        }
    }

    void ContinuousFiring()
    {
        foreach(var firePoint in FirePoints)
        {
            var Instance = Instantiate(Bullet, firePoint.position, firePoint.rotation);
            Instance.AddForce(Vector2.down * BulletSpeed, ForceMode2D.Impulse);
            Destroy(Instance.gameObject,DestroyBulletAfter);
        }
    }
}
