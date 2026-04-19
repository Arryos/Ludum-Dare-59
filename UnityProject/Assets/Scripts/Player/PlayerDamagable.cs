using System;
using UnityEngine;

public class PlayerDamagable : DamagableObject
{
	[SerializeField] private PlayerWaveEmission playerWaveEmission;

	public Action<Frequencies> onPlayerFrequencyChanged;

	private void Update()
	{
		if (transform.position.y < -10f)
		{
			Die();
		}
	}

	private void OnEnable()
	{
		playerWaveEmission.Frequency = Frequency;
	}

	private void OnTriggerEnter(Collider other)
	{
		DamagingObject damagingObject = other.GetComponent<DamagingObject>();
		if (damagingObject == null)
		{
			return;
		}

		if (damagingObject.Target == DamageTargets.Player)
		{
			Die();
		}
	}

	protected override void OnFrequencyChanged()
	{
		playerWaveEmission.Frequency = Frequency;
		onPlayerFrequencyChanged?.Invoke(Frequency);
		base.OnFrequencyChanged();
	}

	public override void Die()
	{
		base.Die();
		GameManager.Instance.GameOver();
		Destroy(gameObject);
	}
}