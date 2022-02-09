using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject EnemySpawn;

    [Header("Player Settings")]
    [SerializeField] float speed = 5f;

    [Header("Tree Status")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    [Header("Debug")]
    [SerializeField] private Transform[] DynamicMovePoints;

    private int DestinationCounter = 0;
    float distanceBtwDestin = 0.0f;
    private int Length;
    [HideInInspector] public bool EnemySpawnCheck;
    

    RootNode rootNode;

    void Start()
    {
        InitVariables();
        Rooting();
    }

    void Rooting()
    {
        rootNode = new RootNode();

        Sequence bossAi = new Sequence("Boss Ai");
        Leaf setPoints = new Leaf("Set Points", SetPoints);
        Leaf movement = new Leaf("Movement", Movement);

        bossAi.AddChild(setPoints);
        bossAi.AddChild(movement);

        rootNode.AddChild(bossAi);
    }

    public Node.Status SetPoints()
    {
        DynamicMovePoints = GameObject.Find("Test Input").GetComponent<MovePoints>().BossPaths;
        Length = DynamicMovePoints.Length;
        return Node.Status.SUCCESS;
    }

    public Node.Status Movement()
    {
        return SetMultipleDestinations(DynamicMovePoints);
    }

    Node.Status SetMultipleDestinations(Transform[] destination)
    {
        distanceBtwDestin = Vector3.Distance(transform.position,destination[DestinationCounter].position);

        if(distanceBtwDestin > 0.5 && DestinationCounter < Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination[DestinationCounter].position, speed * Time.deltaTime);
            return Node.Status.RUNNING;
        }
        else if(DestinationCounter == Length - 1)
        {
            DestinationCounter = 0;
            if(EnemySpawnCheck)
            {
                Instantiate(EnemySpawn, transform.position, Quaternion.identity);
            }
        }
        else if(distanceBtwDestin < 0.5 && DestinationCounter < Length)
        {
            DestinationCounter += 1;
            if(EnemySpawnCheck)
            {
                Instantiate(EnemySpawn, transform.position, Quaternion.identity);
            }
            return Node.Status.RUNNING;
        }

        return Node.Status.RUNNING;
    }

    void InitVariables()
    {
        EnemySpawnCheck = false;
        transform.Rotate(new Vector3(0,0,180));
    }

    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }
}
