using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDamagable : DamagableObject
{
	[SerializeField] private DamagingObject damagingObject;

	[Header("Shield")]
	[SerializeField] private CapsuleCollider collider;

	[SerializeField] private Transform shieldParent;
	[SerializeField] private Shield shieldPrefab;

	[Space]
	[SerializeField] private Frequencies[] initialFrequencies;

	[SerializeField] private bool debugWithSpheres;

	public List<Shield> Shields { get; private set; }

	private void Awake()
	{
		Shields = new List<Shield>();

		foreach (Transform child in shieldParent)
		{
			Destroy(child.gameObject);
		}

		int size = 2;
		Shield lastShield = null;
		foreach (Frequencies frequency in initialFrequencies)
		{
			Shield newShield = Instantiate(shieldPrefab, shieldParent);
			newShield.Frequency = frequency;
			newShield.transform.localPosition = Vector3.zero + ((size - 2) * 0.001f * Vector3.forward);
			newShield.transform.localScale = new Vector3(size, 1.0f, size);
			newShield.ChangeGridVisibility(false);
			size++;

			lastShield = newShield;

			Shields.Add(newShield);
		}

		if (lastShield != null)
		{
			lastShield.ChangeGridVisibility(true);
		}

		collider.radius = 1 + (0.5f * (size - 2));
		collider.height = 3 * collider.radius;
		Frequency = initialFrequencies[^1];
	}

	private void Update()
	{
		if (transform.position.y < -10f)
		{
			Die();
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Color previousColor = Handles.color;
		int size = 2;
		foreach (Frequencies frequency in initialFrequencies)
		{
			Handles.color = frequency switch
			{
				Frequencies.Square => Color.green,
				Frequencies.Triangle => Color.red,
				Frequencies.Wave => Color.blue,
				_ => Handles.color
			};
			Handles.DrawWireDisc(transform.position, Vector3.up, size * 0.5f);

			size++;
		}

		Handles.color = previousColor;
	}
#endif

	private void OnTriggerEnter(Collider other)
	{
		DamagingObject otherDamagingObject = other.GetComponent<DamagingObject>();
		if (otherDamagingObject == null)
		{
			return;
		}

		Shield firstShield = shieldParent.GetChild(shieldParent.childCount - 1).GetComponent<Shield>();
		if (firstShield == null)
		{
			return;
		}

		if (otherDamagingObject.Frequency != firstShield.Frequency)
		{
			firstShield.BadHit();
			return;
		}

		Destroy(shieldParent.GetChild(shieldParent.childCount - 1).gameObject);

		if (shieldParent.childCount > 1)
		{
			Shield nextShield = shieldParent.GetChild(shieldParent.childCount - 2).GetComponent<Shield>();
			if (nextShield == null)
			{
				return;
			}

			damagingObject.Frequency = nextShield.Frequency;
			Frequency = nextShield.Frequency;

			nextShield.ChangeGridVisibility(true);

			collider.radius = 1 + (0.5f * (shieldParent.childCount - 2));
			collider.height = 3 * collider.radius;
			return;
		}

		Die();
	}

	public override void Die()
	{
		base.Die();
		Destroy(transform.parent.gameObject);
	}
}