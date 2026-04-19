using UnityEngine;

public class ConditionalDoor : MonoBehaviour
{
	[SerializeField]
	EnvironmentCondition condition;

	[SerializeField]
	Collider doorCollider;

	private void OnEnable()
	{
		condition.ConditionFulfilled += OpenDoor;
	}
	private void OnDisable()
	{
		condition.ConditionFulfilled += OpenDoor;
	}

	public void OpenDoor()
	{
		doorCollider.enabled = false;
		// TODO: Animate
	}
}
