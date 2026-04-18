using UnityEngine;

public class EnvironmentFrequencyObject : FrequencyObject
{
	[SerializeField]
	private Collider objectCollider;
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

	private PlayerDamagable playerDamagable;

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
		objectCollider.enabled = true;

		tweenBounce.Bounce();

		// Kill player if they are inside collider
		if (objectCollider.bounds.Contains(playerDamagable.transform.position))
		{
			GameManager.Instance.GameOver();
		}

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
