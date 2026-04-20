using System;
using System.Collections;
using UnityEngine;

public class PlayerDamagable : DamagableObject
{
	[SerializeField] private PlayerWaveEmission playerWaveEmission;

	[Header("Death")]
	[SerializeField] private Transform playerModel;
	[SerializeField] private AudioSource deathSFX;
	[SerializeField] private GameObject deathVFXPrefab;

	public Action<Frequencies> onPlayerFrequencyChanged;
	private Coroutine deathCoroutine;

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
		if (deathCoroutine != null)
		{
			return;
		}

		GetComponent<PlayerController>().CancelInputs();

		deathSFX.Play();

		Instantiate(deathVFXPrefab, transform);
		deathCoroutine = StartCoroutine(DeathCoroutine());
	}

	private IEnumerator DeathCoroutine()
	{
		float elapsedTime = 0f;
		while(elapsedTime < 2.0f)
		{
			playerModel.localScale = playerModel.localScale * (1f - elapsedTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		base.Die();
		GameManager.Instance.GameOver();
		Destroy(gameObject);
	}
}