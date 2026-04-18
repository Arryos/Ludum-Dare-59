using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private Transform cameraTransform;

	private void Awake()
	{
		cameraTransform = Camera.main.transform;
	}

	private void Update()
	{
		if (cameraTransform == null) return;

		Vector3 directionToCamera = cameraTransform.position - transform.position;
		transform.LookAt(transform.position - directionToCamera.normalized);
	}
}