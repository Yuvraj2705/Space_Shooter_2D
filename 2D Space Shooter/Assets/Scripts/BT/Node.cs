using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public enum Status { SUCCESS, RUNNING, FAILURE };
	public Status status;

	public List<Node> childs = new List<Node>();

	public string name;

	public int activeChildIndex = 0;

	public Node()
	{

	}

	public Node(string _name)
	{
		name = _name;
	}

	public void AddChild(Node _node)
	{
		childs.Add(_node);
	}

	public virtual Status Process()
	{
		return Status.SUCCESS;
	}
}
