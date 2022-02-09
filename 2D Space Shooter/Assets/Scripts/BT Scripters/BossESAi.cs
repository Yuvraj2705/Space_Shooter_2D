using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossESAi : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float speed = 1f;
    [SerializeField] float WaitFor;
    [SerializeField] float destroyAfter = 2;

    [Header("Tree Status")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    RootNode rootNode;
    Rigidbody2D rb;
    float Timer;
    Vector3 playerPos;

    void Start()
    {
        InitVariables();
        Rooting();
    }

    void Rooting()
    {
        rootNode = new RootNode();
        Sequence enemyAi = new Sequence("EnemyAi");
        Leaf wait = new Leaf("waiting", Wait);
        Leaf followPlayer = new Leaf("FollowMech", FollowPlayer);
        Leaf destroy = new Leaf("Destroy", Destruction);

        enemyAi.AddChild(wait);
        enemyAi.AddChild(followPlayer);
        enemyAi.AddChild(destroy);

        rootNode.AddChild(enemyAi);
    }

    public Node.Status Destruction()
    {
        Destroy(gameObject, destroyAfter);
        return Node.Status.SUCCESS;
    }

    public Node.Status FollowPlayer()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 dir = transform.position - playerPos;
        rb.AddForce(-dir * speed, ForceMode2D.Impulse);
        return Node.Status.SUCCESS;
    }

    public Node.Status Wait()
    {
        return WaitForParticularSeconds(WaitFor);
    }

    Node.Status WaitForParticularSeconds(float waitTime)
    {
        Timer += Time.deltaTime;
        if(Timer > waitTime)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    void InitVariables()
    {
        transform.Rotate(new Vector3(0,0,180));
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }
}
