using DG.Tweening;
using UnityEngine;

public class TweenShake : MonoBehaviour
{
	private Vector3 initialPosition;
	private Tween currentTween;

	void Awake()
	{
		initialPosition = transform.localPosition;
	}

	public void Shake(float duration, float strength)
	{
		// Si un shake est déjà en cours, on le termine proprement
		if (currentTween != null && currentTween.IsActive())
		{
			currentTween.Kill();
			transform.localPosition = initialPosition;
		}

		currentTween = transform.DOShakePosition(
			duration,
			strength
		).OnComplete(() =>
		{
			transform.localPosition = initialPosition;
		});
	}
}
