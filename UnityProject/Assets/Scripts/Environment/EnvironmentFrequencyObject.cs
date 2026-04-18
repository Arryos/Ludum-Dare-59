using UnityEngine;

public class EnvironmentFrequencyObject : FrequencyObject
{
	[SerializeField]
	private Collider[] colliders;

	private void OnEnable()
	{
		GameManager.Instance.onPlayerFrequencyChanged += 
	}

	private void CheckNewFrequency(Frequencies newFrequency)
	{
		if(Frequency == newFrequency)
		{
			EnableObject();
		}
		else
		{
			DisableObject();
		}
	}

	private void EnableObject()
	{

	}

	private void DisableObject()
	{

	}
}
