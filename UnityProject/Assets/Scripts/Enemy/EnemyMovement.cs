using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
	[Header("Components")]
	[SerializeField]
	private NavMeshAgent agent;

	[Header("Parameters")]
	[field: SerializeField]
	public float Speed { get; set; }

	public bool CanMove { get; set; }

	private Transform target;
	private Ray floorRay;

	void Update()
    {
		if (target == null) return;

		if (!CanMove)
		{
			agent.speed = 0f;
			return;
		}

		agent.speed = Speed;
		transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
	}

	public void GoToTarget(Transform target)
	{
		if (agent.isActiveAndEnabled)
		{
			agent.SetDestination(target.position);
			this.target = target;
		}
	}

	private void FixedUpdate()
	{
		agent.enabled = IsGrounded();
	}

	private bool IsGrounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, 1.1f);
	}
}
