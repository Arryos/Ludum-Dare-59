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
		agent.SetDestination(target.position);
		this.target = target;
	}
}
