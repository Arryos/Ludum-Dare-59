using UnityEngine;

public class OneTimeControlsMenu : MonoBehaviour
{
	[SerializeField] private GameObject objects;

	private void Start()
	{
		if (!GameManager.Instance.IsPaused)
		{
			GameManager.Instance.TogglePause();
		}

		objects.SetActive(true);
	}

	private void Update()
	{
		if (GameManager.Instance.IsPaused)
		{
			if (!Input.anyKeyDown)
			{
				return;
			}

			GameManager.Instance.TogglePause();
		}

		Destroy(gameObject);
	}
}