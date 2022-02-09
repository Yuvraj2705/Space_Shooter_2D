using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETOAi : MonoBehaviour
{
    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private Vector3 target;
    
    void Start()
    {
        rootNode = new RootNode();
        Sequence enemyAi = new Sequence("Enemy Ai");
        Leaf goToTarget = new Leaf("Go To Target",GoToTarget);
        Leaf destruction = new Leaf("Destruction",Destruction);

        enemyAi.AddChild(goToTarget);
        enemyAi.AddChild(destruction);
        rootNode.AddChild(enemyAi);
    }

    public Node.Status Destruction()
    {
        Destroy(gameObject,2);
        return Node.Status.SUCCESS;
    }

    public Node.Status GoToTarget()
    {
        return SetDestination(target);
    }

    Node.Status SetDestination(Vector3 destination)
    {
        if(Vector3.Distance(transform.position, destination) > 0.5)
        {
            transform.position = Vector3.MoveTowards(transform.position,destination, 5 * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, destination) < 0.5)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }
}
