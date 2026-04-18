using UnityEngine;

public class PlayerDamagable : DamagableObject
{
	[SerializeField] private Shield shield;

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

	protected override void OnFrequencyChanged()
	{
		shield.Frequency = Frequency;
		base.OnFrequencyChanged();
	}

	protected override void Die()
	{
		base.Die();
		Destroy(gameObject);
	}
}