using UnityEngine;

public class PlayerBullet : DamagingObject
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 5f;

	[Header("Color")]
	[SerializeField] private MeshRenderer bulletRenderer;

	[SerializeField] private Material squareBulletMaterial;
	[SerializeField] private Material triangleBulletMaterial;
	[SerializeField] private Material waveBulletMaterial;

	private float timeLeft;

	private void Update()
	{
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0)
		{
			Destroy(gameObject);
		}

		transform.Translate(Vector3.forward * (speed * Time.deltaTime));
	}

	private void OnEnable()
	{
		timeLeft = lifetime;
		UpdateFrequencyMaterial();
	}

	private void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}

	protected override void OnFrequencyChanged()
	{
		UpdateFrequencyMaterial();
		base.OnFrequencyChanged();
	}

	private void UpdateFrequencyMaterial()
	{
		bulletRenderer.material = Frequency switch
		{
			Frequencies.Square => squareBulletMaterial,
			Frequencies.Triangle => triangleBulletMaterial,
			Frequencies.Wave => waveBulletMaterial,
			_ => bulletRenderer.material
		};
	}
}