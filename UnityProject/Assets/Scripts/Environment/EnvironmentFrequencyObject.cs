using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFrequencyObject : FrequencyObject
{
	[SerializeField]
	private BoxCollider objectCollider;
	[SerializeField]
	private MeshRenderer meshRenderer;
	[SerializeField]
	private TweenBounce tweenBounce;

	[Header("Enabled Materials")]
	[SerializeField]
	private Material squareEnabledMaterial;
	[SerializeField]
	private Material triangleEnabledMaterial;
	[SerializeField]
	private Material waveEnabledMaterial;

	[Header("Disabled Materials")]
	[SerializeField]
	private Material squareDisabledMaterial;
	[SerializeField]
	private Material triangleDisabledMaterial;
	[SerializeField]
	private Material waveDisabledMaterial;

	[Header("Enemies References")]
	[SerializeField]
	private List<DamagableObject> enemiesDamageable;

	private PlayerDamagable playerDamagable;
	private bool isActive;

	private void Awake()
	{
		playerDamagable = GameManager.Instance.PlayerDamagable;
	}

	private void OnEnable()
	{
		playerDamagable.onPlayerFrequencyChanged += CheckNewFrequency;
	}
	private void OnDisable()
	{
		playerDamagable.onPlayerFrequencyChanged -= CheckNewFrequency;
	}

	private void OnValidate()
	{
		switch (Frequency)
		{
			case Frequencies.Square:
				meshRenderer.material = squareEnabledMaterial;
				break;
			case Frequencies.Triangle:
				meshRenderer.material = triangleEnabledMaterial;
				break;
			case Frequencies.Wave:
				meshRenderer.material = waveEnabledMaterial;
				break;
			default:
				break;
		}
	}

	private void Start()
	{
		CheckNewFrequency(GameManager.Instance.PlayerFrequency);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isActive) return;

		if(other.TryGetComponent(out PlayerDamagable player))
		{
			player.Die();
		}
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
		isActive = true;
		objectCollider.enabled = true;

		Vector3 point = objectCollider.ClosestPoint(playerDamagable.transform.position);

		if (point == playerDamagable.transform.position)
		{
			playerDamagable.Die();
		}

		foreach (DamagableObject enemy in enemiesDamageable)
		{
			if(enemy != null)
			{
				Vector3 enemyPoint = objectCollider.ClosestPoint(playerDamagable.transform.position);
				if (enemyPoint == enemy.transform.position)
				{
					enemy.Die();
				}
			}
		}

		tweenBounce.Bounce();

		switch (Frequency)
		{
			case Frequencies.Square:
				meshRenderer.material = squareEnabledMaterial;
				break;
			case Frequencies.Triangle:
				meshRenderer.material = triangleEnabledMaterial;
				break;
			case Frequencies.Wave:
				meshRenderer.material = waveEnabledMaterial;
				break;
			default:
				break;
		}
	}

	private void DisableObject()
	{
		isActive = false;

		objectCollider.enabled = false;

		switch (Frequency)
		{
			case Frequencies.Square:
				meshRenderer.material = squareDisabledMaterial;
				break;
			case Frequencies.Triangle:
				meshRenderer.material = triangleDisabledMaterial;
				break;
			case Frequencies.Wave:
				meshRenderer.material = waveDisabledMaterial;
				break;
			default:
				break;
		}
	}
}
