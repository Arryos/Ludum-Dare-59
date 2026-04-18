using UnityEngine;

public class Shield : FrequencyObject
{
	[SerializeField] private MeshRenderer shield;
	[SerializeField] private Material squareShieldMaterial;
	[SerializeField] private Material triangleShieldMaterial;
	[SerializeField] private Material waveShieldMaterial;

	protected override void OnFrequencyChanged()
	{
		shield.material = Frequency switch
		{
			Frequencies.Square => squareShieldMaterial,
			Frequencies.Triangle => triangleShieldMaterial,
			Frequencies.Wave => waveShieldMaterial,
			_ => shield.material
		};
	}
}