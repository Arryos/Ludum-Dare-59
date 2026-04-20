using UnityEngine;

public class ConditionalDoor : MonoBehaviour
{
	[SerializeField]
	EnvironmentCondition condition;

	[SerializeField]
	Collider doorCollider;

	[SerializeField]
	Animator doorAnimator;

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
		doorAnimator.SetTrigger("Open");
		doorCollider.enabled = false;
		// TODO: Animate
	}
}
