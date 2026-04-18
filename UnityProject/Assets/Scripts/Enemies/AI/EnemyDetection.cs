using System;
using TMPro;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
	[Header("Debug")]
	[SerializeField]
	private bool canDetect = true;

	[field: SerializeField]
	public Transform Target { get; set; }

	[Header("Parameters")]
	[Header("Radius")]
	[SerializeField]
	private bool hasRadius;

	[SerializeField]
	private float detectionRadius;

	[Header("Sight")]
	[SerializeField]
	private bool seeThroughWalls = false;

	[SerializeField]
	private LayerMask obstacleMask;

	[SerializeField]
	private bool directionalSight = false;

	[SerializeField]
	private float viewAngle;

	[Header("Frequency")]
	[SerializeField]
	private bool detectAllFrequencies = true;
	[SerializeField]
	private Frequencies detectionFrequency;

	public Action TargetDetected;

	private bool isDetecting;
	private Vector3 dirToPlayer;

	private void Awake()
	{
		isDetecting = false;
	}

	void Update()
    {
		if (!canDetect || Target == null) return;

		if(TargetInRange() && TargetInSight() && TargetInFrequency() && ! isDetecting)
		{
			isDetecting = true;
			TargetDetected?.Invoke();
		}
	}

	private bool TargetInRange()
	{
		if (!hasRadius) return true;

		return Vector3.Distance(transform.position, Target.position) <= detectionRadius;
	}

	private bool TargetInSight()
	{
		bool sightDetected = true;

		dirToPlayer = (Target.position - transform.position).normalized;

		// Detect Sight Obstruction
		if (!seeThroughWalls)
		{
			sightDetected &= !Physics.Linecast(transform.position, Target.position, obstacleMask);
		}

		// Detect Sight Angle
		if (directionalSight)
		{
			float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);

			sightDetected &= angleToPlayer < viewAngle;
		}

		return sightDetected;
	}

	private bool TargetInFrequency()
	{
		if (detectAllFrequencies)
		{
			return true;
		}
		if (Target.TryGetComponent(out FrequencyObject frequencyObject))
		{
			return frequencyObject.Frequency == detectionFrequency;
		}
		return true;
	}

	#region Debug

	private void OnDrawGizmos()
	{
		if (hasRadius)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, detectionRadius);
		}
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, Target.position);

		// View direction
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward);
	}
	#endregion
}
