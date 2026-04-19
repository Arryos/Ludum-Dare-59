using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : DamagingObject
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 5f;
	[SerializeField] private float animationSpeed = 5f;

	[Header("Materials")]
	[SerializeField] private Material squareBulletMaterial;
	[SerializeField] private Material triangleBulletMaterial;
	[SerializeField] private Material waveBulletMaterial;

	[Header("Mesh")]
	[SerializeField] private MeshRenderer meshRenderer;

	private float timeLeft;
	private MaterialPropertyBlock materialPropertyBlock;
	private int offsetId;

	private void Update()
	{
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0)
		{
			Destroy(gameObject);
		}

		transform.Translate(Vector3.forward * (speed * Time.deltaTime));

		// Offset bullet material to animate signal
		meshRenderer.GetPropertyBlock(materialPropertyBlock);
		materialPropertyBlock.SetFloat(offsetId, animationSpeed * Time.time);
		meshRenderer.SetPropertyBlock(materialPropertyBlock);
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
		meshRenderer.material = Frequency switch
		{
			Frequencies.Square => squareBulletMaterial,
			Frequencies.Triangle => triangleBulletMaterial,
			Frequencies.Wave => waveBulletMaterial,
			_ => squareBulletMaterial
		};

		materialPropertyBlock = new MaterialPropertyBlock();
		offsetId = Shader.PropertyToID("_Offset");
	}
}