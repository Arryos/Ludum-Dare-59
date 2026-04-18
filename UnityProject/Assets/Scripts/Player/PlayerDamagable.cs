using System;
using UnityEngine;

public class PlayerDamagable : DamagableObject
{
	[SerializeField] private Shield shield;

	public Action<Frequencies> onPlayerFrequencyChanged;

	private void OnEnable()
	{
		shield.Frequency = Frequency;
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

	private void Update()
	{
		if (transform.position.y < -10f)
		{
			Die();
		}
	}

	protected override void OnFrequencyChanged()
	{
		shield.Frequency = Frequency;
		onPlayerFrequencyChanged?.Invoke(Frequency);
		base.OnFrequencyChanged();
	}

	protected override void Die()
	{
		base.Die();
		GameManager.Instance.GameOver();
		Destroy(gameObject);
	}
}