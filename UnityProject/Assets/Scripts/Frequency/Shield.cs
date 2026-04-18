using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Shield : FrequencyObject
{
	[Header("Mesh renderers")]
	[SerializeField] private MeshRenderer hit;

	[SerializeField] private MeshRenderer grid;
	[SerializeField] private MeshRenderer outside;

	[Header("Square")]
	[SerializeField] private Material squareHitMaterial;

	[SerializeField] private Material squareGridMaterial;
	[SerializeField] private Material squareOutsideMaterial;

	[Header("Triangle")]
	[SerializeField] private Material triangleHitMaterial;

	[SerializeField] private Material triangleGridMaterial;
	[SerializeField] private Material triangleOutsideMaterial;

	[Header("Wave")]
	[SerializeField] private Material waveHitMaterial;

	[SerializeField] private Material waveGridMaterial;
	[SerializeField] private Material waveOutsideMaterial;

	[Header("Blink")]
	[SerializeField] private float fadeDuration = 0.5f;

	private CancellationTokenSource cancellationTokenSource;

	protected override void OnFrequencyChanged()
	{
		hit.material = Frequency switch
		{
			Frequencies.Square => squareHitMaterial,
			Frequencies.Triangle => triangleHitMaterial,
			Frequencies.Wave => waveHitMaterial,
			_ => hit.material
		};

		grid.material = Frequency switch
		{
			Frequencies.Square => squareGridMaterial,
			Frequencies.Triangle => triangleGridMaterial,
			Frequencies.Wave => waveGridMaterial,
			_ => grid.material
		};

		outside.material = Frequency switch
		{
			Frequencies.Square => squareOutsideMaterial,
			Frequencies.Triangle => triangleOutsideMaterial,
			Frequencies.Wave => waveOutsideMaterial,
			_ => outside.material
		};
	}

	public void ChangeGridVisibility(bool visible)
	{
		grid.gameObject.SetActive(visible);
	}

	public void BadHit()
	{
		cancellationTokenSource?.Cancel();
		cancellationTokenSource = new CancellationTokenSource();

		BlinkHit(cancellationTokenSource.Token);
	}

	private async void BlinkHit(CancellationToken cancellationToken)
	{
		try
		{
			cancellationToken.ThrowIfCancellationRequested();

			hit.gameObject.SetActive(true);

			float elapsedTime = 0;
			while (elapsedTime < fadeDuration)
			{
				cancellationToken.ThrowIfCancellationRequested();
				hit.material.color = new Color(hit.material.color.r, hit.material.color.g, hit.material.color.b,
					elapsedTime / fadeDuration);
				elapsedTime += Time.deltaTime;
				await Task.Delay(1, cancellationToken);
			}

			hit.material.color = new Color(hit.material.color.r, hit.material.color.g, hit.material.color.b, 1);

			elapsedTime = 0;
			while (elapsedTime < fadeDuration)
			{
				cancellationToken.ThrowIfCancellationRequested();
				hit.material.color = new Color(hit.material.color.r, hit.material.color.g, hit.material.color.b,
					1 - (elapsedTime / fadeDuration));
				elapsedTime += Time.deltaTime;
				await Task.Delay(1, cancellationToken);
			}

			hit.gameObject.SetActive(false);
		}
		catch (OperationCanceledException e)
		{
			// It is normal to cancel this operation.
		}
	}
}