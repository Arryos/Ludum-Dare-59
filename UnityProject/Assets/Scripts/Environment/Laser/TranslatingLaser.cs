using UnityEngine;

public class TranslatingLaser : MonoBehaviour
{
	[SerializeField]
	Transform startPoint;
	[SerializeField]
	Transform endPoint;
	[SerializeField]
	float speed;
	[SerializeField]
	bool pingPong;

	private Transform target;

	void Start()
	{
		if (startPoint != null)
		{
			transform.position = startPoint.position;
			target = endPoint;
		}
	}

	void Update()
	{
		if (target == null) return;

		transform.position = Vector3.MoveTowards(
			transform.position,
			target.position,
			speed * Time.deltaTime
		);

		if (Vector3.Distance(transform.position, target.position) < 0.01f)
		{
			if (pingPong)
			{
				target = (target == startPoint) ? endPoint : startPoint;
			}
			else
			{
				enabled = false;
			}
		}
	}
}
