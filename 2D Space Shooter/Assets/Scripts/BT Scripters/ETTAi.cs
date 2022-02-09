using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ETTAi : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Rigidbody2D Bullet;

    [Header("Player Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float force = 5f;
    [SerializeField] float destroyAfter = 2;

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
    private Transform[] DynamicMovePoints;
    private int DestinationCounter = 0;
    private int Length;
    private Vector3 playerCurrentPosition;

    void Start()
    {
        InitVariables();

        rootNode = new RootNode();

        Sequence enemyAi = new Sequence("Enemy Ai");
        Leaf setPoint = new Leaf("Set Point", SetPoint);
        Leaf followPath = new Leaf("Follow Path", FollowPath);
        Leaf suicideMech = new Leaf("Suicide Mechanism Ai", SucideMech);
        Leaf destruction = new Leaf("Destroy Object", Destruction);
        
        enemyAi.AddChild(setPoint);
        enemyAi.AddChild(followPath);
        enemyAi.AddChild(suicideMech);
        enemyAi.AddChild(destruction);
        rootNode.AddChild(enemyAi);
    }

    public Node.Status Destruction()
    {
        Destroy(gameObject, destroyAfter);
        return Node.Status.SUCCESS;
    }

    public Node.Status SucideMech()
    {
        playerCurrentPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 direction = transform.position - playerCurrentPosition;
        rb.AddForce(-direction * force, ForceMode2D.Impulse);
        return Node.Status.SUCCESS;
    }

    public Node.Status SetPoint()
    {
        DynamicMovePoints = GameObject.Find("Spawner").GetComponent<Spawner>().MovePoints_Set_01;
        Length = DynamicMovePoints.Length;
        return Node.Status.SUCCESS;
    }

    public Node.Status FollowPath()
    {
        return SetMultipleDestinations(DynamicMovePoints);
    }

    public Node.Status GoToTarget()
    {
        return SetDestination(StayPoint);
    }

    Node.Status SetMultipleDestinations(Transform[] destination)
    {
        distanceBtwDestin = Vector3.Distance(transform.position,destination[DestinationCounter].position);

        if(distanceBtwDestin > 0.5 && DestinationCounter < Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination[DestinationCounter].position,speed * Time.deltaTime);
            return Node.Status.RUNNING;
        }
        else if(DestinationCounter == Length - 1)
        {
            return Node.Status.SUCCESS;
        }
        else if(distanceBtwDestin < 0.5 && DestinationCounter < Length)
        {
            DestinationCounter += 1;
            return Node.Status.RUNNING;
        }

        return Node.Status.RUNNING;
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
        rb = GetComponent<Rigidbody2D>();
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
