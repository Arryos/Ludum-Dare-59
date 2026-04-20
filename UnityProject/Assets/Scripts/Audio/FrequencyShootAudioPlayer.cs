using UnityEngine;

public class FrequencyShootAudioPlayer : MonoBehaviour
{
	[SerializeField]
	CustomAudioSource squareAudioSource;
	[SerializeField]
	CustomAudioSource triangleAudioSource;
	[SerializeField]
	CustomAudioSource waveAudioSource;
	public void PlayFrequency(Frequencies frequency)
	{
		switch(frequency)
		{
			case Frequencies.Square:
				squareAudioSource.PlaySound();
				break;
			case Frequencies.Triangle:
				triangleAudioSource.PlaySound();
				break;
			case Frequencies.Wave:
				waveAudioSource.PlaySound();
				break;
			default:
				break;
		}
	}
}
