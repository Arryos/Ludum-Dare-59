using UnityEngine;

public class FrequencyObject : MonoBehaviour
{
	[SerializeField] private Frequencies frequency;

	public Frequencies Frequency
	{
		get => frequency;
		set
		{
			if (frequency == value)
			{
				return;
			}

			frequency = value;
			OnFrequencyChanged();
		}
	}

	protected virtual void OnFrequencyChanged()
	{
	}
}