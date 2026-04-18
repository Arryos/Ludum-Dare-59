using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyChase : EnemyState
{
	[Header("Parameters")]
	[SerializeField]
	private float chaseSpeed;

	[SerializeField]
	private float attackRange;

	[SerializeField]
	private float attackCooldown;

	[SerializeField]
	private float navRefreshSpeed;

	[Header("Components")]
	[SerializeField]
	private EnemyState outputState;

	[SerializeField]
	private EnemyMovement enemyMovement;

	[SerializeField]
	private EnemyDetection enemyDetection;


	private float currentTick = 0f;
	private float currentAttackTime = 0f;
	private bool canAttack = true;

	public override void Enter()
	{
		enemyMovement.Speed = chaseSpeed;
		enemyMovement.CanMove = true;
	}

	public override void Exit() { }

	public override void UpdateState(float delta)
	{
		// Check navigation refresh
		currentTick += delta;
		if(currentTick >= navRefreshSpeed)
		{
			currentTick = 0f;
			RefreshTarget();
		}

		// Check attack cooldown
		if (!canAttack)
		{
			currentAttackTime += delta;

			if (currentAttackTime >= attackCooldown)
			{
				canAttack = true;
			}
			return;
		}

		// Check if player is in range for attack
		if (Vector3.Distance(transform.position, enemyDetection.Target.position) <= attackRange)
		{
			canAttack = false;
			currentAttackTime = 0f;
			Attack();
		}
	}

	public override void FixedUpdateState(float delta) { }

	private async void Attack()
	{
		enemyMovement.CanMove = false;
		// TODO: Play Anim
	}

	private void RefreshTarget()
	{
		enemyMovement.GoToTarget(enemyDetection.Target);
	}
}
