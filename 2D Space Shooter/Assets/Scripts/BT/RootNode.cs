using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
	public RootNode()
	{
		name = "Root";
	}

	public RootNode(string _name)
	{
		name = _name;
	}

	struct NodeWithLevel
	{
		public int level;
		public Node node;
	}

	public override Status Process()
	{
		return childs[activeChildIndex].Process();
	}

	public void DebugTreeWithLevel()
	{
		string fullTreeStr = "";
		Stack<NodeWithLevel> nodes = new Stack<NodeWithLevel>();

		Node activeNode = this;
		nodes.Push( new NodeWithLevel { level = 0, node = activeNode });

		while (nodes.Count != 0)
		{
			NodeWithLevel nextNode = nodes.Pop();
			fullTreeStr += new string ('+', nextNode.level) + nextNode.node.name + "\n";

			for (int i = nextNode.node.childs.Count - 1; i >= 0; i--)
			{
				nodes.Push(new NodeWithLevel { level = nextNode.level + 1, node = nextNode.node.childs[i] });
			}
		}

		Debug.Log(fullTreeStr);
	}

	public void DebugTree()
	{
		string fullTreeStr = "";
		Stack<Node> nodes = new Stack<Node>();
		
		Node activeNode = this;
		nodes.Push(activeNode);

		while (nodes.Count != 0)
		{
			Node nextNode = nodes.Pop();
			fullTreeStr += nextNode.name + "\n";

			for (int i = nextNode.childs.Count - 1; i >= 0; i--)
			{
				nodes.Push(nextNode.childs[i]);
			}
		}

		Debug.Log(fullTreeStr);
	}
}
