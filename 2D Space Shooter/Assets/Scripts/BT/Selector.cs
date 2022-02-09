using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
	public Selector(string _name)
	{
		name = _name;
	}

	public override Status Process()
	{
		Status childStatus = childs[activeChildIndex].Process();

		if (childStatus == Status.RUNNING)
			return Status.RUNNING;

		if (childStatus == Status.SUCCESS)
		{
			activeChildIndex = 0;
			return Status.SUCCESS;
		}

		activeChildIndex++;
		if (activeChildIndex >= childs.Count)
		{
			activeChildIndex = 0;
			return Status.FAILURE;
		}

		return Status.RUNNING;
	}
}
