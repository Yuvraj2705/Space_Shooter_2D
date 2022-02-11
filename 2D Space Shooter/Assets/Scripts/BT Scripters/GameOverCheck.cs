using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    [Header("Tree Status")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    RootNode rootNode;
    GameObject player;

    private float timer = 0;
    private float waitTime = 2;

    void Start()
    {
        rootNode = new RootNode();

        Sequence playerCheck = new Sequence("Check Player");
        Leaf isPlayerDead = new Leaf("is PlayerDead", CheckingPlayer);
        Leaf nextScene = new Leaf("next scene", NextScene);
        Leaf waitFor = new Leaf("Wait", WaitFor);

        playerCheck.AddChild(isPlayerDead);
        playerCheck.AddChild(waitFor);
        playerCheck.AddChild(nextScene);

        rootNode.AddChild(playerCheck);
    }

    public Node.Status CheckingPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status NextScene()
    {
        GameObject.FindGameObjectWithTag("over").GetComponent<Menu>().activatingGameOverMenu();
        return Node.Status.SUCCESS;
    }

    public Node.Status WaitFor()
    {
        timer =  timer + Time.deltaTime;
        if(timer > waitTime)
        {
            timer = 0;
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
