using System;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
	public Action<EnemyState, EnemyState> OnStateTransition;

	public abstract void Enter();

	public abstract void Exit();

	public abstract void UpdateState(float delta);

	public abstract void FixedUpdateState(float delta);
}
