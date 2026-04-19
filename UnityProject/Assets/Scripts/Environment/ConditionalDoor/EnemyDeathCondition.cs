using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathCondition : EnvironmentCondition
{
	[Header("Enemies")]
	[SerializeField]
	List<EnemyDamagable> enemyDamagables;

	public override Action ConditionFulfilled { get; set; }

	private void OnEnable()
	{
		foreach(EnemyDamagable enemy in enemyDamagables)
		{
			enemy.OnDeath += CheckCondition;
		}
	}

	private void CheckCondition(object sender, System.EventArgs e)
	{
		if (sender is EnemyDamagable enemy)
		{
			enemyDamagables.Remove(enemy);
		}

		if(enemyDamagables.Count == 0)
		{
			ConditionFulfilled?.Invoke();
		}
	}
}
