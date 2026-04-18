using UnityEditor;
using UnityEngine;

public class EnemyDamagable : DamagableObject
{
	[SerializeField] private DamagingObject damagingObject;

	[Header("Shield")]
	[SerializeField] private SphereCollider collider;

	[SerializeField] private Transform shieldParent;
	[SerializeField] private Shield shieldPrefab;

	[Space]
	[SerializeField] private Frequencies[] initialFrequencies;

	[SerializeField] private bool debugWithSpheres;

	private void Awake()
	{
		foreach (Transform child in shieldParent)
		{
			Destroy(child.gameObject);
		}

		int size = 2;
		foreach (Frequencies frequency in initialFrequencies)
		{
			Shield newShield = Instantiate(shieldPrefab, shieldParent);
			newShield.Frequency = frequency;
			newShield.transform.localPosition = Vector3.zero + ((size - 2) * 0.001f * Vector3.forward);
			newShield.transform.localScale *= size;
			size++;
		}

		collider.radius = (size - 1) / 2f;
		Frequency = initialFrequencies[^1];
	}

	private void OnDrawGizmosSelected()
	{
		Color previousColor = debugWithSpheres ? Gizmos.color : Handles.color;
		int size = 2;
		foreach (Frequencies frequency in initialFrequencies)
		{
			if (debugWithSpheres)
			{
				Gizmos.color = frequency switch
				{
					Frequencies.Square => Color.green,
					Frequencies.Triangle => Color.red,
					Frequencies.Wave => Color.blue,
					_ => Gizmos.color
				};
				Gizmos.DrawWireSphere(transform.position, size * 0.5f);
			}
			else
			{
				Handles.color = frequency switch
				{
					Frequencies.Square => Color.green,
					Frequencies.Triangle => Color.red,
					Frequencies.Wave => Color.blue,
					_ => Handles.color
				};
				Handles.DrawWireDisc(transform.position, Vector3.forward, size * 0.5f);
			}

			size++;
		}

		if (debugWithSpheres)
		{
			Gizmos.color = previousColor;
		}
		else
		{
			Handles.color = previousColor;
		}
	}

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
			collider.radius = nextShield.transform.localScale.x / 2f;

			return;
		}

		Die();
	}

	protected override void Die()
	{
		base.Die();
		Destroy(transform.parent.gameObject);
	}
}