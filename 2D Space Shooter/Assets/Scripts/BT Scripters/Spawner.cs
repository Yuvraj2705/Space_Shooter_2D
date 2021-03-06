using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("SpawnPoints and EndPoints")]
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform SpawnPoint_02;
    [SerializeField] Transform SpawnPoint_03;
    [SerializeField] Transform[] HoldPoints_Set_01;
    [SerializeField] Transform[] HoldPoints_Set_02;
    [SerializeField] Transform[] HoldPoints_Set_03;
    [SerializeField] Transform[] MovePoints_Set_01;
    [SerializeField] Transform[] MovePoints_Set_02;
    private Transform[] DynamicHoldPoints;

    [Header("Debug Temp HoldPoint")]
    public Vector3 tempHoldPoint;

    [Header("Prefabs")]
    [SerializeField] GameObject Enemy_01;
    [SerializeField] GameObject Enemy_02;
    [SerializeField] GameObject Boss_01;

    [Header("PowerUps")]
    [SerializeField] Rigidbody2D Health;
    [SerializeField] Transform HealthSpawn;
    [SerializeField] Rigidbody2D Gun;
    [SerializeField] Transform GunSpawn;
    [SerializeField] float PowerUpSpeed;

    [Header("UI")]
    [SerializeField] GameObject WaveOne;
    [SerializeField] GameObject WaveTwo;
    [SerializeField] GameObject WaveThree;
    [SerializeField] GameObject BossWave;

    [Header("Time Debug")]
    [SerializeField] private float timer;

    [Header("Timer Settings")]
    private float SpawnTime;
    [SerializeField] private float timeRate;

    [Header("Spawn Settings")]
    [SerializeField] int NoOfEnemies = 5;
    private int count = 0;
    [HideInInspector] public Transform[] dynamicMovePointsSpawner;
    GameObject[] enemies;
    GameObject[] boss;
    int enemyCount;

    [Header("Debug Tree Status")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    private float t;

    #region Behaviour Tree Variables

    RootNode rootNode;

    #endregion


    void Start()
    {
        RootCoding();
    }

    void RootCoding()
    {
        rootNode = new RootNode();

        Sequence spawner = new Sequence("Spawner");
        Leaf waveOneData = new Leaf("Wave One Data", WaveOneData);
        Leaf waveTwoData = new Leaf("Wave Two Data", WaveTwoData);
        Leaf waveThreeData_01 = new Leaf("Wave Three Data PArt 1", WaveThreeData_01);
        Leaf waveThreeData_02 = new Leaf("Wave Three Data Part 2", WaveThreeData_02);
        Leaf bossData = new Leaf("Boss Data", BossData);
        Leaf spawnETO = new Leaf("SpawnETo", SpawnETOAdvance);
        Leaf spawnETT = new Leaf("SpawnETT", SpawnETT);
        Leaf spawnBoss = new Leaf("Spawning Boss", SpawnBoss);
        Leaf wait = new Leaf("Waiting", WaitingForNext);
        Leaf enemyCheck = new Leaf("Checking Enemies Left", EnemyCheck);
        Leaf bossCheck = new Leaf("Boss Check", BossCheck);
        Leaf backToMenu = new Leaf("Back To Menu", NextScene);

        /*
        ! Wave One
        */
        spawner.AddChild(waveOneData);
        spawner.AddChild(spawnETO);

        /*
        ! Wave Second
        */

        spawner.AddChild(enemyCheck);
        spawner.AddChild(waveTwoData);
        spawner.AddChild(spawnETO);

        /*
        ! Wave Three
        */
        spawner.AddChild(enemyCheck);
        spawner.AddChild(waveThreeData_01);
        spawner.AddChild(spawnETO);
        spawner.AddChild(waveThreeData_02);
        spawner.AddChild(spawnETT);
        spawner.AddChild(enemyCheck);

        /*
        ! Boss Spawn
        */
        spawner.AddChild(spawnBoss);
        spawner.AddChild(bossData);
        spawner.AddChild(spawnETT);
        spawner.AddChild(bossCheck);
        spawner.AddChild(backToMenu);

        rootNode.AddChild(spawner);
    }

    public Node.Status NextScene()
    {
        t += Time.deltaTime;
        if(t > 5)
        {
            GameObject.FindGameObjectWithTag("over").GetComponent<Menu>().activatingGameOverMenu();
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status SpawnBoss()
    {
        var Instance = Instantiate(BossWave, WaveOne.transform.position,Quaternion.identity);
        Destroy(Instance, 2);
        Instantiate(Boss_01,SpawnPoint_03.position,Quaternion.identity);
        PowerUpSpawn();
        return Node.Status.SUCCESS;
    }

    public Node.Status SpawnETT()
    {
        timer += Time.deltaTime;
        if(timer > SpawnTime)
        {
            Instantiate(Enemy_02,SpawnPoint_02.position, Quaternion.identity);
            SpawnTime = timer + timeRate;
            count++;
        }
        if(NoOfEnemies == count)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status SpawnETOAdvance()
    {
        timer += Time.deltaTime;
        if(timer > SpawnTime)
        {
            Instantiate(Enemy_01,SpawnPoint.position, SpawnPoint.rotation);
            tempHoldPoint = DynamicHoldPoints[count].position;
            SpawnTime = timer + timeRate;
            count++;
        }
        if(NoOfEnemies == count)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status BossData()
    {
        timer = 0;
        count = 0;
        timeRate = 2f;
        SpawnTime = 10;
        NoOfEnemies = 10;
        dynamicMovePointsSpawner = MovePoints_Set_02;
        return Node.Status.SUCCESS;
    }

    public Node.Status WaveThreeData_02()
    {
        timer = 0;
        count = 0;
        timeRate = 0.5f;
        SpawnTime = 5;
        NoOfEnemies = 7;
        dynamicMovePointsSpawner = MovePoints_Set_01;
        return Node.Status.SUCCESS;
    }

    public Node.Status WaveThreeData_01()
    {
        timer = 0;
        count = 0;
        timeRate = 1;
        SpawnTime = 5;
        DynamicHoldPoints = HoldPoints_Set_03;
        NoOfEnemies = DynamicHoldPoints.Length;
        var Instance = Instantiate(WaveThree, WaveOne.transform.position,Quaternion.identity);
        Destroy(Instance, SpawnTime);
        PowerUpSpawn();
        return Node.Status.SUCCESS;
    }

    public Node.Status WaveTwoData()
    {
        timer = 0;
        count = 0;
        timeRate = 1;
        SpawnTime = 5;
        DynamicHoldPoints = HoldPoints_Set_02;
        NoOfEnemies = DynamicHoldPoints.Length;
        var Instance = Instantiate(WaveTwo, WaveOne.transform.position,Quaternion.identity);
        Destroy(Instance, SpawnTime);
        PowerUpSpawn();
        return Node.Status.SUCCESS;
    }

    public Node.Status WaveOneData()
    {
        timer = 0;
        count = 0;
        timeRate = 1;
        SpawnTime = 5;
        DynamicHoldPoints = HoldPoints_Set_01;
        NoOfEnemies = DynamicHoldPoints.Length;
        var Instance = Instantiate(WaveOne, WaveOne.transform.position,Quaternion.identity);
        Destroy(Instance, SpawnTime);
        return Node.Status.SUCCESS;
    }

    void PowerUpSpawn()
    {
        var health = Instantiate(Health, HealthSpawn.position,Quaternion.identity);
        health.AddForce(Vector2.down * PowerUpSpeed, ForceMode2D.Impulse);
        Destroy(health.gameObject,10);

        var gun = Instantiate(Gun, GunSpawn.position, Quaternion.identity);
        gun.AddForce(Vector2.down * PowerUpSpeed, ForceMode2D.Impulse);
        Destroy(gun.gameObject,10);
    }

    public Node.Status WaitingForNext()
    {
        timer = 0;
        return Waiting(5);
    }

    public Node.Status EnemyCheck()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        if(enemyCount == 0)
        {
            return Node.Status.SUCCESS;
        }  
        return Node.Status.RUNNING;
    }

    public Node.Status BossCheck()
    {
        boss = GameObject.FindGameObjectsWithTag("Boss");
        if(boss.Length == 0)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    Node.Status Waiting(float waitTime)
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            timer = 0;
            Debug.Log("Waited for : " + waitTime);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    void Update()
    {
        if(treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }
}
