using System;
using UnityEngine;

public abstract class EnvironmentCondition: MonoBehaviour
{
	public abstract Action ConditionFulfilled { get; set; }
}
