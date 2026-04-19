using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		GameManager.Instance.LevelComplete();
	}
}
