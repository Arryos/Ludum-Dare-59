using UnityEngine;

public class DeathLaser : FrequencyObject
{
	[SerializeField]
	MeshRenderer meshRenderer;
	[SerializeField]
	Material squareMaterial;
	[SerializeField]
	Material triangleMaterial;
	[SerializeField]
	Material waveMaterial;

	private void OnValidate()
	{
		switch (Frequency)
		{
			case Frequencies.Square:
				meshRenderer.material = squareMaterial;
				break;
			case Frequencies.Triangle:
				meshRenderer.material = triangleMaterial;
				break;
			case Frequencies.Wave:
				meshRenderer.material = waveMaterial;
				break;
			default:
				break;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out PlayerDamagable player))
		{
			if(Frequency == player.Frequency)
			{
				player.Die();
			}
		}
	}
}
