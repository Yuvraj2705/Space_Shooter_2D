using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
	public Sequence(string _name)
	{
		name = _name;
	}

	public override Status Process()
	{
		Status childStatus = childs[activeChildIndex].Process();

		if (childStatus == Status.RUNNING)
			return Status.RUNNING;

		if (childStatus == Status.FAILURE)
			return Status.FAILURE;

		activeChildIndex++;
		if (activeChildIndex >= childs.Count)
		{
			activeChildIndex = 0;
			return Status.SUCCESS;
		}

		return Status.RUNNING;
	}
}
