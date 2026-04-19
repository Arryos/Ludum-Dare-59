using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : DamagingObject
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 5f;

	[Header("Color")]
	[SerializeField] private Color squareBulletColor;
	[SerializeField] private Color triangleBulletColor;
	[SerializeField] private Color waveBulletColor;

	[Header("Sprites")]
	[SerializeField] private Sprite squareBulletSprite;
	[SerializeField] private Sprite triangleBulletSprite;
	[SerializeField] private Sprite waveBulletSprite;

	[Header("References")]
	[SerializeField] private Image bulletImage;

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
		bulletImage.color = Frequency switch
		{
			Frequencies.Square => squareBulletColor,
			Frequencies.Triangle => triangleBulletColor,
			Frequencies.Wave => waveBulletColor,
			_ => bulletImage.color
		};

		bulletImage.sprite = Frequency switch
		{
			Frequencies.Square => squareBulletSprite,
			Frequencies.Triangle => triangleBulletSprite,
			Frequencies.Wave => waveBulletSprite,
			_ => bulletImage.sprite
		};
	}
}