using DG.Tweening;
using UnityEngine;

public class TweenBounce : MonoBehaviour
{
	[SerializeField]
	float startScale;

	Tween tween;

	public void Bounce()
	{
		if (tween != null && tween.IsActive())
		{
			tween.Complete();
		}

		transform.localScale = Vector3.one * startScale;

		tween = transform.DOScale(1f, 0.5f)
				 .SetEase(Ease.OutBack);
	}
}
