using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
	[Header("Components")]
	[SerializeField]
	private EnemyDetection detection;
	[SerializeField]
	private NavMeshAgent agent;

	[Header("Parameters")]
	[SerializeField]
	private float speed;

	private bool canMove;
	private Transform target;

	private void OnEnable()
	{
		detection.TargetDetected += OnTargetDetected;
	}
	private void OnDisable()
	{
		detection.TargetDetected -= OnTargetDetected;
	}

    void Update()
    {
		if (!canMove) return;

		agent.SetDestination(target.transform.position);
		agent.speed = speed;
	}

	private void OnTargetDetected(Transform targetTransform)
	{
		target = targetTransform;
		canMove = true;
	}
}
