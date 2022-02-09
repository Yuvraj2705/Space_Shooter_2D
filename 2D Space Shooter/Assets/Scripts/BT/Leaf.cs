using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
	public delegate Status Tick();
	public Tick ProcessMethod;

	public Leaf()
	{

	}

	public Leaf(string _name, Tick _ProcessMethod)
	{
		name = _name;
		ProcessMethod = _ProcessMethod;
	}

	public override Status Process()
	{
		if (ProcessMethod != null)
			return ProcessMethod();

		return Status.FAILURE;
	}
}
