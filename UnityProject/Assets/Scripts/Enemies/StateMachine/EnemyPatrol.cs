using UnityEngine;

public class EnemyPatrol : EnemyState
{
	[Header("Parameters")]
	[SerializeField]
	private float patrolSpeed;

	[Header("Components")]
	[SerializeField]
	private EnemyState outputState;

	[SerializeField]
	private EnemyMovement enemyMovement;

	[SerializeField]
	private EnemyDetection enemyDetection;

	private void OnEnable()
	{
		enemyDetection.TargetDetected += OnTargetDetected;
	}

	private void OnDisable()
	{
		enemyDetection.TargetDetected -= OnTargetDetected;
	}

	public override void Enter()
	{
		enemyMovement.Speed = patrolSpeed;
		enemyMovement.CanMove = true;
	}

	public override void Exit() { }

	public override void UpdateState(float delta) { }

	public override void FixedUpdateState(float delta) { }

	private void OnTargetDetected()
	{
		OnStateTransition?.Invoke(this, outputState);
	}
}
