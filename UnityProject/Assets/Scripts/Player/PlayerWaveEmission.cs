using UnityEngine;

public class PlayerWaveEmission : FrequencyObject
{
	[Header("Objects")]
	[SerializeField] private Transform sphereTransform;
	[SerializeField] private MeshRenderer shpereMeshRenderer;
	[SerializeField] private Light pointLight;

	[Header("Timings")]
	[SerializeField] private float emissionDuration = 3;
	[SerializeField] private float restDuration = 1;

	[Header("Sphere")]
	[SerializeField] private float sphereSizeStart = 0.001f;
	[SerializeField] private float sphereSizeEnd = 0.1f;
	[Space]
	[SerializeField] private Color sphereSquareColorStart;
	[SerializeField] private Color sphereSquareColorEnd;
	[SerializeField] private Color sphereTriangleColorStart;
	[SerializeField] private Color sphereTriangleColorEnd;
	[SerializeField] private Color sphereWaveColorStart;
	[SerializeField] private Color sphereWaveColorEnd;

	[Header("Light")]
	[SerializeField] private Color lightSquareColor;
	[SerializeField] private Color lightTriangleColor;
	[SerializeField] private Color lightWaveColor;
	[SerializeField] private float lightStartIntensity;
	[SerializeField] private float lightEndIntensity;

	private Color currentColorEnd;
	private Color currentColorStart;

	private float cycleDuration;
	private float elapsedTime;

	private void Start()
	{
		cycleDuration = emissionDuration + restDuration;
		UpdateColorsForFrequency();
	}

	private void Update()
	{
		elapsedTime += Time.deltaTime;

		// Loop the cycle
		if (elapsedTime >= cycleDuration)
		{
			elapsedTime -= cycleDuration;
		}

		// Determine which phase we're in
		if (elapsedTime < emissionDuration)
		{
			// Emission phase
			float progress = elapsedTime / emissionDuration;
			float currentSize = Mathf.Lerp(sphereSizeStart, sphereSizeEnd, progress);
			sphereTransform.localScale = Vector3.one * currentSize;

			// Animate color from start to end
			Color currentColor = Color.Lerp(currentColorStart, currentColorEnd, progress);
			shpereMeshRenderer.material.color = currentColor;

			// Animate light intensity
			pointLight.intensity = Mathf.Lerp(lightStartIntensity, lightEndIntensity, progress);
		}
		else
		{
			// Rest phase
			sphereTransform.localScale = Vector3.one * sphereSizeStart;
			shpereMeshRenderer.material.color = currentColorEnd;

			// Light stays at end values
			pointLight.intensity = lightEndIntensity;
		}
	}

	protected override void OnFrequencyChanged()
	{
		UpdateColorsForFrequency();
	}

	private void UpdateColorsForFrequency()
	{
		switch (Frequency)
		{
			case Frequencies.Square:
				currentColorStart = sphereSquareColorStart;
				currentColorEnd = sphereSquareColorEnd;
				pointLight.color = lightSquareColor;
				break;
			case Frequencies.Triangle:
				currentColorStart = sphereTriangleColorStart;
				currentColorEnd = sphereTriangleColorEnd;
				pointLight.color = lightTriangleColor;
				break;
			case Frequencies.Wave:
				currentColorStart = sphereWaveColorStart;
				currentColorEnd = sphereWaveColorEnd;
				pointLight.color = lightWaveColor;
				break;
		}
	}
}